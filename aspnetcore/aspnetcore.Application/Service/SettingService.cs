using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public async Task<string> GetFormat()
        {
            var format = await _settingRepository.GetFormat();

            return format;
        }

        public async Task<string> UpdateFormat(string format)
        {
            var response = await _settingRepository.UpdateFormat(format);

            return response;

        }
    }
}
