using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAMOW
{
    class MyException : Exception
    {
        public MyException(Exception exception, string message)
            : base(message + "\n\nSzczegóły błędu:\n" + exception.Message + "\n\n" + exception.StackTrace) { }
    }  
}
