using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//指标报警信息
[Table("Alert")]
public class Alert
{
    [Key]
    public string AlertId { get; set; }//警告Id

    public int Level { get; set; }//警告级别

    public string Content { get; set; }//内容

    public DateTime Time { get; set; }//警告时间

    public bool IsRead { get; set; }//是否已读

    public string WorkInfoId { get; set; }//警告车次Id

    public WorkInfo? WorkInfo { get; set; }//警告车次

    public Alert()
    {
        AlertId = Guid.NewGuid().ToString();
        Content = "";
        Time = DateTime.Now;
        IsRead = false;
    }
}