using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        public async Task<IActionResult> SendRequestAsync<T>(IRequest<Result<T, Error>> request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(request);
            return result.Match(
                value => Ok(value),
                error => error.Key switch
                {
                    ErrorsEnum.UserNotFound => NotFound(error.Key + ": " + error.Description),
                    ErrorsEnum.Forbidden => Forbid(error.Key + ": " + error.Description),
                    _ => (IActionResult)BadRequest(error.Key + ": " + error.Description)
                });
        }
        
    }
}