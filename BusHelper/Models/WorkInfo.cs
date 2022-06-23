using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//公交车信息
[Table("WorkInfos")]
public class WorkInfo
{
    [Key]
    public string? BusID{get;set;}//车牌号

    public string? Weekday { get; set; }//星期

    public DateTime StartTime { get; set; }//开始时间

    public DateTime EndTime{get;set;}//结束时间

    public string? ManagerID{get;set;}//此时值班的管理员

    public string? DriverID{get;set;}//此时值班的司机

    public WorkInfo()
    {

    }
}