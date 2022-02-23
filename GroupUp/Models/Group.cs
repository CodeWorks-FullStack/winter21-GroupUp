using System;
using GroupUp.Interfaces;

namespace GroupUp.Models
{
  public class Group : IRepoItem<int>, ICreated
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatorId { get; set; }
    public string ImgUrl { get; set; }
    public Profile Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
  // used by get all groups for account
  public class GroupProfileViewModel : Group
  {
    public int GroupMemberId { get; set; }
  }
}