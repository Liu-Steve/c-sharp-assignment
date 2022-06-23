using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//某车次危险行为次数记录
[Table("DangerRecords")]
public class DangerRecord
{
    [Key]
    public string RecordId { get; set; }//记录号

    public int Smoke { get; set; }//抽烟

    public int Yawn { get; set; }//打哈欠

    public int SafetyBelt { get; set; }//安全带

    public int LeavingSteering { get; set; }//双手离开方向盘

    public int CloseEye { get; set; }//闭眼

    public int UsingPhone { get; set; }//使用手机

    public int LookAround { get; set; }//视角未看前方

    public int Conflict { get; set; }//司机乘客冲突

    public string WorkId { get; set; }//车次Id

    public WorkInfo Work { get; set; }//车次

    public DangerRecord()
    {
        RecordId = Guid.NewGuid().ToString();
        Smoke = 0;
        Yawn = 0;
        SafetyBelt = 0;
        LeavingSteering = 0;
        CloseEye = 0;
        UsingPhone = 0;
        LookAround = 0;
        Conflict = 0;
    }
}