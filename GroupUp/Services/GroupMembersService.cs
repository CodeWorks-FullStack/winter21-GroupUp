using System;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupMembersService
  {
    private readonly GroupMembersRepository _repo;
    private readonly GroupsRepository _groupsRepo;

    public GroupMembersService(GroupMembersRepository repo, GroupsRepository groupsRepo)
    {
      _repo = repo;
      _groupsRepo = groupsRepo;
    }

    internal GroupMember Create(GroupMember groupMember)
    {
      return _repo.Create(groupMember);
    }

    internal void Delete(int id, string userId)
    {
      GroupMember memberToDelete = _repo.GetById(id);
      if (memberToDelete == null)
      {
        throw new Exception("Member not found");
      }
      Group group = _groupsRepo.GetById(memberToDelete.GroupId);
      if (memberToDelete.AccountId != userId || group.CreatorId != userId)
      {
        throw new Exception("You are not authorized to delete this group member.");
      }
      _repo.Delete(id);
    }
  }
}