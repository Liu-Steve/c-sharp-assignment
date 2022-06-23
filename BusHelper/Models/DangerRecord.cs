using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("DangerRecords")]
public class DangerRecord
{
    [Key]
    public string? recordID { get; set; }//记录号

    public string? Smoke { get; set; }//抽烟

    public string? Yawn{get;set;}//打哈欠

    public string? SafetyBelt{get;set;}//安全带

    public string? LeavingSteering{get;set;}//双手离开方向盘

    public string? CloseEye{get;set;}//闭眼

    public string? UsingPhone{get;set;}//使用手机

    public string? LookAround{get;set;}//视角未看前方

    public string? Conflict{get;set;}//司机乘客冲突

    public DangerRecord()
    {

    }
}