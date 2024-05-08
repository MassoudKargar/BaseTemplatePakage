using Base.EndPoints.Web.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseTemplate.Endpoints.API.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class People : BaseController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePerson() => Ok();
    }
}
