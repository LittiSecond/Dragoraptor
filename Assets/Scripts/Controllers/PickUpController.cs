using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PickUpController
    {
        #region Fields

        private readonly PlayerSatiety _satiety;

        #endregion


        #region ClassLifeCycles

        public PickUpController(PlayerSatiety satiety)
        {
            _satiety = satiety;
        }

        #endregion


        #region Methods

        public bool PickUp(PickableResource[] content)
        {
            bool isPicked = false;

            if (content != null)
            {
                for (int i = 0; i < content.Length; i++)
                {
                    var item = content[i];

                    switch (item.Type)
                    {
                        case ResourceType.Satiety:
                            _satiety.AddSatiety(item.Amount);
                            isPicked = true;
                            break;
                    }

                }
            }

            return isPicked;
        }

        #endregion
    }
}