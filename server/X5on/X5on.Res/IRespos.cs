using System;
using System.Collections.Generic;
using System.Text;

namespace X5on.Res
{
    public  interface IRespos<T> where T:class 
    {
        int Add(T t, bool commit = false);

        int Update(T t, bool commit = false);

        int Delete(T t, bool commit = false);
    }
}
