using Microsoft.AspNetCore.Mvc;
using aspnetcore.Application;

namespace aspnetcore
{
    public class BaseController<TEntityDto, TCreateDto, TUpdateDto> : BaseReadOnlyController<TEntityDto>
    {
        #region Fields
        protected readonly IBaseService<TEntityDto, TCreateDto, TUpdateDto> BaseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntityDto, TCreateDto, TUpdateDto> baseService) : base(baseService)
        {
            BaseService = baseService;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Phương thức thêm bản ghi
        /// </summary>
        /// <param name="createDto">Thông tin bản ghi được thêm</param>
        /// <returns>bản ghi được thêm</returns>
        /// Created by: VTThanh (8/9/2023)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertAsync(TCreateDto createDto)
        {
            var response = await BaseService.InsertAsync(createDto);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Sửa thông tin một bản ghi theo Id
        /// </summary>
        /// <param name="updateDto">Thông tin bản ghi được cập nhật</param>
        /// <param name="id">Mã định danh</param>
        /// <returns>Nhân viên được cập nhật</returns>
        /// CreatedBy: VTThanh (14/9/2023)
        [HttpPut]
        [Route("{id}")]
        public async Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var response = await BaseService.UpdateAsync(id, updateDto);

            return response;
        }

        /// <summary>
        /// Phương thức xóa bản ghi
        /// </summary>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: VTThanh (8/9/2023)
        [HttpDelete]
        [Route("{id}")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var response = await BaseService.DeleteAsync(id);

            return response;
        }

        /// <summary>
        /// Phương thức xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: VTThanh (8/9/2023)
        [HttpDelete]
        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var response = await BaseService.DeleteManyAsync(ids);

            return response;
        }
        #endregion
    }
}
