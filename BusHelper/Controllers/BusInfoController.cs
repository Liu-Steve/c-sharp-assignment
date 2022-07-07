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
    public IActionResult PostRealTimeData(RealTimeRecord realTimeRecord)
    {
        try{
            //调用API获取结果，解析json写入
            JObject json = (JObject)JsonConvert.DeserializeObject
            (DriverBehaviorAnalysis.driver_behavior("img/" + realTimeRecord.RealPic));
            DriverBehaviorAnalysis.parseJson(realTimeRecord, json);
            RealTimeService.addRealTime(realTimeRecord);
            return Ok("更新成功");
        }
        catch(FileNotFoundException ex)
        {
            return BadRequest("图片不存在");
        }
        catch(ArgumentNullException ex)
        {
            return BadRequest("被解析的json参数为空");
        }
        catch(JsonReaderException ex)
        {
            return BadRequest("被解析的json参数不是json格式");
        }
        catch(NullReferenceException ex)
        {
            return BadRequest("解析了json格式的参数，但不是期望的json结果，按照格式从中找不到需要的八个指标");
        }
    }

    //更新实时数据
    [HttpPost]
    // [Authorize]
    public IActionResult PostAudio(LeavingMsg msg)
    {
        LeavingMessageService.addLeavingMsg(msg);
        return Ok();
    }

    //查询道路信息
    [HttpGet]
    [Authorize]
    public string getAllRoads()
    {
        RoadService roadService = new RoadService();
        List<Road> list = roadService.getAllRoads();
        return JsonConvert.SerializeObject(list);
    }

    //获取某辆车的五个指标和八个行为分析
    [HttpPost]
    public IActionResult getRealTime([FromBody] string busId)
    {
        var realTimeInfo = RealTimeService.getRealTime(busId);
        return Ok(JsonConvert.SerializeObject(realTimeInfo));
    }

    //获取某辆车的累计异常行为记录
    [HttpPost]
    public IActionResult getRecord([FromBody]string busId)
    {
        var dangerRecord=RealTimeService.getRecord(busId);
        return Ok(JsonConvert.SerializeObject(dangerRecord));
    }

    //查询所有未读信息
    [HttpGet]
    public IEnumerable<AudioUnread> getUnreadAudio()
    {
        List<AudioUnread> list=LeavingMessageService.getUnreadAudio();
        return list;
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
    // [Authorize]
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
                var header = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = header.FileName.Trim('"');
                string ext = Path.GetExtension(fileName);
                if(ext == ".MP3")
                {
                    folderName = Path.Combine("audio");
                    pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                }
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
    [Authorize]
    public void postWorkInfo(WorkInfo workInfo)
    {
        WorkInfoService.addWorkInfo(workInfo);
    }

    //获取所有车辆当前的位置信息
    [HttpGet]
    [Authorize]
    public IActionResult getBusLocation()
    {
        //接收匿名对象
        ArrayList location = RealTimeService.getRealLocation();
        return Ok(JsonConvert.SerializeObject(location));
    }

    //返回图片
    [HttpPost]
    public String getRealPic([FromBody] string picId)
    {
        return DriverBehaviorAnalysis.getFileBase64("img/"+picId);
    }

    //获取未读的全部警告信息
    [HttpGet]
    [Authorize]
    public IEnumerable<Alert> GetUnreadAlerts()
    {
        List<Alert> list = AlertService.GetUnreadAlerts();
        return list;
    }

    //将未读标记为已读
    [HttpPost]
    public IActionResult MarkAlertRead([FromBody] string alertId)
    {
        bool success = AlertService.MarkAlertRead(alertId);
        if (success)
        {
            return Ok();
        }
        return NotFound("alert does not exist");
    }

}
