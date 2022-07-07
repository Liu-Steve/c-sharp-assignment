using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//留言信息
[Table("ManagerMsgs")]
public class ManagerMsg
{
    [Key]
    public string MsgId { get; set; }//留言ID

    public string? Content { get; set; }//留言内容文件名

    public bool IsRead { get; set; }//是否已读

    public DateTime Time { get; set; }//留言时间

    public string BusId { get; set; }//留言车牌

    public Bus? Bus { get; set; }//留言车牌

    public ManagerMsg()
    {
        MsgId = Guid.NewGuid().ToString();
        IsRead = false;
        Time = DateTime.Now;
    }
}