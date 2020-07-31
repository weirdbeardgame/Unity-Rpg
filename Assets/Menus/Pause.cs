using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// All this needs to be is a description of the menu itself no manager functions need to exist

namespace menu
{

    public enum Apps { ITEMS = 1, WEAPONS = 2, QUEST = 3, STATUS = 4, QUIT = 5 };

    public class Pause : ScriptableObject, IMenu
    {
        public Apps Apps;

        public Widget Prefab;

        public Material MenuScreen;

        public void Open()
        {

        }

        public void SetShader(Material m)
        {

        }

        public Material GetShader()
        {
            return MenuScreen;
        }




    } // What open should do is initalize all variables. 

}