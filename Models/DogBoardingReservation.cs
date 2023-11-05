#pragma warning disable CS8618
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
namespace HowlsDogPark.Models;

// Joining Table
public class DogBoardingReservation
{
  [Key]
  public int UserBoardingReservationId {get;set;}

  public DateTime CreatedAt {get;set;} = DateTime.Now;
  public DateTime UpdatedAt {get;set;} = DateTime.Now;

  public int DogId {get;set;}
  public int BoardingId {get;set;}
  
  // Navigation Properties
  public Dog? ReservedDog {get;set;}
  public Boarding? ReservedBoarding {get;set;}
}