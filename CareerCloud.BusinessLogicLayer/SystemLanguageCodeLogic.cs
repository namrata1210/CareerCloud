using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemLanguageCodeLogic:SystemLanguageCodePoco
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco>repository)
        {

        }
        public void Add(SystemLanguageCodePoco[]pocos)
        {
            verify(pocos);
            Add(pocos);
        }
        public void Update(SystemLanguageCodePoco[]pocos)
        {
            verify(pocos);
            Update(pocos);
        }
        protected void verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            {
                foreach(SystemLanguageCodePoco poco in pocos)
                {
                    if(string.IsNullOrEmpty(poco.LanguageID))
                    {
                        exceptions.Add(new ValidationException(1000, $"Cannot be empty-{poco.LanguageID}"));
                    }
                    if(string.IsNullOrEmpty(poco.Name))
                    {
                        exceptions.Add(new ValidationException(1001, $"Cannot be empty-{poco.LanguageID}"));
                    }
                    if(string.IsNullOrEmpty(poco.NativeName))
                    {
                        exceptions.Add(new ValidationException(1002, $"Cannot be empty-{poco.LanguageID}"));
                    }
                }
                if(exceptions.Count>0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
