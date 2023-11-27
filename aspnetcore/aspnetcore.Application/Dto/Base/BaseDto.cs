using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public abstract class BaseDto
    {
        /// <summary>
        /// Định danh
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        //public Guid CreatorId { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public DateTime? ModifiedDate { get; set; }
    }
}
