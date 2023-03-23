using UnityEngine;


namespace Dragoraptor
{
    public class NpcDirectionByFlip
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

        public NpcDirectionByFlip(SpriteRenderer mainSprite)
        {
            _spriteRenderer = mainSprite;
        }

        #endregion
    }
}
