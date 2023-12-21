namespace Task1.source;

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