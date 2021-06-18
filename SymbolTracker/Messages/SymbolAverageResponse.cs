using System;

namespace SymbolTracker.Messages
{
    public class SymbolAverageResponse
    {
        public SymbolAverageResponse(string symbolName, decimal average, DateTime date)
        {
            SymbolName = symbolName;
            Average = average;
            Date = date;
        }

        public string SymbolName { get; private set; }
        public decimal Average { get; private set; }
        public DateTime Date { get; private set; }
    }
}