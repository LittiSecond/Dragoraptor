using UnityEngine;


namespace Dragoraptor
{
    public static class AreaChecker
    {

        public static ObjctType CheckPoint(Vector2 worldPosition)
        {
            ObjctType type = ObjctType.None;

            var collision = Physics2D.OverlapPoint(worldPosition);
            if (collision != null)
            {
                int layer = collision.gameObject.layer;
                switch (layer)
                {
                    case (int)SceneLayer.Ground:
                        type = ObjctType.Ground;
                        break;
                    case (int)SceneLayer.Player:
                        type = ObjctType.Player;
                        break;
                    case (int)SceneLayer.Npc:
                        type = ObjctType.Npc;
                        break;
                }

            }

            return type;
        }

    }
}
