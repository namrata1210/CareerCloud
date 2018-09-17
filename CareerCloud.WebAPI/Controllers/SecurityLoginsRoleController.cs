﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CareerCloud.WebAPI.Controllers
{
    [RoutePrefix("api/careercloud/security/v1")]
    public class SecurityLoginsRoleController : ApiController
    {
        private SecurityLoginsRoleLogic _logic;
        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>(false);
        _logic = new SecurityLoginsRoleLogic(repo);
    }
    [HttpGet]
    [Route("education/{SecurityLoginsRoleId}")]
    [ResponseType(typeof(SecurityLoginsRolePoco))]
    public IHttpActionResult GetSecurityLoginsRole(Guid SecurityLoginsRoleId)
    {
            SecurityLoginsRolePoco poco = _logic.Get(SecurityLoginsRoleId);
        if (poco != null)
        {
            return NotFound();
        }
        return Ok(poco);
    }
    [HttpGet]
    [Route("education")]
    [ResponseType(typeof(List<SecurityLoginsRolePoco>))]
    public IHttpActionResult GetallSecurityLoginsRole()
    {
        List<SecurityLoginsRolePoco> result = _logic.GetAll();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost]
    [Route("education")]
    public IHttpActionResult PostSecurityLoginsRole
        ([FromBody] SecurityLoginsRolePoco[] pocos)
    {
        _logic.Add(pocos);
        return Ok();
    }
    [HttpPut]
    [Route("education")]
    public IHttpActionResult PutSecurityLoginsRole
        ([FromBody] SecurityLoginsRolePoco[] pocos)
    {
        _logic.Update(pocos);
        return Ok();
    }
    [HttpDelete]
    [Route("education")]
    public IHttpActionResult DeleteSecurityLoginsRole
        ([FromBody] SecurityLoginsRolePoco[] pocos)
    {
        _logic.Delete(pocos);
        return Ok();
    }
}
}
