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

    public class CompanyLocationController : ApiController
    {

        private CompanyLocationLogic _logic;
        public CompanyLocationController()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>(false);
            _logic = new CompanyLocationLogic(repo);
        }
        [HttpGet]
        [Route("education/{CompanyLocationId}")]
        [ResponseType(typeof(CompanyLocationPoco))]
        public IHttpActionResult GetCompanyLocation(Guid CompanyLocationId)
        {
            CompanyLocationPoco poco = _logic.Get(CompanyLocationId);
            if (poco != null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<CompanyLocationPoco>))]
        public IHttpActionResult GetallCompanyLocation()
        {
            List<CompanyLocationPoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostCompanyLocation
            ([FromBody] CompanyLocationPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutCompanyLocation
            ([FromBody] CompanyLocationPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteCompanyLocation
            ([FromBody] CompanyLocationPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
