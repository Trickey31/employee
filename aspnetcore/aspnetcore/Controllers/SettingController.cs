using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aspnetcore.Application;

namespace aspnetcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<string> GetFormat()
        {
            var response = await _settingService.GetFormat();

            return response;
        }

        [HttpPut]
        public async Task<string> UpdateFormat(string format)
        {
            var response = await _settingService.UpdateFormat(format);

            return response;
        }
    }
}
