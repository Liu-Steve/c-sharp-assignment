namespace BusHelper;

public class Bus
{
    public string BusID { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public Bus()
    {
        BusID = Guid.NewGuid().ToString();
    }
}
