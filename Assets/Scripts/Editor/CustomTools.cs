﻿using System;
using UnityEngine;
using UnityEditor;


namespace Dragoraptor.Editor
{
    public sealed class CustomTools : EditorWindow
    {
        #region Fields

        private const string CAPTION = "    Functional to testing tools";

        private const string DAMAGE_CHARACTER_BUTTON_CAPTION = "make damage to the character";
        private const string DAMAGE_CHARACTER_FIELD_CAPTION = "damage amount to the character: ";

        private const string DAMAGE_NPC_BUTTON_CAPTION = "make damage to the NPC";
        private const string DAMAGE_NPC_FIELD_CAPTION = "damage amount to the NPC: ";
        private const string DAMAGE_NPC_OBJECT_CAPTION = "NPC to damaged: ";

        private const string CHARACTER_NOT_FOUND = "Testing tools: character not found";

        private NpcBaseLogick _npc;

        private int _damagToCharacter = 1000;
        private int _damagToNpc = 1000;

        #endregion

        #region ClassLifeCycles

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.Label(CAPTION);
            GUILayout.Space(20);

            _damagToCharacter = EditorGUILayout.IntField(DAMAGE_CHARACTER_FIELD_CAPTION, _damagToCharacter);

            if (GUILayout.Button(DAMAGE_CHARACTER_BUTTON_CAPTION))
            {
                DamageCharacter();
            }

            GUILayout.Space(20);

            _npc = EditorGUILayout.ObjectField(DAMAGE_NPC_OBJECT_CAPTION, _npc, typeof(NpcBaseLogick), true) as 
                NpcBaseLogick;
            _damagToNpc = EditorGUILayout.IntField(DAMAGE_NPC_FIELD_CAPTION, _damagToNpc);
            if (GUILayout.Button(DAMAGE_NPC_BUTTON_CAPTION))
            {
                DamageNpc();
            }

        }

        #endregion


        #region Methods

        [MenuItem("CustomTools/Testing tools")]
        public static void ShowCustomTools()
        {
            EditorWindow.GetWindow(typeof(CustomTools));
        }

        private void DamageCharacter()
        {
            PlayerBody playerBody = GameObject.FindObjectOfType<PlayerBody>();
            if (playerBody)
            {
                playerBody.TakeDamage(_damagToCharacter);
            }
            else
            {
                Debug.Log(CHARACTER_NOT_FOUND);
            }
        }

        private void DamageNpc()
        {
            if (_npc)
            {
                if (_npc.gameObject.activeInHierarchy)
                {
                    _npc.TakeDamage(_damagToNpc);
                }
            }
            else
            {
                Debug.Log(CHARACTER_NOT_FOUND);
            }
        }

        #endregion
    }
}