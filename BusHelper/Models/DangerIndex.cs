using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("DangerIndex")]
public class DangerIndex
{
    [Key]
    public string RecordId { get; set; }//记录号

    public int HeartRate { get; set; }//心率

    public int HighBloodPressure { get; set; }//血压高

    public int LowBloodPressure { get; set; }//血压低

    public int Temperature { get; set; }//体温

    public int BloodOxygen { get; set; }//血氧

    public string RealTimeRecordId { get; set; }//导航属性

    public RealTimeRecord RealTimeRecord { get; set; }//导航属性

    public DangerIndex()
    {
        RecordId = Guid.NewGuid().ToString();
    }
}