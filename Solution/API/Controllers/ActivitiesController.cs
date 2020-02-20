using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Activities;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
      private readonly IMediator _mediator;
      private readonly Repo _repo;

      public ActivitiesController(IMediator mediator, IRepo repo)
      {
        _repo = (Repo) repo;
        _mediator = mediator;
      }

        // GET: api/Activies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> Get(int page = 0 , int pageSize = 5)
        {
          //return await _mediator.Send(new List.Query());

          var ac = await _repo.FindPaged<Activity>(page, pageSize);
          return Ok(ac);

        }

        // GET: api/Activies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
          return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST: api/Activies
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
          return await _mediator.Send(command);
        }

        // PUT: api/Activies/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id , Edit.Command command)
        {
          command.Id = id;
          return await _mediator.Send(command);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
          return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}
