using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupMembersService
  {
    private readonly GroupMembersRepository _repo;
    private readonly GroupsRepository _groupsRepo;
    private readonly AccountsRepository _accountsRepo;

    public GroupMembersService(GroupMembersRepository repo, GroupsRepository groupsRepo, AccountsRepository accountsRepo)
    {
      _repo = repo;
      _groupsRepo = groupsRepo;
      _accountsRepo = accountsRepo;
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

    internal List<ProfileGroupViewModel> GetMembers(int groupId)
    {
      return _accountsRepo.GetByGroupId(groupId);
    }

    internal List<GroupProfileViewModel> GetByAccountId(string id)
    {
      return _groupsRepo.GetGroupsByAccountId(id);
    }
  }
}