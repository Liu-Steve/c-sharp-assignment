using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("Calls")]
public class Call
{
    [Key]
    public string CallId { get; set; }//通话ID

    public string? Content { get; set; }//通话内容文件名

    public DateTime StartTime { get; set; }//通话开始时间

    public int Length { get; set; }//通话时长

    public string WorkInfoId { get; set; }//通话车次Id

    public WorkInfo? WorkInfo { get; set; }//通话车次

    public Call()
    {
        CallId = Guid.NewGuid().ToString();
    }
}