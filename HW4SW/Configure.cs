using System.IO.Ports;

namespace HW4SW;

public class Configure
{
    public string ComPortName { get; set; } = "COM2";
    public int BaudRate { get; set; } = 9600;
    public int DataBits { get; set; } = 8;
    public Parity Parity { get; set; } = Parity.None;
    public StopBits StopBits { get; set; } = StopBits.One;
    public string NatsUrl { get; set; } = "nats://localhost:4222";
    public string Tittle { get; set; } = "bytes";
}