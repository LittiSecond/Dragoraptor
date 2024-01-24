using UnityEngine;


namespace Dragoraptor
{
    public sealed class FadingOnGroundFall : MassFading
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == (int)SceneLayer.Ground)
            {
                StartFading();
            }
        }

    }
}
