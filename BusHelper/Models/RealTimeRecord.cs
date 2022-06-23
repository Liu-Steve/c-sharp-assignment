using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//实时信息
[Table("RealTimeRecords")]
public class RealTimeRecord
{
    [Key]
    public string? RecordID { get; set; }//记录号

    public double X { get; set; }//位置坐标X

    public double Y { get; set; }//位置坐标Y

    public DateTime Time { get; set; }//时间序号

    public Bus BusID{get;set;}//车牌号

    public DangerAction DangerAction { get; set; }//当前可能的危险行为，八个行为分析

    public DangerRecord DangerRecord { get; set; }//累计危险行为记录

    public string? HeartRate { get; set; }//心率

    public string? HighBloodPressure { get; set; }//血压高

    public string? LowBloodPressure { get; set; }//血压低

    public string? Temperature { get; set; }//体温

    public string? BloodOxygen { get; set; }//血氧

    public string? RealPic { get; set; }//实时图像的名称

    public RealTimeRecord()
    {

    }
}