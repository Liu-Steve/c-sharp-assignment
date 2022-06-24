using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using BusHelper.Models;

namespace BusHelper.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BusInfoController : ControllerBase
{
    private static List<Bus> buses = new List<Bus>();

    private readonly ILogger<BusInfoController> _logger;

    private readonly IConfiguration _configuration;

    public BusInfoController(ILogger<BusInfoController> logger, 
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
/*
    [HttpPost]
    public IActionResult AddBus(JObject obj)
    {
        double? newX = (double?)obj["x"];
        double? newY = (double?)obj["y"]; 
        if(newX == null)
            return BadRequest("Need x in post");
        if(newY == null)
            return BadRequest("Need y in post");
        Bus newBus = new Bus(){
            // X = (double)newX,
            // Y = (double)newY
        };
        buses.Add(newBus);
        return Ok(newBus.BusID);
    }

    [HttpPost]
    public IActionResult UpdateBus(JObject obj)
    {
        string? busID = (string?)obj["bus_id"];
        double? newX = (double?)obj["x"];
        double? newY = (double?)obj["y"]; 
        if(busID == null)
            return BadRequest("Need bus_id in Post");
        if(newX == null)
            return BadRequest("Need x in post");
        if(newY == null)
            return BadRequest("Need y in post");
        Bus? updateBus = buses.Find(bus => bus.BusID == busID);
        if(updateBus == null)
            return BadRequest("No bus matched is found");
        // updateBus.X = (double)newX;
        // updateBus.Y = (double)newY;
        return Ok();
    }

    [HttpGet]
    public IEnumerable<Bus> GetBusPos()
    {
        return buses.ToArray();
    }
*/
    [HttpPost]
    public IActionResult Test()
    {
        Driver? driver;
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            // var newBus = new Bus(){
            //     X = 15,
            //     Y = 20
            // };
            // context.Buses.Add(bus);
            // context.SaveChanges();
            driver = context.Drivers.FirstOrDefault();
        }
        if(driver != null)
            return Ok(JsonConvert.SerializeObject(driver));
        else
            return BadRequest();
    }
}
