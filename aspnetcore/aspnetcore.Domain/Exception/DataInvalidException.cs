using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class DataInvalidException : Exception
    {
        public int StatusCode { get; set; }
        public DataInvalidException() { }
        public DataInvalidException(int statusCode)
        {
            StatusCode = statusCode;
        }
        public DataInvalidException(string message) : base(message) { }
        public DataInvalidException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
