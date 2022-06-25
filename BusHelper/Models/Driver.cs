using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//司机信息
[Table("Drivers")]
public class Driver
{
    [Key]
    public string DriverId { get; set; }//员工号

    public string Name { get; set; }//员工姓名

    public string? PhoneNumber { get; set; }//电话号码

    public string? FacePic { get; set; }//员工头像文件名

    public List<WorkInfo>? WorkInfos { get; set; }//执行车次
}