using System;

namespace Dragoraptor
{
    public interface ILevelMapView
    {
        event Action<int> OnLevelClick;
        void SetLevelStatus(int levelNumber, LevelStatus newStatus);
        void SetLevelSelected(int levelNumber);
    }
}