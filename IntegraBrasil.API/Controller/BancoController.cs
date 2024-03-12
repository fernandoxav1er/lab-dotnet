using IntegraBrasil.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IntegraBrasil.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _bancoService;

        public BancoController(IBancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet("/buscarTodosBancos")]
        [SwaggerOperation(Summary = "Retorna todos os bancos brasileiros", Description = "Get")]
        public async Task<IActionResult> BuscarTodosBancos()
        {
            var response = await _bancoService.BuscarTodosBancos();

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }
        }

        [HttpGet("/buscarBanco{codigoBanco}")]
        [SwaggerOperation(Summary = "Retorna apenas um banco conforme o código informado", Description = "Get")]
        public async Task<IActionResult> BuscarBanco([RegularExpression("^[0-9]*$")] string codigoBanco)
        {
            var response = await _bancoService.BuscarBanco(codigoBanco);

            if (response.CodigoHttp == HttpStatusCode.OK)
                return Ok(response.DadosRetorno);
            else
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
        }
    }
}