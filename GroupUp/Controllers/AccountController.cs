using System;
using System.Threading.Tasks;
using GroupUp.Models;
using GroupUp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly GroupMembersService _gs;

    public AccountController(AccountService accountService, GroupMembersService gs)
    {
      _accountService = accountService;
      _gs = gs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("groups")]
    [Authorize]
    public async Task<ActionResult<List<GroupProfileViewModel>>> GetGroupsByAccountId()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<GroupProfileViewModel> groups = _gs.GetByAccountId(userInfo.Id);
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}