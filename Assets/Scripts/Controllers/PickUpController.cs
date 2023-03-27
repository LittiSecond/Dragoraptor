using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PickUpController
    {
        #region Fields

        private readonly PlayerSatiety _satiety;
        private readonly ScoreController _scoreController;

        #endregion


        #region ClassLifeCycles

        public PickUpController(PlayerSatiety satiety, ScoreController scoreController)
        {
            _satiety = satiety;
            _scoreController = scoreController;
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
                        case ResourceType.Score:
                            _scoreController.AddScore(item.Amount);
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