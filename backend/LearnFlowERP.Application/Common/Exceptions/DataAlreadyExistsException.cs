using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Common.Exceptions
{
    public class DataAlreadyExistsException : Exception
    {
        public DataAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
