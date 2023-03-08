using System.Collections.Generic;


namespace Dragoraptor
{
    public static class PrefabPaths
    {
        public static readonly Dictionary<string, string> Paths = new Dictionary<string, string>()
        {

            {
                "HuntScreen", "Prefabs/Ui/HuntScreen"
            },
            {
                "MainScreen", "Prefabs/Ui/MainScreen"
            },
            {
                "PlayerCharacter", "Prefabs/PlayerCharacter"
            },
            {
                "StoneBall", "Prefabs/Bullets/StoneBall"
            },
        };
    }
}
