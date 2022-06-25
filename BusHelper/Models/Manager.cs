using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//管理员信息
[Table("Managers")]
public class Manager
{
    [Key]
    public string ManagerId { get; set; }//管理员账号

    public string Pwd { get; set; }//密码

    public string? Area { get; set; }//所在地区

    public List<WorkInfo>? WorkInfos { get; set; }//执行车次
}

//已