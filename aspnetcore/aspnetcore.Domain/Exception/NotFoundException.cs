using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public NotFoundException() { }
        public NotFoundException(int statusCode)
        {
            StatusCode = statusCode;
        }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
