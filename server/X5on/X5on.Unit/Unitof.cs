using System;
using X5on.Data;

namespace X5on.Unit
{
    public class Unitof : IUnitof, IDisposable
    {
        private X5onContext context;

        public Unitof(X5onContext Context)
        {
            context = Context;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
