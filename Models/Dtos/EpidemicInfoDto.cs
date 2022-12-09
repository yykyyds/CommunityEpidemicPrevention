namespace Models.Dtos
{
    public class EpidemicInfoDto
    {
        public int DiagnosisCount { get; set; }
        //无症状
        public int AsymptomaticCount { get; set; }
        public string? Content { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int Total { get; set; }
    }
}
