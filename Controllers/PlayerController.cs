using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using darts.Models;
using Microsoft.EntityFrameworkCore;

namespace darts.Controllers;

public class PlayerController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public PlayerController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/update")]
    public IActionResult Update()
    {
        //reads and parses all json information
        List<PlayerJson> AllPlayers = new List<PlayerJson>();
        using (StreamReader r = new StreamReader(@"./stats.json"))
        {
            string json = r.ReadToEnd();
            AllPlayers = JsonSerializer.Deserialize<List<PlayerJson>>(json);
        }

        List<Player> ExhistingPlayers = _context.Players.ToList();

        foreach (PlayerJson player in AllPlayers)
        {
            Player ePlayer = ExhistingPlayers.FirstOrDefault(p => p.FirstName == player.FirstName && p.LastName == player.LastName);
            if (ePlayer != null)
            {
                UpdatePlayer(player, ePlayer);
                UpdatePoints(player, ePlayer);
            }
            else
            {
                AddPlayer(player);
                Player oldplayer = _context.Players.FirstOrDefault(p => p.FirstName == player.FirstName && p.LastName == player.LastName);
                UpdatePoints(player, oldplayer);
                // TeamUpdate();
            }
        }
        TeamUpdate();
        //enables team update
        List<Team> AllTeams = _context.Teams.ToList();
        foreach (Team team in AllTeams)
        {
            team.TeamUpdate = 0;
            _context.SaveChanges();
        }
        return RedirectToAction("Dashboard", "User");
        // return View();
    }

    void UpdatePoints(PlayerJson newStats, Player oldStats)
    {
        int tempPoints = 0;
        tempPoints += newStats.Hat * 2;
        tempPoints += newStats.HTon * 3;
        tempPoints += newStats.LTon;
        tempPoints += newStats.Whrse * 5;
        tempPoints += newStats._9MR * 4;
        tempPoints += newStats._8MR * 3;
        tempPoints += newStats._7MR * 2;
        tempPoints += newStats._6MR * 2;
        tempPoints += newStats._5MR;
        oldStats.PlayerPoints = tempPoints;

        _context.SaveChanges();
    }

    void TeamUpdate()
    {
        List<Team> AllTeams = _context.Teams.ToList();
        foreach (Team team in AllTeams)
            {
                Team? OneTeam = _context.Teams
                .Include(i => i.TeamPlayers)
                .SingleOrDefault(i => i.TeamId == team.TeamId);
                System.Console.WriteLine(OneTeam.TeamPlayers.Sum(i => i.PlayerPoints));
                team.TeamPoints = OneTeam.TeamPlayers.Sum(i => i.PlayerPoints);
                _context.SaveChanges();
            }
    }

    void UpdatePlayer(PlayerJson newPlayer, Player oldPlayer)
    {
        oldPlayer.Hat += newPlayer.Hat;
        oldPlayer.HTon += newPlayer.HTon;
        oldPlayer.LTon += newPlayer.LTon;
        oldPlayer.Whrse += newPlayer.Whrse;
        oldPlayer._9MR += newPlayer._9MR;
        oldPlayer._8MR += newPlayer._8MR;
        oldPlayer._7MR += newPlayer._7MR;
        oldPlayer._6MR += newPlayer._6MR;
        oldPlayer._5MR += newPlayer._5MR;
        oldPlayer.UpdatedAt = DateTime.Now;

        _context.SaveChanges();

    }

    void AddPlayer(PlayerJson player)
    {
        Player newPlayer = new Player()
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                HTon = player.HTon,
                LTon = player.LTon,
                Hat = player.Hat,
                Whrse = player.Whrse,
                _9MR = player._9MR,
                _8MR = player._8MR,
                _7MR = player._7MR,
                _6MR = player._6MR,
                _5MR = player._5MR,
                PlayerPoints = 0
            };
        _context.Add(newPlayer);
        _context.SaveChanges();
    }


    // [HttpPost("/player/create")]
    // public IActionResult PlayerCreate(Player newPlayer)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _context.Add(newPlayer);
    //         _context.SaveChanges();
    //         return RedirectToAction("Index");
    //     }

    //     return View("PlayerNew");
    // }
    [SessionCheck]
    [HttpGet("/player/{id}")]
    public IActionResult PlayerView(int id)
    {
        Player? OnePlayer = _context.Players.SingleOrDefault(i => i.PlayerId == id);

        List<PlayerJson> AllPlayers = new List<PlayerJson>();
        using (StreamReader r = new StreamReader(@"./stats.json"))
        {
            string json = r.ReadToEnd();
            AllPlayers = JsonSerializer.Deserialize<List<PlayerJson>>(json);
        }

        PlayerModel ThePlayer = new PlayerModel()
        {
            playerDb = OnePlayer,
            playerJson = AllPlayers.FirstOrDefault(f => f.FirstName == OnePlayer.FirstName && f.LastName == OnePlayer.LastName)
        };

        if (OnePlayer == null)
        {
            return View("Index");
        }

        return View(ThePlayer);
    }

    // [HttpGet("/player/{id}/edit")]
    // public IActionResult PlayerEdit(int id)
    // {
    //     Player? OnePlayer = _context.Players.SingleOrDefault(i => i.PlayerId == id);

    //     if (OnePlayer == null)
    //     {
    //         return View("Index");
    //     }

    //     return View(OnePlayer);
    // }


//using to add player to team
    [HttpPost("/player/{id}/update")]
    public IActionResult PlayerUpdate(Player newPlayer, int id)
    {
        Player? OldPlayer = _context.Players.SingleOrDefault(i => i.PlayerId == id);
        Team? MyTeam = _context.Teams.FirstOrDefault(team => team.UserId == (int)HttpContext.Session.GetInt32("uuid"));

        if (OldPlayer != null)
        {
            OldPlayer.TeamId = MyTeam?.TeamId;
            // add more attributes here if needed
            OldPlayer.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Draft", "User");

        }

        return RedirectToAction("Draft", "User");
    }

    [HttpPost("/player/{id}/delete")]
    public IActionResult PlayerDelete(int id)
    {
        Player? PlayerToDelete = _context.Players.SingleOrDefault(i => i.PlayerId == id);
        if (PlayerToDelete != null)
        {
            _context.Players.Remove(PlayerToDelete);
            _context.SaveChanges();

            return RedirectToAction("Draft", "User");
        }

        return RedirectToAction("Draft", "User");
    }

    [HttpPost("/player/{id}/remove")]
    public IActionResult PlayerRemove(int id)
    {
        Player? PlayerToRemove = _context.Players.SingleOrDefault(i => i.PlayerId == id);
        if (PlayerToRemove != null)
        {
            PlayerToRemove.TeamId = null;
            _context.SaveChanges();

            return RedirectToAction("TeamView", "Team");
        }

        return RedirectToAction("TeamView", "Team");
    }
}