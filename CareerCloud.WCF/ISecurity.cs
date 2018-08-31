﻿using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.WCF
{
    interface ISecurity
    {
        [OperationContract]
        void AddSecurityLogin(SecurityLoginPoco[] pocos);
        [OperationContract]
        List<SecurityLoginPoco> GetAllSecurityLogin();
        [OperationContract]
        SecurityLoginPoco GetSingleSecurityLogin(Guid Id);
        [OperationContract]
        void RemoveSecurityLogin(SecurityLoginPoco[] pocos);
        [OperationContract]
        void UpdateSecurityLogin(SecurityLoginPoco[] pocos);
        [OperationContract]
        void AddSecurityLoginsLog(SecurityLoginsLogPoco[] pocos);
        [OperationContract]
        List<SecurityLoginsLogPoco> GetAllSecuritysLoginLog();
        [OperationContract]
        SecurityLoginsLogPoco GetSingleSecuritysLoginLog(Guid Id);
        [OperationContract]
        void RemoveSecurityLoginsLog(SecurityLoginsLogPoco[] pocos);
        [OperationContract]
        void UpdateSecurityLoginsLog(SecurityLoginsLogPoco[] pocos);
        [OperationContract]
        void AddSecurityLoginsRole(SecurityLoginsRolePoco[] pocos);
        [OperationContract]
        List<SecurityLoginsRolePoco> GetAllSecurityLoginsRole();
        [OperationContract]
        SecurityLoginsRolePoco GetSingleSecurityLoginsRole(Guid Id);
        [OperationContract]
        void RemoveSecurityLoginsRole(SecurityLoginsRolePoco[] pocos);
        [OperationContract]
        void UpdateSecurityLoginsRole(SecurityLoginsRolePoco[] pocos);
        [OperationContract]
        void AddSecurityRole(SecurityRolePoco[] pocos);
        [OperationContract]
        List<SecurityRolePoco> GetSecurityRole();
        [OperationContract]
        SecurityRolePoco GetSingleSecurityRole(Guid Id);
        [OperationContract]
        void RemoveSecurityRole(SecurityRolePoco[] pocos);
        [OperationContract]
        void UpdateSecurityRole(SecurityRolePoco[] pocos);
           
            
    }
}
