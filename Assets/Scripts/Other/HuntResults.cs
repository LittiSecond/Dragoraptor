namespace Dragoraptor
{
    public sealed class HuntResults : IHuntResults
    {

        public bool IsAlive { get; set; }

        public bool IsSatietyCompleted { get; set; }

        public bool IsSucces { get; set; }

        public int BaseScore { get; set; }

        public int CollectedSatiety { get; set; }

        public int MaxSatiety { get; set; }

        public int SatietyCondition { get; set; }

        public float SatietyScoreMultipler { get; set; }

        public int TotalScore { get; set; }

        public float VictoryScoreMultipler { get; set; }

    }
}