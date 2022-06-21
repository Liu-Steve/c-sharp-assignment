using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusHelper;

[Table("Buses")]
public class Bus
{
    [Key]
    public string BusID { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public Bus()
    {
        BusID = Guid.NewGuid().ToString();
    }
}
