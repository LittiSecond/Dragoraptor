using UnityEngine;


namespace Dragoraptor
{
    public class NpcBaseDirection
    {
        #region Fields

        private SpriteRenderer _spriteRenderer;

        #endregion


        #region Properties

        public bool IsDirectionToLeft
        { 
            get
            {
                return _spriteRenderer.flipX;
            }
            set
            {
                _spriteRenderer.flipX = value;
            }
        }

        #endregion


        #region ClassLifeCycles

        public NpcBaseDirection(SpriteRenderer mainSprite)
        {
            _spriteRenderer = mainSprite;
        }

        #endregion
    }
}
