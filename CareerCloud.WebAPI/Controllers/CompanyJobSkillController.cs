using CareerCloud.BusinessLogicLayer;
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
    [RoutePrefix("api/careercloud/company/v1")]
    public class CompanyJobSkillController : ApiController
    {
        private CompanyJobSkillLogic _logic;
        public CompanyJobSkillController()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>(false);
            _logic = new CompanyJobSkillLogic(repo);
        }
        [HttpGet]
        [Route("education/{CompanyJobSkillId}")]
        [ResponseType(typeof(CompanyJobSkillPoco))]
        public IHttpActionResult GetCompanyJobSkill(Guid CompanyJobSkillId)
        {
            CompanyJobSkillPoco poco = _logic.Get(CompanyJobSkillId);
            if (poco != null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<CompanyJobSkillPoco>))]
        public IHttpActionResult GetallCompanyJobSkill()
        {
            List<CompanyJobSkillPoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostCompanyJobSkill
            ([FromBody] CompanyJobSkillPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutCompanyJobSkill
            ([FromBody] CompanyJobSkillPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteCompanyJobSkill
            ([FromBody] CompanyJobSkillPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

    }
}
