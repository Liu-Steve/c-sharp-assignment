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
    public string RoadId { get; set; }//几号线

    public string RoadInfo { get; set; }//路线的具体信息存储的Json文件名

    public List<Bus> Buses { get; set; }//线路的所有的公交

    public Road(string roadId, string roadInfo)
    {
        RoadId = roadId;
        RoadInfo = roadInfo;
    }
}