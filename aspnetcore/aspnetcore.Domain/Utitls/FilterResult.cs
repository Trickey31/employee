using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class FilterResult<Employee>
    {
        #region Fields
        public List<Employee> Results { get; set; }
        public int TotalCount { get; set; }

        #endregion
    }
}
