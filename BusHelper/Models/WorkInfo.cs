using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//公交车次信息
[Table("WorkInfos")]
public class WorkInfo
{
    [Key]
    public string WorkId { get; set; }//车次号，自动生成uuid

    public DateTime StartTime { get; set; }//开始时间

    public DateTime? EndTime { get; set; }//结束时间

    public string BusId { get; set; }//车牌号

    public Bus? Bus { get; set; }//执行公交

    public string ManagerId { get; set; }//此时值班的管理员Id

    public Manager? Manager { get; set; }//管理员

    public string DriverId { get; set; }//此时值班的司机Id

    public Driver? Driver { get; set; }//司机

    public DangerRecord DangerRecord { get; set; }//危险操作次数统计

    public List<Call>? Calls { get; set; }//通话记录

    public List<LeavingMsg>? LeavingMsgs { get; set; }//留言记录

    public List<Alert>? Alerts { get; set; }//警告记录

    public WorkInfo()
    {
        WorkId = Guid.NewGuid().ToString();
        StartTime = DateTime.Now;
        EndTime = null;
    }
}