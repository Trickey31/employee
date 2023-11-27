using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public class DepartmentDto : BaseDto
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
