﻿using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.WCF
{
    interface ISystem
    {
        [OperationContract]
        void AddSystemCountryCode(SystemCountryCodePoco[] pocos);
        [OperationContract]
        List<SystemCountryCodePoco> GetAllSystemCountryCode();
        [OperationContract]
        SystemCountryCodePoco GetSingleSystemCountryCode(Guid Id);
        [OperationContract]
        void RemoveSystemCountryCode(SystemCountryCodePoco[] pocos);
        [OperationContract]
        void UpdateSystemCountryCode(SystemCountryCodePoco[] pocos);
        [OperationContract]
        void AddSystemLanguageCode(SystemLanguageCodePoco[] pocos);
        [OperationContract]
        List<SystemLanguageCodePoco> GetAllSystemLanguageCode();
        [OperationContract]
        SystemLanguageCodePoco GetSingleSystemLanguageCode(Guid Id);
        [OperationContract]
        void RemoveSystemLanguageCode(SystemLanguageCodePoco[]pocos);
        [OperationContract]
        void UpdateSystemLanguageCode(SystemLanguageCodePoco[] pocos);

    }
}
