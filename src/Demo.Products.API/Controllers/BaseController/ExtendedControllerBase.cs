using Demo.Products.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Xero.Products.API.Controllers.BaseController
{
    public class ExtendedControllerBase<LoggerT> : ControllerBase
    {
        private readonly ILogger<LoggerT> _logger;
        internal readonly ISender _sender;

        public ExtendedControllerBase(ILogger<LoggerT> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override BadRequestObjectResult BadRequest(object? error)
        {
            if (error is Failed failed)
            {
                _logger.LogError(failed.Exception, failed.Error);
            }

            return base.BadRequest(error);
        }
    }
}
