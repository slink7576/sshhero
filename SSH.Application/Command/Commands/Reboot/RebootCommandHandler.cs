using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.Reboot
{
    public class RebootCommandHandler : IRequestHandler<RebootCommand, bool>
    {
        public async Task<bool> Handle(RebootCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    return client.Reboot();
                }
            }
            catch(Exception c)
            {
                return false;
            }
        }
    }
}
