using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupUp.Interfaces;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class GroupsRepository : IRepository<Group, int>
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Group> GetAll()
    {
      string sql = @"
      SELECT 
        g.*,
        a.* 
      FROM groups g
      JOIN accounts a ON a.id = g.creatorId";

      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
      {
        g.Creator = p;
        return g;
      }).ToList();
    }

    public Group GetById(int id)
    {
      string sql = @"
      SELECT 
        g.*,
        a.* 
      FROM groups g
      JOIN accounts a ON a.id = g.creatorId
      WHERE g.id = @id";

      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
      {
        g.Creator = p;
        return g;
      }, new { id }).FirstOrDefault();
    }

    public Group Create(Group group)
    {
      string sql = @"
      INSERT INTO groups
      (name, description, imgUrl, creatorId)
      VALUES
      (@Name, @Description, @ImgUrl, @CreatorId);
      SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, group);
      group.Id = id;
      return group;
    }

    public void Update(Group group)
    {
      string sql = @"
      UPDATE groups
      SET
        name = @Name,
        description = @Description,
        imgUrl = @ImgUrl
      WHERE id = @Id";

      _db.Execute(sql, group);
    }

    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM groups
      WHERE id = @id
      LIMIT 1";

      _db.Execute(sql, new { id });
    }
  }
}