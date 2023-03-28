using System.IO;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using darts.Models;

namespace darts.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        
        // string JsonContent = System.IO.File.ReadAllText(@"./update.json");
        // var week = JsonSerializer.Deserialize<Week>(JsonContent);
        // Console.WriteLine($"Week: {week?.WeekNumber}");

        // List<PlayerJson> AllPlayers = new List<PlayerJson>();  
        //     using (StreamReader r = new StreamReader(@"./stats.json"))  
        //     {  
        //         string json = r.ReadToEnd();  
        //         AllPlayers = JsonSerializer.Deserialize<List<PlayerJson>>(json);  
        //     }
        int? UserId = HttpContext.Session.GetInt32("uuid");
        if (UserId == null)
        {
            return View();
        }
            return RedirectToAction("Dashboard","User");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
