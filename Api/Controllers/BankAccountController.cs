using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BankAccountController : Controller
{
    private readonly IBankAccountService _bankAccountService;

    public BankAccountController(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BankAccountDto>> Get(Guid id)
    {
        var account = await _bankAccountService.GetByIdAsync(id);
        if (account == null) return NotFound();
        return Ok(account);
    }
}
