﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    class ApplicantEducationLogic:BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco>repository)
            :base(repository)
        {}
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }


        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantEducationPoco Poco in pocos)
            {

                if(string.IsNullOrEmpty(Poco.Major))
                {
                    exceptions.Add(new ValidationException(107,
                    $"Cannot be empty or less than 3 characters-{Poco.Id}"));
                }
                else if(Poco.Major.Length<3)
                {
                    exceptions.Add(new ValidationException(107,
                    $"Cannot be empty or less than 3 characters-{Poco.Id}"));

                }
                if(Poco.StartDate>DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108,
                        $"Cannot be greater than today-{Poco.Id}"));
                }
                if(Poco.CompletionDate<Poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109,
                        $"CompletionDate cannot be earlier than StartDate-{Poco.Id}"));

                }
            }
            if (exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }

        }
    }
}
