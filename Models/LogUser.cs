#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace HowlsDogPark.Models;

public class LogUser
{
  [Required(ErrorMessage = "Email is Required")]
  [EmailAddress]
  [Display(Name = "Email")]
  public string LEmail {get;set;}

  [Required(ErrorMessage = "Password is Required")]
  [DataType(DataType.Password)]
  [Display(Name = "Password")]
  public string LPassword {get;set;}

}