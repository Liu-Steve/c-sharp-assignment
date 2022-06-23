using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper.Models;

//测试车辆
[Table("Buses")]
public class Bus
{
    [Key]
    public string BusId { get; set; }//车牌号

    public string RoadId { get; set; }//几号线

    public Road Road { get; set; }//路线

    public List<WorkInfo> Works { get; set; }//执行车次

    // public Bus(string busId, Road road)
    // {
    //     //BusID = Guid.NewGuid().ToString();
    //     BusId = busId;
    //     RoadId = road.RoadId;
    //     Road = road;
    //     Works = new List<WorkInfo>();
    // }
}
