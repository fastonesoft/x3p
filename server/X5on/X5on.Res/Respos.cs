using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using X5on.Data;
using X5on.Unit;

namespace X5on.Res
{
    public abstract class Respos<T> : IRespos<T> where T : class
    {
        private Unitof _unitof;
        private X5onContext _context;

        public Respos(Unitof unitof)
        {
            _unitof = unitof;
            _context = _unitof.x5onContext;
        }

        public virtual int Add(T t, bool commit = false)
        {

            _context.Add(t);
            return commit ? _unitof.Commit() : 0;
        }

        public virtual int Update(T t, bool commit = false)
        {
            _context.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
            return commit ? _unitof.Commit() : 0;
        }

        public virtual int Delete(T t, bool commit = false)
        {
            if (t == null) return 0;
            _context.Remove(t);

            return commit ? _unitof.Commit() : 0;
        }


    }
}
