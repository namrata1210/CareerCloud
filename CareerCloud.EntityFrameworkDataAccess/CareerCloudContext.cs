using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext:DbContext
    {
        public CareerCloudContext():base("dbconnection")
        {

        }
    }
}
