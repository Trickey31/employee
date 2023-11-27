using Microsoft.AspNetCore.Mvc;
using aspnetcore.Application;

namespace aspnetcore
{
    public class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        protected readonly IBaseReadOnlyService<TEntityDto> BaseReadOnlyService;

        public BaseReadOnlyController(IBaseReadOnlyService<TEntityDto> baseReadOnlyService)
        {
            BaseReadOnlyService = baseReadOnlyService;
        }


        ///// <summary>
        ///// Phương thức lấy danh sách các nhân viên
        ///// </summary>
        ///// <returns>Danh sách nhân viên</returns>
        ///// Created by: VTThanh (8/9/2023)
        [HttpGet]
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var response = await BaseReadOnlyService.GetAllAsync();

            return response;
        }


        /// <summary>
        /// Phương thức lấy nhân viên theo id
        /// </summary>
        /// <returns>Nhân viên</returns>
        /// Created by: VTThanh (8/9/2023)
        [HttpGet]
        [Route("{id}")]
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var response = await BaseReadOnlyService.GetAsync(id);

            return response;
        }
    }
}
