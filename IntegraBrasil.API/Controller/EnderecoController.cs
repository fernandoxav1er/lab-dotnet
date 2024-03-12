using IntegraBrasil.API.Interfaces;
using IntegraBrasil.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegraBrasil.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnderecoController : ControllerBase
    {
        public readonly IEnderecoService _enderecoService;
        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet("buscar/{cep}")]
        public async Task<IActionResult> BuscarEndereco([FromRoute] string cep)
        {
            var response = await _enderecoService.BuscarEndereco(cep);

            if (response.CodigoHttp == HttpStatusCode.OK) 
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int) response.CodigoHttp, response.ErroRetorno);
            }
        }
    }
}
