using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.IdentityModel.Tokens.Jwt;
using BusHelper.Models;
using BusHelper.Service;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace BusHelper.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BusInfoController : ControllerBase
{
    private static List<Bus> buses = new List<Bus>();

    private readonly ILogger<BusInfoController> _logger;

    private readonly BusService _service;

    public BusInfoController(ILogger<BusInfoController> logger, 
        IConfiguration configuration)
    {
        _logger = logger;
        _service = new BusService(configuration);
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
    public IActionResult Login(Manager manager)
    {
        var token = _service.CreateToken(manager.ManagerId);
        return Ok(token);
    }

    [HttpPost]
    [Authorize]
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

    //更新实时数据
    [HttpPost]
    // [Authorize]
    public void PostRealTimeData(RealTimeRecord realTimeRecord)
    {
        //调用API获取结果，解析json写入
        JObject json=(JObject)JsonConvert.DeserializeObject
            (DriverBehaviorAnalysis.driver_behavior("img/"+realTimeRecord.RealPic));
        DriverBehaviorAnalysis.parseJson(realTimeRecord,json);
        RealTimeService.addRealTime(realTimeRecord);
    }


    //查询道路信息
    [HttpGet]
    public IActionResult getAllRoads()
    {
        RoadService roadService=new RoadService();
        List<Road> list=roadService.getAllRoads();
        if(list.Count!=0)
        {
            return Ok(JsonConvert.SerializeObject(list));
        }
        else
        {
            return BadRequest("");
        }
    }

    //获取某辆车的实时信息
    [HttpPost]
    public IActionResult getRealTime(string busId)
    {
        ArrayList list=RealTimeService.getRealTime(busId);
        return Ok(JsonConvert.SerializeObject(list));
    }
}
