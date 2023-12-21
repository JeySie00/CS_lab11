using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pomelo;


public class Program
{
    async public static Task Main()
    {
        var tcpListener = new TcpListener(IPAddress.Any, 8888);

        tcpListener.Start();
        Console.WriteLine("Сервер запущен");
        try
        {
            while (true)
            {
                using var tcpClient = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"Установлено подключение: {tcpClient.Client.RemoteEndPoint}");
                var stream = tcpClient.GetStream();
                byte[] data = new byte[256];
                await stream.ReadAsync(data);
                string s = Encoding.UTF8.GetString(data);
                Console.WriteLine(s);
                using (var context = new StockContext())
                {
                    var ticker = context.Tickers.FirstOrDefault(t => t.TickerSymbol == s);
                    var price = context.Prices.FirstOrDefault(p => p.TickerId == ticker.Id).PriceAfter;
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(Convert.ToString(price)));
                }
            }
        }
        finally
        {
            tcpListener.Stop();
        }


    }
}

public class Ticker
{
    public int Id { get; set; }
    public string TickerSymbol { get; set; }
}

public class Price
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public double PriceBefore { get; set; }
    public double PriceAfter { get; set; }
    public DateTimeOffset DateBefore { get; set; }
    public DateTimeOffset DateAfter { get; set; }
}

public class TodayCondition
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public bool State { get; set; }
}

public class StockContext : DbContext
{
    public DbSet<Ticker> Tickers { get; set; }
    public DbSet<Price> Prices { get; set; }
    // public DbSet<TodayCondition> TodaysCondition { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=muputi64;database=tickers;",
            new MySqlServerVersion(new Version(8, 0, 25)));
    }
}