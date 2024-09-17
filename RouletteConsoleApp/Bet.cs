// Bet.cs
namespace RouletteConsoleApp
{
    public class Bet
    {
        public string Type { get; }
        public decimal Amount { get; }

        public Bet(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}