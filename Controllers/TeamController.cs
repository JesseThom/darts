using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using darts.Models;
using Microsoft.EntityFrameworkCore;

namespace darts.Controllers;

public class TeamController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public TeamController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [SessionCheck]
        [HttpGet("/team/new")]
    public IActionResult TeamNew()
    {
        return View();
    }

        [HttpPost("/team/create")]
    public IActionResult TeamCreate(Team newTeam)
    {
        if(ModelState.IsValid)
        {
            newTeam.UserId = (int)HttpContext.Session.GetInt32("uuid");
            newTeam.TeamPoints = 0;
            _context.Add(newTeam);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }

        return View("TeamNew");
    }

    [SessionCheck]
        [HttpGet("/team/{id}")]
    public IActionResult TeamView(int id)
    {
        if(id != 0)
        {
            Team? OneTeam = _context.Teams
            .Include(i => i.TeamPlayers)
            .SingleOrDefault(i => i.TeamId == id);
            return View(OneTeam);
        }
        else
        {
            Team? OneTeam = _context.Teams
            .Include(i => i.TeamPlayers)
            .SingleOrDefault(i => i.TeamOwner.UserId == HttpContext.Session.GetInt32("uuid"));
            return View(OneTeam);
        }

    //     if(OneTeam == null){
    //         return RedirectToAction("Dashboard","User");
    //     }

    //     return View(OneTeam);
    }

        [HttpGet("/team/{id}/edit")]
    public IActionResult TeamEdit(int id)
    {
        Team? OneTeam = _context.Teams.SingleOrDefault(i => i.TeamId == id);

        if(OneTeam == null){
            return View("Dashboard", "User");
        }

        return View(OneTeam);
    }

        [HttpPost("/team/{id}/update")]
    public IActionResult TeamUpdate(Team newTeam, int id)
    {
        Team? OldTeam = _context.Teams.SingleOrDefault(i => i.TeamId == id);

        if(ModelState.IsValid && OldTeam != null)
        {
            OldTeam.TeamName = newTeam.TeamName;
          // add more attributes here if needed
            OldTeam.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

        return RedirectToAction("TeamView",new {id = id});

        }

            return View("TeamEdit",OldTeam);
    }

        [HttpPost("/team/{id}/delete")]
    public IActionResult TeamDelete(int id)
    {
        Team? TeamToDelete = _context.Teams.SingleOrDefault(i => i.TeamId ==id);
        if(TeamToDelete != null)
        {
        _context.Teams.Remove(TeamToDelete);
        _context.SaveChanges();

        return RedirectToAction("Index");
        }

        return View("Index");
    }
        [HttpGet("/disable")]
        public IActionResult Disable()
    {
        System.Console.WriteLine("*********diabled****");
        List<Team> AllTeams = _context.Teams.ToList();
        foreach (Team team in AllTeams)
        {
            team.TeamUpdate = 1;
            _context.SaveChanges();
        }
        return RedirectToAction("Dashboard", "User");
    }
    
    [SessionCheck]
    [HttpGet("/bracket")]
    public IActionResult Bracket()
    {
        return View();
    }
}