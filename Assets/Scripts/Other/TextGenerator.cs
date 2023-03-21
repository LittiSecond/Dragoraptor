using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class TextGenerator
    {
        #region Fields


        private const string DEFEAT_TEXT = "<color=#9C1B1B>Поражение</color>";
        private const string VICTORY_TEXT = "<color=#9C1B1B>Победа</color>";


        #endregion


        #region ClassLifeCycles

        #endregion


        #region Methods

        public string CreateVictoryText(bool isSucces)
        {
            string text = null;
            if (isSucces)
            {
                text = VICTORY_TEXT;
            }
            else
            {
                text = DEFEAT_TEXT;
            }
            return text;
        }

        public string CreateBigDescription(IHuntResults huntResults)
        {
            string text = "TextGenerator->CreateBigDescription:";

            return text;
        }

        public void StrangeInvoke()
        {

        }

        #endregion
    }
}