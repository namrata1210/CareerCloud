using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginRoleLogic:BaseLogic<SecurityLoginsRolePoco>
    {
        public SecurityLoginRoleLogic(IDataRepository<SecurityLoginsRolePoco>repository)
            :base(repository)
        {

        }
    }
}
