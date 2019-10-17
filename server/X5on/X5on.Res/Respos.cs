using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using X5on.Unit;

namespace X5on.Res
{
    public abstract class Respos<T> : IRespos<T> where T : class
    {
        private DbContext context;
        private Unitof unitof;

        public Respos(DbContext Context)
        {
            context = Context;
            unitof = new Unitof(context);
        }

        public virtual bool Add(T t, bool commit = true)
        {
            context.Add(t);
            if (commit)
            {
                return unitof.Commit();
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(T t, bool commit = true)
        {
            context.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            if (commit)
            {
                return unitof.Commit();
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(T t, bool commit = true)
        {
            if (t == null) return false;
            context.Remove(t);

            if (commit)
            {
                return unitof.Commit();
            }
            else
            {
                return false;
            }
        }


    }
}
