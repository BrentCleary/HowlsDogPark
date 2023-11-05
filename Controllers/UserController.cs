using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using HowlsDogPark.Models;


namespace HowlsDogPark.Controllers;

public class UserController : Controller
{
  private readonly ILogger<UserController> _logger;
  private MyContext _context;

  public UserController(ILogger<UserController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }

  // ------- USER ROUTES -----------
  // ----- Create User (FORM)

  [HttpGet("/users/new")]
  public IActionResult NewUser()
  {

    return View("~/Views/User/UserCreateLogin.cshtml");
  }

  // ----- Create User (POST)

  [HttpPost("/users/create")]
  public IActionResult CreateUser(User newUser)
  {
    if(ModelState.IsValid)
    {
      PasswordHasher<User> Hasher = new PasswordHasher<User>();
      newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
      _context.Add(newUser);
      _context.SaveChanges();

      HttpContext.Session.SetInt32("UserId", newUser.UserId);
      
      return RedirectToAction("Index", "Home");
    
    }
    else
    {
      return View("UserCreateLogin");
    }
  }



  // ----- View One User (VIEW)

  [HttpGet("/users/{id}/show")]
  public IActionResult ShowUser(int id)
  {
    System.Console.WriteLine("+_+_+_+_+_+_+_+_" + id + " +_+_+_+_+_+_+_+_");

    User? currentUser = _context.Users.Include(d => d.AllDogs).Include(b => b.AllBoardings).Include(m => m.Membership).FirstOrDefault(u => u.UserId == id); 

    System.Console.WriteLine("------------" + HttpContext.Session.GetInt32("UserId") + "------------");

    return View("UserProfile", currentUser);
    
  }



  // ----- All Users (VIEW)

  [SessionCheck]
  [HttpGet("/users")]
  public IActionResult UserIndex()
  {

    List<User> AllUsers = _context.Users.ToList();
    // Session Check in Console

    System.Console.WriteLine("------------" + HttpContext.Session.GetInt32("UserId") + "------------");


    return View("AllUsers", AllUsers);
  }




  // ----- User Login (POST)

  [HttpPost("/users/login")]
  public IActionResult LoginUser(LogUser loginUser)
  {
    if(ModelState.IsValid)
    {
      User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LEmail);
      if(userInDb == null)
      {
        ModelState.AddModelError("LEmail", "Invalid Email/Password");
        return View("UserCreateLogin");
      }
      PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
      var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LPassword);
      if(result == 0)
      {
        ModelState.AddModelError("LEmail", "Invalid Email/Password");
        return View("UserCreateLogin");
      }
      else
      {
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        // Session Check in Console
        System.Console.WriteLine("------------" + HttpContext.Session.GetInt32("UserId") + "------------");


        return RedirectToAction("Index", "Home");
      }
    }
    else
    {
      return View("UserCreateLogin");
    }
  }


  // ----- User LogOut (VIEW)

  [HttpGet("/users/logout")]
  public IActionResult LogoutUser()
  {
    HttpContext.Session.Remove("UserId");

    return RedirectToAction("Index", "Home");
  }



  public IActionResult Privacy()
  {
      return View();
  }

}





    // Session Check for logged in User

    // Apply [SessionCheck] to all routes user must be logged in to view

    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? userId = context.HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                context.Result = new RedirectToActionResult("NewUser", "User", null);
            }
        }
    }

