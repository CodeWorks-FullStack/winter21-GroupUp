using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using GroupUp.Interfaces;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class GroupMembersRepository : IRepository<GroupMember, int>
  {
    private readonly IDbConnection _db;

    public GroupMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    public GroupMember Create(GroupMember item)
    {
      string sql = @"
        INSERT INTO groupmembers 
        (groupId, accountId)
        VALUES
        (@GroupId, @AccountId);
        SELECT LAST_INSERT_ID();";
      var id = _db.ExecuteScalar<int>(sql, item);
      item.Id = id;
      return item;

    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM groupmembers WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    public List<GroupMember> GetAll()
    {
      throw new NotImplementedException();
    }

    public GroupMember GetById(int id)
    {
      string sql = "SELECT * FROM groupmembers WHERE id = @id";
      return _db.QueryFirstOrDefault<GroupMember>(sql, new { id });
    }

    public void Update(GroupMember item)
    {
      throw new NotImplementedException();
    }
  }
}