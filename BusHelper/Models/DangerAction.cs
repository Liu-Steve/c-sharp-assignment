using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//实时行为危险分析
[Table("DangerActions")]
public class DangerAction
{
    [Key]
    public string RecordId { get; set; }//记录号

    public bool Smoke { get; set; }//抽烟

    public bool Yawn { get; set; }//打哈欠

    public bool NoSafetyBelt { get; set; }//未系安全带

    public bool LeavingSteering { get; set; }//双手离开方向盘

    public bool CloseEye { get; set; }//闭眼

    public bool UsingPhone { get; set; }//使用手机

    public bool LookAround { get; set; }//视角未看前方

    public bool Conflict { get; set; }//司机乘客冲突

    public string RealTimeRecordId { get; set; }//导航属性

    public RealTimeRecord RealTimeRecord { get; set; }//导航属性

    public DangerAction()
    {
        RecordId = Guid.NewGuid().ToString();
        Smoke = false;
        Yawn = false;
        NoSafetyBelt = false;
        LeavingSteering = false;
        CloseEye = false;
        UsingPhone = false;
        LookAround = false;
        Conflict = false;
    }
}