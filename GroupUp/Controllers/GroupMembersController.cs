using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupUp.Models;
using GroupUp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupMembersController : ControllerBase
  {
    private readonly GroupMembersService _gms;

    public GroupMembersController(GroupMembersService gms)
    {
      _gms = gms;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<GroupMember>> Create([FromBody] GroupMember groupMember)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupMember.AccountId = userInfo.Id;
        GroupMember newGroupMember = _gms.Create(groupMember);
        return Created($"/api/groupmembers/{newGroupMember.Id}", newGroupMember);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _gms.Delete(id, userInfo.Id);
        return Ok("Group member deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}