using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Interfaces.Handler
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command, CancellationToken cancellationToken = default);
    }
}
