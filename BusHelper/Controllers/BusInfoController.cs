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
using System.Web;
using System.Net.Http.Headers;

namespace BusHelper.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BusInfoController : ControllerBase
{
    private static List<Bus> buses = new List<Bus>();

    private readonly ILogger<BusInfoController> _logger;

    private readonly AuthService _service;

    public BusInfoController(ILogger<BusInfoController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _service = new AuthService(configuration);
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
        if (_service.Vaild(manager))
        {
            var token = _service.CreateToken(manager.ManagerId);
            return Ok(token);
        }
        else
            return BadRequest("Authorization failed! Manager is not exist.");
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
        if (driver != null)
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
        JObject json = (JObject)JsonConvert.DeserializeObject
            (DriverBehaviorAnalysis.driver_behavior("img/" + realTimeRecord.RealPic));
        DriverBehaviorAnalysis.parseJson(realTimeRecord, json);
        RealTimeService.addRealTime(realTimeRecord);
    }


    //查询道路信息
    [HttpGet]
    public IActionResult getAllRoads()
    {
        RoadService roadService = new RoadService();
        List<Road> list = roadService.getAllRoads();
        if (list.Count != 0)
        {
            return Ok(JsonConvert.SerializeObject(list));
        }
        else
        {
            return BadRequest("");
        }
    }

    //获取某辆车的五个指标和八个行为分析，以及八个累计行为记录
    [HttpPost]
    public IActionResult getRealTime([FromBody] string busId)
    {
        var realTimeInfo = RealTimeService.getRealTime(busId);
        return Ok(JsonConvert.SerializeObject(realTimeInfo));
    }

    //获取某辆车的司机信息
    [HttpPost]
    public IActionResult getBusInfo([FromBody] string busId)
    {
        //接收匿名对象
        var busInfo = RealTimeService.getBusInfo(busId);
        return Ok(JsonConvert.SerializeObject(busInfo));
    }

    //上传文件
    [HttpPost, DisableRequestSizeLimit]
    [Authorize]
    public async Task<IActionResult> Upload()
    {
        try
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            var folderName = Path.Combine("img");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    //司机打卡，添加工作信息
    [HttpPost]
    public void postWorkInfo(WorkInfo workInfo)
    {
        WorkInfoService.addWorkInfo(workInfo);
    }

    //获取所有车辆当前的位置信息
    [HttpGet]
    public IActionResult getBusLocation()
    {
        //接收匿名对象
        ArrayList location = RealTimeService.getRealLocation();
        return Ok(JsonConvert.SerializeObject(location));
    }
}
