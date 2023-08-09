using System.Text.Json.Serialization;

namespace MatthiWare.FinancialModelingPrep.Model.Economics
{
    public class TreasuryRateResponse
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("month1")]
        public double Month1 { get; set; }

        [JsonPropertyName("month2")]
        public double Month2 { get; set; }

        [JsonPropertyName("month3")]
        public double Month3 { get; set; }

        [JsonPropertyName("month6")]
        public double Month6 { get; set; }

        [JsonPropertyName("year1")]
        public double Year1 { get; set; }

        [JsonPropertyName("year2")]
        public double Year2 { get; set; }
        
        [JsonPropertyName("year3")]
        public double Year3 { get; set; }
        
        [JsonPropertyName("year5")]
        public double Year5 { get; set; }
        
        [JsonPropertyName("year7")]
        public double Year7 { get; set; }
        
        [JsonPropertyName("year10")]
        public double Year10 { get; set; }
        
        [JsonPropertyName("year20")]
        public double Year20 { get; set; }

        [JsonPropertyName("year30")]
        public double Year30 { get; set; }
    }
}
