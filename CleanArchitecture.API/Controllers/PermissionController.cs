using CleanArchitecture.Application.Features.Perminissions.Commands.CreatePermission;
using CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision;
using CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList;
using CleanArchitecture.Application.Models.Kafka;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermissionController : Controller
    {
        private IMediator _mediator;
        private ILogger<PermissionController> _logger;
        private readonly IProducer<Null, string> _producer;
        public PermissionController(IMediator mediator,ILogger<PermissionController> logger, IProducer<Null, string> producer)
        {
            _mediator = mediator;
            _logger = logger;
            _producer = producer;
        }

        [HttpPost("RequestPermission")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreatePermission([FromBody] CreatePermissionCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Running Endpoint Request Permission");
            var messageOperation = new OperationDto()
            {
                Id = Guid.NewGuid(),
                NameOperation = "Request operation"
            };
            await _producer.ProduceAsync("operation", new Message<Null, string> { Value = JsonSerializer.Serialize(messageOperation) });
            return result;
        }

        [HttpPut("ModifyPermission")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePermission([FromBody] UpdatePermissionCommand command)
        {
            await _mediator.Send(command);
            _logger.LogInformation("Running Endpoint Modify Permission");
            var messageOperation = new OperationDto()
            {
                Id = Guid.NewGuid(),
                NameOperation = "Modify operation"
            };
            await _producer.ProduceAsync("operation", new Message<Null, string> { Value = JsonSerializer.Serialize(messageOperation) });
            return NoContent();
        }

        [HttpGet("GetPermissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PermissionVM>>> GetPermissionsList()
        {
            var query = new GetPermissionQuery();
            var videos = await _mediator.Send(query);
            _logger.LogInformation("Running Endpoint GetPermissions");
            var messageOperation = new OperationDto()
            {
                Id = Guid.NewGuid(),
                NameOperation = "Get operation"
            };
            await _producer.ProduceAsync("operation", new Message<Null, string> { Value = JsonSerializer.Serialize(messageOperation) });
            return Ok(videos);
        }
    }
}
