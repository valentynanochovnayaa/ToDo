using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        public async Task<IActionResult> SendRequestAsync<T>(IRequest<Result<T, ErrorsEnum>> request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(request);
            return Ok(result.Value); /*.Match(
                value => Ok(value),
                error => error switch
                {
                    ErrorsEnum.UserNotFound => NotFound(),
                    ErrorsEnum.Forbidden => Forbid(),
                    _ => (IActionResult)BadRequest()
                });*/
        }
        
    }
}