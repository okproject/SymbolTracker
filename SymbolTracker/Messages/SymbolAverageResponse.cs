using System;

namespace SymbolTracker.Messages
{
    public class SymbolAverageResponse
    {
        public string SymbolName { get; set; }
        public decimal Average { get; set; }
        public DateTime Date { get; set; }
    }
}