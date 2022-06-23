using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("Calls")]
public class Calls
{
    [Key]
    public string? CallID { get; set; }//通话ID

    public string? Content { get; set; }//通话内容

    public string? Manager{get;set;}//通话管理员

    public string? Time{get;set;}//通话时间

    public string? Driver{get;set;}//通话司机

    public string? Bus{get;set;}//通话车辆

    public Calls()
    {

    }
}