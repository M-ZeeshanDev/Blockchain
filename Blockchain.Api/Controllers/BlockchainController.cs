using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Services.Contracts;
using Blockchain.WebContracts;
using BlockMapper.WebContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blockchain.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.ControllerRoute)]
    public class BlockchainController : BlockchainBaseController
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainController(ILogger<BlockchainController> logger,
            IBlockchainService blockchainService) : base(logger)
        {
            _blockchainService = blockchainService;
        }

        [HttpGet("{id:long}", Name = "Blockchain-Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlockchainResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<BlockchainResponse> serviceResult = await _blockchainService.GetAsync(id);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }

        [HttpPost("Create", Name = "Blockchain-Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlockchainResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlockRequest createBlockRequest, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<BlockchainResponse> serviceResult = await _blockchainService.CreateAsync(createBlockRequest);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }
    }
}

