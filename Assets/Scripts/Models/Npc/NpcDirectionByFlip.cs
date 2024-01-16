using UnityEngine;


namespace Dragoraptor
{
    public class NpcDirectionByFlip
    {
        private SpriteRenderer _spriteRenderer;

        
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


        public NpcDirectionByFlip(SpriteRenderer mainSprite)
        {
            _spriteRenderer = mainSprite;
        }

    }
}
