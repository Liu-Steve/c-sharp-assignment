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
    public string? DriverID { get; set; }//员工号

    public string? Name { get; set; }//员工姓名

    public string? PhoneNumber { get; set; }//电话号码

    public string? CarLicense { get; set; }//车牌号，也即驾驶的车辆

    public string? FacePic{get;set;}//员工头像

    public Driver(string DriverID,string name,string phoneNumber,string carLicense,string facePic)
    {

    }
}