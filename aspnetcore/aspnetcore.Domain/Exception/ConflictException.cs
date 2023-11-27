using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    // khi mà trùng mã => http trả về conflict (409)
    public class ConflictException : Exception
    {
        public int StatusCode { get; set; }
        public ConflictException() { }
        public ConflictException(int statusCode) 
        {
            StatusCode = statusCode;
        }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, int statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }

    }
}
