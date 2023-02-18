using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerBody : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigedbody;

        #endregion


        #region Methods

        public Rigidbody2D GetRigidbody()
        {
            return _rigedbody;
        }

        #endregion

    }
}