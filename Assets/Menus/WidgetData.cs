using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "New Widget")]
public class WidgetData : ScriptableObject
{
    public Sprite NormalSprite;
    public SpriteState WidgetState;

}



/************************************************************************************
 *   A Dynamic design would be a simple open or use data like an interger being passed.
 *   The Menu Manager should handle the lifetime of each widget object indiviually. And 
 *   Execute their functions as they would.  
***************************************************************************************/