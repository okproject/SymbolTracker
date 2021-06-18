namespace SymbolTracker.Messages
{
    public class RegisterSymbolRequest
    {
        public RegisterSymbolRequest(string stockSymbol)
        {
            StockSymbol = stockSymbol;
        }

        public string StockSymbol { get; private set; }
    }
}