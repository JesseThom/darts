using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using darts.Models;

namespace darts.Controllers;

public class UserController : Controller
{
    static int? UserId;
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public UserController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [SessionCheck]
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        // User? OneUser = _context.Users.SingleOrDefault(i => i.UserId == UserId);
        
        List<Team> AllTeams =_context.Teams
            .Include(team => team.TeamOwner)
            .Include(i => i.TeamPlayers)
            .OrderByDescending(order =>order.TeamPoints)
            .ToList();

        return View(AllTeams);
    }

    [SessionCheck]
    [HttpGet("/draft")]
    public IActionResult Draft()
    {
        Team? MyTeam = _context.Teams
            .FirstOrDefault(team => team.UserId == (int)HttpContext.Session.GetInt32("uuid"));
            
        //redirects to dashboard if no team is created yet
        if (MyTeam is null)
        {
            return RedirectToAction("Dashboard");
        }

        MyViewModel DraftModel = new MyViewModel()
        {
            UsersTeam = MyTeam,
            
            AvailablePlayers = _context.Players
                .Where(team => team.TeamId == null)
                .OrderBy(o => o.FirstName)
                .ToList(),

            TeamPlayers = _context.Players
                .Include(i => i.PlayerTeam)
                .Where(team => team.TeamId == MyTeam.TeamId )
                .OrderBy(o => o.FirstName)
                .ToList()
        };

        return View(DraftModel);
    }

    [SessionCheck]
    [HttpGet("/scoring")]
    public IActionResult Scoring()
    {
        return View();
    }

    [HttpPost("/user/login")]
    public IActionResult UserLogin(Login newLogin)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        User? OneUser = _context.Users.SingleOrDefault(i => i.Name == newLogin.NameLogin);
        if(OneUser == null )
        {
            ModelState.AddModelError("NameLogin","Invalid Name/Password");
            return View("Index");
        }

        UserId = OneUser.UserId;
        PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
        var result = Hasher.VerifyHashedPassword(newLogin, OneUser.Password, newLogin.PasswordLogin);
        if (result == 0)
        {
            ModelState.AddModelError("NameLogin","Invalid Name/Password");
            return View("Index");
        }
        HttpContext.Session.SetInt32("uuid",OneUser.UserId);
        HttpContext.Session.SetString("name",OneUser.Name);
        System.Console.WriteLine(HttpContext.Session.GetInt32("uuid"));
        return RedirectToAction("Dashboard");
    }

    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("uuid");
        HttpContext.Session.Remove("name");
        return RedirectToAction("Index","Home");
    }

    [HttpPost("/user/create")]
    public IActionResult UserCreate(User newUser)
    {
        if(ModelState.IsValid)
        {
            User? OneUser = _context.Users.SingleOrDefault(i => i.Name == newUser.Name);
            if(OneUser != null)
            {
                ModelState.AddModelError("Name","Name already exhist");
                return View("Index");
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();

            UserId = newUser.UserId;
            HttpContext.Session.SetInt32("uuid",newUser.UserId);
            HttpContext.Session.SetString("name",newUser.Name);

            return RedirectToAction("Dashboard");
        }

        return View("Index");

    }

    [HttpGet("/draft/toggle")]
    public IActionResult DraftToggle()
    {

        List<Team> AllTeams = _context.Teams.ToList();
        foreach (Team team in AllTeams)
        {
            team.TeamUpdate = 1;
            _context.SaveChanges();
        }
        return RedirectToAction("Draft");
    }
    // [HttpGet("/user/{id}")]
    // public IActionResult UserView(int id)
    // {
    //     User? OneUser = _context.Users.SingleOrDefault(i => i.UserId == id);

    //     if(OneUser == null){
    //         return View("Index");
    //     }

    //     return View(OneUser);
    // }

    // [HttpGet("/user/{id}/edit")]
    // public IActionResult UserEdit(int id)
    // {
    //     User? OneUser = _context.Users.SingleOrDefault(i => i.UserId == id);

    //     if(OneUser == null){
    //         return View("Index");
    //     }

    //     return View(OneUser);
    // }

    // [HttpPost("/user/{id}/update")]
    // public IActionResult UserUpdate(User newUser, int id)
    // {
    //     User? OldUser = _context.Users.SingleOrDefault(i => i.UserId == id);

    //     if(ModelState.IsValid && OldUser != null)
    //     {
    //       // OldUser.FirstName = newUser.FirstName;
    //       // add more attributes here if needed
    //         OldUser.UpdatedAt = DateTime.Now;

    //         _context.SaveChanges();

    //     return RedirectToAction("UserView",new {id = id});

    //     }

    //         return View("UserEdit",OldUser);
    // }

    // [HttpPost("/user/{id}/delete")]
    // public IActionResult UserDelete(int id)
    // {
    //     User? UserToDelete = _context.Users.SingleOrDefault(i => i.UserId ==id);
    //     if(UserToDelete != null)
    //     {
    //     _context.Users.Remove(UserToDelete);
    //     _context.SaveChanges();

    //     return RedirectToAction("Index");
    //     }

    //     return View("Index");
    // }
}   