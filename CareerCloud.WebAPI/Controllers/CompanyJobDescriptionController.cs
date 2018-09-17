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
    public class CompanyJobDescriptionController : ApiController
    {
        private CompanyJobDescriptionLogic _logic;
        public CompanyJobDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>(false);
            _logic = new CompanyJobDescriptionLogic(repo);
        }
        [HttpGet]
        [Route("education/{CompanyJobDescriptionId}")]
        [ResponseType(typeof(CompanyJobDescriptionPoco))]
        public IHttpActionResult GetCompanyJobDescription(Guid CompanyJobDescriptionId)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(CompanyJobDescriptionId);
            if (poco != null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<CompanyJobDescriptionPoco>))]
        public IHttpActionResult GetallCompanyJobDescription()
        {
            List<CompanyJobDescriptionPoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostCompanyJobDescription
            ([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutCompanyJobDescription
            ([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteCompanyJobDescription
            ([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
