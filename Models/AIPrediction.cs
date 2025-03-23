namespace DocBookAPI.Models
{
    public class AIPrediction
    {
        public string Id { get; set; }

        public string PatientId { get; set; }
        public string ReportId { get; set; }
        public string PredictedIllness { get; set; }
        public double ConfidenceScore { get; set; } // Between 0 and 100
        public DateTime PredictionTimestamp { get; set; } = DateTime.UtcNow;
    }
}
