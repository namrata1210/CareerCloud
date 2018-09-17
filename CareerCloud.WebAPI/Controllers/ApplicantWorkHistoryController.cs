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
    public class ApplicantWorkHistoryController : ApiController
    {
        private ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryController()
        {
            var repo = new EFGenericRepository<ApplicantWorkHistoryPoco>(false);
            _logic = new ApplicantWorkHistoryLogic(repo);
        }
        [HttpGet]
        [Route("education/{ApplicantWorkHistoryId}")]
        [ResponseType(typeof(ApplicantWorkHistoryPoco))]
        public IHttpActionResult GetApplicantWorkHistory(Guid ApplicantWorkHistoryId)
        {
            ApplicantWorkHistoryPoco poco = _logic.Get(ApplicantWorkHistoryId);
            if (poco != null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("education")]
        [ResponseType(typeof(List<ApplicantWorkHistoryPoco>))]
        public IHttpActionResult GetallApplicantWorkHistory()
        {
            List<ApplicantWorkHistoryPoco> result = _logic.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("education")]
        public IHttpActionResult PostApplicantWorkHistory
            ([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }
        [HttpPut]
        [Route("education")]
        public IHttpActionResult PutApplicantWorkHistory
            ([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }
        [HttpDelete]
        [Route("education")]
        public IHttpActionResult DeleteApplicantWorkHistory
            ([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

    }
}
