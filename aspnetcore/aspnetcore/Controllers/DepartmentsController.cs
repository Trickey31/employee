using Microsoft.AspNetCore.Mvc;
using aspnetcore.Application;

namespace aspnetcore
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseReadOnlyController<DepartmentDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
