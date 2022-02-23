using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _repo;

    public GroupsService(GroupsRepository repo)
    {
      _repo = repo;
    }

    internal List<Group> GetAll()
    {
      return _repo.GetAll();
    }

    internal Group GetById(int id)
    {
      Group group = _repo.GetById(id);
      if (group == null)
      {
        throw new Exception("Invalid Id");
      }
      return group;
    }

    internal Group Create(Group group)
    {
      return _repo.Create(group);
    }

    internal Group Update(Group group)
    {
      Group original = GetById(group.Id);
      if (original.CreatorId != group.CreatorId)
      {
        throw new Exception("You are not the creator of this group");
      }
      original.Name = group.Name != null ? group.Name : original.Name;
      original.Description = group.Description != null ? group.Description : original.Description;
      original.ImgUrl = group.ImgUrl != null ? group.ImgUrl : original.ImgUrl;
      _repo.Update(original);
      return original;
    }

    internal void Delete(int id, string userId)
    {
      Group group = GetById(id);
      if (group.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this group");
      }
      _repo.Delete(id);
    }
  }
}