using Base.EndPoints.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseTemplate.Endpoints.API.People;

[Route("api/[controller]")]
public class People(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("Create")]
    public IActionResult CreatePerson() => Ok();
}
