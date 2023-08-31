using System.IO.Ports;
using System.Text;
using HW4SW;
using Microsoft.Extensions.Configuration;
using NATS.Client;

var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();
Configure configure = config.GetSection("Configure").Get<Configure>() ?? new();

if (args.Length is 1 && args[0] == "nats")
{
    Options opts = ConnectionFactory.GetDefaultOptions();
    opts.Url = configure.NatsUrl;
    using IConnection connection = new ConnectionFactory().CreateConnection(opts);
    ISyncSubscription subSync = connection.SubscribeSync(configure.Tittle);
    while (true)
    {
        try
        {
            Msg m = subSync.NextMessage(1000);
            string text = Encoding.UTF8.GetString(m.Data);
            Console.WriteLine($"Received: '{text}' from '{m.Subject}'");
            m = subSync.NextMessage(100);
        }

        catch (NATSTimeoutException)
        {
            Console.WriteLine("No new messages");
        }
        Console.WriteLine("Wait 1 sec");
        await Task.Delay(1000);
    }
}


if (args.Length is 0)
{
    using SerialPort comPort = new
    (
        configure.ComPortName,
        configure.BaudRate,
        configure.Parity,
        configure.DataBits,
        configure.StopBits
    );

    try
    {
        comPort.Open();
        Options opts = ConnectionFactory.GetDefaultOptions();
        opts.Url = configure.NatsUrl;

        using IConnection connection = new ConnectionFactory().CreateConnection(opts);
        Console.WriteLine("Read data from COM port and send to NATS");

        List<char> chars = new();
        while (true)
        {
            int read = comPort.ReadByte();
            if (read != -1)
            {
                char c = (char)read;
                switch (c)
                {
                    case '\r':
                    {
                        if (chars.Count > 0)
                        {
                            string source = string.Join(string.Empty, chars);
                            Console.WriteLine($"send message: {source}");
                            byte[] bytes = Encoding.UTF8.GetBytes(source);
                            connection.Publish(configure.Tittle, bytes);
                            chars = new();
                        }
                        break;
                    }
                    case '\n': break;
                    default: chars.Add(c); break;
                }
            }
            await Task.Delay(1000);
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    finally { comPort.Close(); }
}