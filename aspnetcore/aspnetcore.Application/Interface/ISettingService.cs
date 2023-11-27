using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public interface ISettingService
    {
        Task<string> GetFormat();

        Task<string> UpdateFormat(string format);
    }
}
