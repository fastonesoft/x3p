using System;
using System.Collections.Generic;
using System.Text;

namespace X5on.Res
{
    public  interface IRespos<T> where T:class 
    {
        bool Add(T t, bool commit = true);
        
        bool Update(T t, bool commit = true);

        bool Delete(T t, bool commit = true);
    }
}
