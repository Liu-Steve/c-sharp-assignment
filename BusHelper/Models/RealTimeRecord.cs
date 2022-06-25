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
    public string RecordId { get; set; }//记录号

    public double X { get; set; }//位置坐标经度

    public double Y { get; set; }//位置坐标纬度

    public DateTime Time { get; set; }//时间序号

    public string BusId { get; set; }//公交车牌

    public Bus? Bus { get; set; }//公交车

    public DangerAction DangerAction { get; set; }//当前可能的危险行为，八个行为分析

    public DangerIndex DangerIndex { get; set; }//当前的生理指标

    public string? RealPic { get; set; }//实时图像的名称
    
    public RealTimeRecord()
    {
        RecordId = Guid.NewGuid().ToString();
        Time = DateTime.Now;
    }
}