using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
  public  class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository)
            : base(repository)
        {

        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string[] websitesmustendwith = new string[] { ".ca", ".com", ".biz" };

            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – '.ca', '.com','.biz' -{ poco.Id }"));
                }

                else if (!websitesmustendwith.Any(t => poco.CompanyWebsite.Contains(t)))
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – '.ca', '.com','.biz' -{ poco.Id }"));
                }
                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)-{poco.Id}"));
                }
                else
                {
                    string[] validphonenumber = poco.ContactPhone.Split('-');
                    if (poco.ContactPhone.Length < 3)
                    {
                        exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)-{poco.Id}"));
                    }
                    else
                    {
                        if (validphonenumber[0].Length < 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)-{poco.Id}"));
                        }
                        else if (validphonenumber[1].Length < 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)-{poco.Id}"));
                        }
                        else if (validphonenumber[2].Length < 4)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)-{poco.Id}"));
                        }

                    }
                }
            }
            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
