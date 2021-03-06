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
    public class SecurityRoleController : ApiController
    {

        private SecurityRoleLogic _logic;
        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>(false);
            _logic = new SecurityRoleLogic(repo);
        }
        [HttpGet]
        [Route("education/{SecurityRoleId}")]
        [ResponseType(typeof(SecurityRolePoco))]
        public IHttpActionResult GetSecurityRole(Guid SecurityRoleId)
        {
            SecurityRolePoco poco = _logic.Get(SecurityRoleId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<SecurityRolePoco>))]
        public IHttpActionResult GetallSecurityRole()
        {
            List<SecurityRolePoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostSecurityRole
            ([FromBody] SecurityRolePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutSecurityRole
            ([FromBody] SecurityRolePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteSecurityRole
            ([FromBody] SecurityRolePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
