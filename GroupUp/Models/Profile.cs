using System;
using GroupUp.Interfaces;

namespace GroupUp.Models
{
  public class Profile : IRepoItem<string>
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  // used by get all members in group
  public class ProfileGroupViewModel : Profile
  {
    // NOTE This is how we establish a connection to the relationship model
    // everything a profile is plus the one relationship id
    public int GroupMemberId { get; set; }
  }
}