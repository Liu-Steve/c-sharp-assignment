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
    public string BusID { get; set; }//车牌号

    public string? RoadID { get; set; }//几号线

    public Bus()
    {
        BusID = Guid.NewGuid().ToString();
    }
}
