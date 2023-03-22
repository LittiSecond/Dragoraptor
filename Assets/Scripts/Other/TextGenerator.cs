using System;
using UnityEngine;
using System.Text.RegularExpressions;


namespace Dragoraptor
{
    public sealed class TextGenerator
    {
        #region Fields


        private const string DEFEAT_TEXT = "<color=#9C1B1B>Поражение</color>";
        private const string VICTORY_TEXT = "<color=#9C1B1B>Победа</color>";
        private const string RESULT_SCORE_TEXT = "Итоговый счёт: ";
        private const string BIG_TEXT_PATTERN_ID = "BigTextPattern";

        private const string STRING_FORMAT = "F";

        //-------------- fild names to insert values
        private const string SCORE = "score";
        private const string COLLECTED_SATIETY = "collectedSatiety";
        private const string MAX_SATIETY = "maxSatiety";
        private const string NEED_SATIETY = "needSatiety";
        private const string SATIETY_MULTIPLER = "satietyMultipler";
        private const string VICTORY_MULTIPLER = "victoryMultipler";
        //--------------
 
        private const string REGEX_PATTERN_FIND_FIELDS = @"{\w+}";


        private string _huntResultsDescriptionPattern;

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
            string text = null;

            if (_huntResultsDescriptionPattern == null)
            {
                _huntResultsDescriptionPattern = LoadPattern();
            }
            if (_huntResultsDescriptionPattern != null)
            {
                text = _huntResultsDescriptionPattern;

                MatchCollection matches = Regex.Matches(text, REGEX_PATTERN_FIND_FIELDS);

                int length = matches.Count;

                if (length > 0)
                {
                    for (int i = length - 1; i >= 0; i--)
                    {
                        Match current = matches[i];

                        string fieldName = current.Value.Substring(1, current.Length - 2);
                        string value;

                        value = GetFieldValue(huntResults, fieldName);

                        if (value != null)
                        {
                            text = text.Remove(current.Index, current.Length);
                            text = text.Insert(current.Index, value);
                        }
                    }
                }

            }

            return text;
        }

        private string LoadPattern()
        {
            TextAsset textAsset = PrefabLoader.LoadTextAsset(BIG_TEXT_PATTERN_ID);
            return textAsset.text;
        }

        private static string GetFieldValue(IHuntResults huntResults, string fieldName)
        {
            string value = null;

            switch (fieldName)
            {
                case SCORE:
                    value = huntResults.BaseScore.ToString();
                    break;
                case COLLECTED_SATIETY:
                    value = huntResults.CollectedSatiety.ToString();
                    break;
                case MAX_SATIETY:
                    value = huntResults.MaxSatiety.ToString();
                    break;
                case NEED_SATIETY:
                    value = huntResults.SatietyCondition.ToString();
                    break;
                case SATIETY_MULTIPLER:
                    value = huntResults.SatietyScoreMultipler.ToString(STRING_FORMAT);
                    break;
                case VICTORY_MULTIPLER:
                    value = huntResults.VictoryScoreMultipler.ToString();
                    break;
            }

            return value;

        }

        public string CreateResultsScoreText(IHuntResults huntResults)
        {
            return RESULT_SCORE_TEXT + huntResults.TotalScore.ToString();
        }


        #endregion
    }
}