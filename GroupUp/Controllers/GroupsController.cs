using System;
using System.Collections.Generic;
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
  public class GroupsController : ControllerBase
  {
    private readonly GroupsService _gs;

    public GroupsController(GroupsService gs)
    {
      _gs = gs;
    }

    [HttpGet]
    public ActionResult<List<Group>> GetAll()
    {
      try
      {
        List<Group> groups = _gs.GetAll();
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Group> GetById(int id)
    {
      try
      {
        Group group = _gs.GetById(id);
        return Ok(group);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Group>> Create([FromBody] Group group)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        group.CreatorId = userInfo.Id;
        Group newGroup = _gs.Create(group);
        return Created($"/api/groups/{newGroup.Id}", newGroup);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> Update(int id, [FromBody] Group group)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        group.Id = id;
        group.CreatorId = userInfo.Id;
        Group updatedGroup = _gs.Update(group);
        return Ok(updatedGroup);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _gs.Delete(id, userInfo.Id);
        return Ok("Group was deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}