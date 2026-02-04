using Microsoft.AspNetCore.Mvc;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Exceptions;

[ApiController]
[Route("contacorrente")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("movimentacao")]
    public async Task<IActionResult> Movimentacao([FromBody] MovimentoRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { type = ex.Code, message = ex.Message });
        }
    }

    [HttpGet("{idContaCorrente}/saldo")]
    public async Task<IActionResult> Saldo([FromRoute] string idContaCorrente)
    {
        try
        {
            var response = await _mediator.Send(new SaldoRequest { IdContaCorrente = idContaCorrente });
            return Ok(response);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { type = ex.Code, message = ex.Message });
        }
    }
}
