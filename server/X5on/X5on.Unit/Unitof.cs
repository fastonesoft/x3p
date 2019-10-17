using Microsoft.EntityFrameworkCore;
using System;

namespace X5on.Unit
{
    public class Unitof:IUnitof, IDisposable
    {
        private DbContext context;

        public Unitof(DbContext Context)
        {
            context = Context;
        }

        public bool Commit()
        {
            return context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            if(context != null)
            {
                context.Dispose();
            }
        }
    }
}
