using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class Department : BaseAudit, IEntity
    {
        #region Properties
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        #endregion


        #region Methods
        public Guid GetId()
        {
            return DepartmentId;
        }

        public void SetId(Guid id)
        {
            DepartmentId = id;
        }

        #endregion
    }
}
