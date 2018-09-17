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
    [RoutePrefix("api/careercloud/applicant/v1")]
    public class ApplicantResumeController : ApiController
    {
        private ApplicantResumeLogic _logic;
        public ApplicantResumeController()
        
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>(false);
            _logic = new ApplicantResumeLogic(repo);
        }
        [HttpGet]
        [Route("education/{ApplicantResumeId}")]
        [ResponseType(typeof(ApplicantResumePoco))]
        public IHttpActionResult GetApplicantResume(Guid ApplicantResumeId)
        {
            ApplicantResumePoco poco = _logic.Get(ApplicantResumeId);
            if (poco != null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<ApplicantResumePoco>))]
        public IHttpActionResult GetallApplicantResume()
        {
            List<ApplicantResumePoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostApplicantResume
            ([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutApplicantResume
            ([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteApplicantResume
            ([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

    }
}
    

