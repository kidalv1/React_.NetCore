using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
  public class Delete
  {
    public class Command : IRequest
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;

      public Handler(DataContext context)
      {
        _context = context;
      }
      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var activity = await  _context.Activities.FindAsync(request.Id);
        if (activity != null) _context.Activities.Remove(activity);
        var succes = await _context.SaveChangesAsync() > 0;
        if (succes) return Unit.Value;
        throw new Exception("Problem with saving");
      }
    }
  }
}
