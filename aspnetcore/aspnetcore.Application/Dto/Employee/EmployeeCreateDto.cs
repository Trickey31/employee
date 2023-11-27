using aspnetcore.Domain;
using aspnetcore.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public class EmployeeCreateDto
    {
        #region Properties

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeCodeMaxLength")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeCodeNotEmpty")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeNameMaxLength")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeNameNotEmpty")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public Gender Gender { get; set; }

        /// <summary>
        /// Số CCCD
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        [MaxLength(12, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_IdentityNumberMaxLength")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? IdentityAddress { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? PositionName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? BankAccount { get; set; }

        /// <summary>
        /// Ngân hàng
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        public string? BankBranch { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_DepartmentNotEmpty")]
        public Guid? DepartmentId { get; set; }


        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// Created by: VTThanh (10/9/2023)
        //public string? DepartmentName { get; set; }

        #endregion
    }
}
