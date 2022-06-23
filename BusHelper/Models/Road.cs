using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//道路信息
[Table("Roads")]
public class Road
{
    [Key]
    public string? RoadID { get; set; }//几号线

    public string? RoadInfo { get; set; }//路线的具体路径，为一串json坐标

    public Road()
    {
        
    }
}