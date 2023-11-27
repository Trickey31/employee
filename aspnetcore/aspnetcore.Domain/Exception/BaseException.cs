using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class BaseException
    {
        /// <summary>
        /// Các thuộc tính lỗi được trả về
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        #region Properties
        
        public int StatusCode { get; set; }
        public string? DevMessage { get; set; }
        public string? UserMessage { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public object? Errors { get; set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// Phương thức dùng để trả về dữ liệu dưới Json
        /// </summary>
        /// <returns>Json String</returns>
        /// Created by: VTThanh (10/9/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
