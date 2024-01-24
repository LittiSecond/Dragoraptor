namespace Dragoraptor
{
    public interface IHuntResults
    {
        bool IsAlive { get; }
        bool IsSatietyCompleted { get; }
        bool IsSucces { get; }
        int BaseScore { get; }
        int TotalScore { get; }
        int CollectedSatiety { get; }
        int SatietyCondition { get; }
        int MaxSatiety { get; }
        float SatietyScoreMultipler { get; }
        float VictoryScoreMultipler { get; }
    }
}
