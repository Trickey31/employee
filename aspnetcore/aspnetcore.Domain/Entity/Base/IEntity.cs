using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public interface IEntity
    {
        /// <summary>
        /// Phương thức lấy id
        /// </summary>
        /// <returns></returns>
        public Guid GetId();

        /// <summary>
        /// Phương thức đặt id
        /// </summary>
        /// <param name="id">id</param>
        public void SetId(Guid id);
    }
}
