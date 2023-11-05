using System.Net;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using HowlsDogPark.Models;


namespace HowlsDogPark.Controllers;

public class BoardingController : Controller
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

  public BoardingController(ILogger<UserController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }

    // ------- Boarding ROUTES -----------
  // ----- Create Boarding (FORM)

  [SessionCheck]
  [HttpGet("/boarding/new")]
  public IActionResult NewBoarding()
  {

    // VIEWBAG - Passing Logged Users Dogs as Dictionary
    User? currentUser = _context.Users.Include(d => d.AllDogs).FirstOrDefault(u => u.UserId == userId); 

    List<Dog> userDogList = currentUser.AllDogs;

    Dictionary<string, int> dogDictionary = new Dictionary<string, int>();
    foreach(Dog dog in userDogList)
    {
      dogDictionary.Add(dog.Name, dog.DogId);
    }

    ViewBag.dogDictionary = dogDictionary;

    return View("BoardingCreate");
  }

    // ----- Create Boarding (POST)

  [HttpPost("/boarding/create")]
  public IActionResult CreateBoarding(Boarding newBoarding)
  {
    if(ModelState.IsValid)
    {
      _context.Add(newBoarding);
      _context.SaveChanges();

      return RedirectToAction("ShowUser", "User", new {id = userId });
    }
    else
    {
      return View("BoardingCreate");
    }
  }


  // TODO: Boarding Edit
  // Create Boarding Edit View
  [HttpGet("/boarding/{id}/edit")]
  public IActionResult EditBoarding(int id)
  {
    // Boarding to Edit
    Boarding? oneBoarding = _context.Boardings.FirstOrDefault(b => b.BoardingId == id);

    // VIEWBAG - Passing Logged Users Dogs as Dictionary
    User? currentUser = _context.Users.Include(d => d.AllDogs).FirstOrDefault(u => u.UserId == userId);

    List<Dog> userDogList = currentUser.AllDogs;

    Dictionary<string, int> dogDictionary = new Dictionary<string, int>();
    foreach(Dog dog in userDogList)
    {
      dogDictionary.Add(dog.Name, dog.DogId);
    }

    ViewBag.dogDictionary = dogDictionary;

    return View("BoardingEdit", oneBoarding);
  }

  // Boarding Edit Post
  [HttpPost("/boarding/{id}/update")]
  public IActionResult UpdateBoarding(int id, Boarding newBoarding)
  {

    Boarding? oldBoarding = _context.Boardings.FirstOrDefault(b => b.BoardingId == id);

    if(ModelState.IsValid)
    {
      oldBoarding.Date = newBoarding.Date;
      oldBoarding.Time = newBoarding.Time;
      oldBoarding.Kennel = newBoarding.Kennel;
      oldBoarding.Notes = newBoarding.Notes;
      oldBoarding.DogId = newBoarding.DogId;

      _context.SaveChanges();
    
      return RedirectToAction("ShowUser", "User", new {id = userId}); 
    }
    else
    {
      return View("BoardingEdit", oldBoarding);
    }

  }

  // Boarding Delete
  [HttpPost("boarding/{id}/delete")]
  public IActionResult BoardingDelete(int id)
  {
    Boarding? boardingtoDelete = _context.Boardings.SingleOrDefault(b => b.BoardingId == id);
    _context.Boardings.Remove(boardingtoDelete);
    _context.SaveChanges();

    return RedirectToAction("ShowUser", "User", new {id = userId});
  }




  public IActionResult Privacy()
  {
      return View();
  }



}