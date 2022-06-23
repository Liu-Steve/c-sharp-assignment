using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("LeavingMsgs")]
public class LeavingMsgs
{
    [Key]
    public string? MsgID { get; set; }//留言ID

    public string? Content { get; set; }//留言内容

    public string? IfRead{get;set;}//是否已读

    public string? Time{get;set;}//留言时间

    public string? Driver{get;set;}//来自司机

    public string? Bus{get;set;}//来自车辆

    public LeavingMsgs()
    {

    }
}