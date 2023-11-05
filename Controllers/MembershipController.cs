using System.Net;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using HowlsDogPark.Models;


namespace HowlsDogPark.Controllers;

public class MembershipController : Controller
{
  private int? userId
  {
    get
    {
      return HttpContext.Session.GetInt32("UserId");
    }
  }

  private readonly ILogger<UserController> _logger;
  private MyContext _context;

  public MembershipController(ILogger<UserController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }

    // ------- Membership ROUTES -----------
  // ----- Create Membership (FORM)

  [SessionCheck]
  [HttpGet("/membership/new")]
  public IActionResult NewMembership()
  {



    return View("CreateMembership");
  }

    // ----- Create Membership (POST)

  [HttpPost("/membership/create")]
  public IActionResult CreateMembership(Membership newMembership)
  {
    if(ModelState.IsValid)
    {
      _context.Add(newMembership);
      _context.SaveChanges();

      return RedirectToAction("ShowUser", "User", new {id = userId });
    }
    else
    {
      return View("CreateMembership");
    }
  }




  // USER MEMBERSHIP JOIN PAGE
  [HttpGet("/membership/join")]
  public IActionResult UserMembershipShow()
  {

    List<Membership> AllMemberships = _context.Memberships.ToList();


    return View("UserJoinMembership", AllMemberships);
  }


  // USER MEMBERSHIP JOIN ACTION

  [HttpPost("/membership/{id}/submit")]
  public IActionResult UserMembershipSubmit(int id)
  {
    User? currentUser = _context.Users.FirstOrDefault(u => u.UserId == userId);

    currentUser.MembershipId = id;
    _context.SaveChanges();
    

    return RedirectToAction("ShowUser", "User", new {id = userId});
  }






  // Create Membership Edit View
  [HttpGet("/membership/{id}/edit")]
  public IActionResult EditMembership(int id)
  {


    return View("EditMembership");
  }

  // Membership Edit Post
  [HttpPost("/membership/{id}/update")]
  public IActionResult UpdateMembership(int id, Membership newMembership)
  {
    if(ModelState.IsValid)
    {

    
      return RedirectToAction("ShowUser", "User", new {id = userId}); 
    }
    else
    {
      return View("EditMembership");
    }

  }

  // Membership Delete
  [HttpPost("membership/{id}/delete")]
  public IActionResult MembershipDelete(int id)
  {


    return RedirectToAction("ShowUser", "User", new {id = userId});
  }




  public IActionResult Privacy()
  {
      return View();
  }



}