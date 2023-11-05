#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HowlsDogPark.Models;

public class Membership
{
  [Key]
  public int MembershipId {get; set;}
  public string MembershipType {get; set;}
  public string Description {get; set;}
  public int Price {get; set;}
  public string Notes {get; set;}

  public DateTime CreatedAt {get;set;} = DateTime.Now;
  public DateTime UpdatedAt {get;set;} = DateTime.Now;

  public List<User> Members {get;set;} = new List<User>();

}