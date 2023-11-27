using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public interface ISettingRepository
    {
        /// <summary>
        /// Phương thức lấy ra format
        /// </summary>
        /// <returns></returns>
        /// Created by: VTThanh (09/10/2023)
        Task<string> GetFormat();

        /// <summary>
        /// Phương thức cập nhật format
        /// </summary>
        /// <returns></returns>
        /// Created by: VTThanh (09/10/2023)
        Task<string> UpdateFormat(string newFormat);
    }
}
