#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace HowlsDogPark.Models;

public class Boarding
{
  [Key]
  public int BoardingId {get;set;}

  public DateTime Date {get;set;}
  public DateTime Time {get;set;}
  public int Kennel {get;set;}
  public string Notes {get;set;}
  
  public DateTime CreatedAt {get;set;}
  public DateTime UpdatedAt {get;set;}
  
  public int UserId { get; set; }
  public User? Requester { get; set; }
  
  public int DogId { get; set; }
  public Dog? RequesterDog { get;set; }


}