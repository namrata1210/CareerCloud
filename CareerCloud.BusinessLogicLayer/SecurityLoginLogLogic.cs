using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginLogLogic:BaseLogic<SecurityLoginsLogPoco>
    {
        public SecurityLoginLogLogic(IDataRepository<SecurityLoginsLogPoco>repository)
            :base(repository)
        {
                
        }
    }
}
