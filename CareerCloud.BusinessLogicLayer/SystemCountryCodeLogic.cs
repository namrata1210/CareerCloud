﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    class SystemCountryCodeLogic:SystemCountryCodePoco
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco>repository)
        {

        }
        public void Add(SystemCountryCodePoco[]pocos)
        {
            Verify(pocos);
            Add(pocos);
        }
        public void Update(SystemCountryCodePoco[]pocos)
        {
            Verify(pocos);
            Update(pocos);
        }
        protected void Verify(SystemCountryCodePoco[]pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            {
                foreach(SystemCountryCodePoco poco in pocos)
                {
                    if(string.IsNullOrEmpty(poco.Code))
                    {
                        exceptions.Add(new ValidationException(900, $"Cannot be empty-{poco.Code}"));
                    }
                    if(string.IsNullOrEmpty(poco.Name))
                    {
                        exceptions.Add(new ValidationException(900, $"Cannot be empty-{poco.Code}"));
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
