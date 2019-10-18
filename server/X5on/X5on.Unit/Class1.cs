using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace X5on.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        #region fields
        /// <summary>
        ///     服务提供器，主要用于查找 框架配置对象，以及DbContextOptionBuilder对象
        /// </summary>
        private readonly IServiceProvider _provider;
        /// <summary>
        ///     当前请求涉及的scope生命的仓储对象
        /// </summary>
        private Hashtable repositorys;

        private IDbContextTransaction _dbTransaction { get; set; }
        /// <summary>
        ///     上下文对象，UOW内部初始化上下文对象，供当前scope内的操作使用，保证同一上下文
        /// </summary>
        public DbContext DbContext => GetDbContext();
        #endregion

        #region ctor
        public UnitOfWork(IServiceProvider provider)
        {
            _provider = provider;
        }
        #endregion

        #region public

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            if (repositorys == null)
                repositorys = new Hashtable();

            var entityType = typeof(TEntity);
            if (!repositorys.ContainsKey(entityType.Name))
            {
                var baseType = typeof(Repository<,>);
                var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(entityType), DbContext);
                repositorys.Add(entityType.Name, repositoryInstance);
            }

            return (IRepository<TEntity, TKey>)repositorys[entityType.Name];
        }

        public void BeginTransaction()
        {
            //DbContext.Database.UseTransaction(_dbTransaction);//如果多上下文，我们可是在其他上下文直接使用 UserTransaction使用已存在的事务
            _dbTransaction = DbContext.Database.BeginTransaction();
        }

        public int Commit()
        {
            int result = 0;
            try
            {
                result = DbContext.SaveChanges();
                if (_dbTransaction != null)
                    _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                result = -1;
                CleanChanges(DbContext);
                _dbTransaction.Rollback();
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return result;
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            try
            {
                result = await DbContext.SaveChangesAsync();
                if (_dbTransaction != null)
                    _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                result = -1;
                CleanChanges(DbContext);
                _dbTransaction.Rollback();
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return await Task.FromResult(result);
        }

        #endregion

        #region private
        private DbContext GetDbContext()
        {
            var options = _provider.ESoftorOption();

            IDbContextOptionsBuilderCreator builderCreator = _provider.GetServices<IDbContextOptionsBuilderCreator>()
                .FirstOrDefault(d => d.DatabaseType == options.ESoftorDbOption.DatabaseType);

            if (builderCreator == null)
                throw new Exception($"无法解析数据库类型为：{options.ESoftorDbOption.DatabaseType}的{typeof(IDbContextOptionsBuilderCreator).Name}实例");
            //DbContextOptionsBuilder
            var optionsBuilder = builderCreator.Create(options.ESoftorDbOption.ConnectString, null);//TODO null可以换成缓存中获取connection对象，以便性能的提升

            if (!(ActivatorUtilities.CreateInstance(_provider, options.ESoftorDbOption.DbContextType, optionsBuilder.Options) is DbContext dbContext))
                throw new Exception($"上下文对象 “{options.ESoftorDbOption.DbContextType.AssemblyQualifiedName}” 实例化失败，请确认配置文件已正确配置。 ");

            return dbContext;
        }

        /// <summary>
        ///     操作失败，还原跟踪状态
        /// </summary>
        /// <param name="context"></param>
        private static void CleanChanges(DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i].State = EntityState.Detached;
            }
        }

        #endregion

        #region override
        public void Dispose()
        {
            _dbTransaction.Dispose();
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public interface IUnitOfWork
    {
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;

        void BeginTransaction();

        int Commit();
        Task<int> CommitAsync();
    }
}


public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
{
    #region ctor
    public Repository(IUnitOfWork unitOfWork)
    {
        _dbContext = unitOfWork.GetDbContext as DbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    #endregion

    #region fields
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _dbContext;
    #endregion

    #region query
    public TEntity GetByKey(TKey key)
    {
        return _dbSet.Find(key);
    }

    public async Task<TEntity> GetByKeyAsync(TKey key)
    {
        return await _dbSet.FindAsync(key);
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    #endregion

    #region insert
    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    #endregion

    #region update
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Remove(Expression<Func<TEntity, bool>> expression)
    {
        var entities = _dbSet.AsNoTracking().Where(expression).ToList();
        _dbSet.RemoveRange(entities);
    }

    #endregion

    #region remove
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    #endregion
}