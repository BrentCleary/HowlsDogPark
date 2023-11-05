#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Adding DataAnnotations.Schema below allows for [NotMapped]
using System.ComponentModel.DataAnnotations.Schema;
namespace HowlsDogPark.Models;

public class User
{
  [Key]
  public int UserId {get;set;}

  [Required]
  public string FirstName {get;set;}

  [Required]
  public string LastName {get;set;}

  [Required]
  [EmailAddress]
  [UniqueEmail]
  public string Email {get;set;}

  [Required]
  [DataType(DataType.Password)]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  public string Password {get;set;}

  [NotMapped]
  [DataType(DataType.Password)]
  [Compare("Password")]
  public string PasswordConfirm {get;set;}

  public DateTime CreatedAt {get;set;} = DateTime.Now;
  public DateTime UpdatedAt {get;set;} = DateTime.Now;

  // List of Owned Dogs
  public List<Dog> AllDogs {get;set;} = new List<Dog>();
  public List<Boarding> AllBoardings {get;set;} = new List<Boarding>();

  public int? MembershipId { get; set; }
  public Membership? Membership { get; set; } = null;
}




// Below is a check in our database (_context) for any objects holding the entered email in their email field

public class UniqueEmailAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if(value == null)
    {
      return new ValidationResult("Email is required");
    }

    MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

    if(_context.Users.Any(e => e.Email == value.ToString()))
    {
      return new ValidationResult("Email already in use");
    }
    else
    {
      return ValidationResult.Success;
    }
  }
}
