using Microsoft.AspNetCore.Mvc;
using Roster.Utilities;

namespace Roster.Controllers
{
    [Route("api/[controller]")]
    public class RosterController : Controller
    {
        //// GET api/roster
        [HttpGet]
        public IActionResult GetAll()
        {
			string filter = string.Empty;
            return CloudTableHelper.GetWithFilter(filter);
		}

        //// GET api/roster/current
        [HttpGet("current")]
        public IActionResult GetCurrentRoster()
        {
			string filter = "DateLastPaid ge datetime'2017-01-01T00:00:00.000Z' or WebsiteJoinDate ge datetime'2017-01-01T00:00:00.000Z'";
            return CloudTableHelper.GetWithFilter(filter);
		}

        //// GET api/roster/lastname/smith
        [HttpGet("lastName/{lastName}")]
        public IActionResult GetByLastName(string lastName)
        {
            string filter = $"LastName eq '{lastName}'";
            return CloudTableHelper.GetWithFilter(filter);
        }

        //// GET api/roster/email/test@test.com
        [HttpGet("email/{emailAdress}")]
        public IActionResult GetByEmail(string emailAdress)
        {
	        string filter = $"Email eq '{emailAdress}' or SecondaryEmail eq '{emailAdress}'";
            return CloudTableHelper.GetWithFilter(filter);
        }

        //// GET api/roster/ushpa/5
        [HttpGet("ushpa/{ushpa}")]
        public IActionResult GetByUshpa(int ushpa)
        {
	        string filter = $"USHPA eq '{ushpa}'";
            return CloudTableHelper.GetWithFilter(filter);
        }

        //// POST api/roster
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/roster/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/roster/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
