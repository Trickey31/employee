using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public enum StatusCode
    {
        /// <summary>
        /// Conflict
        /// </summary>
        Conflict = 409,
        /// <summary>
        /// Bad Request
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Not Found
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Server Error
        /// </summary>
        ServerError = 500,
    }
}
