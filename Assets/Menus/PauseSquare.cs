using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using menu;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class PauseSquare : Widget
{

    public override void Initalize(int Var1, string Name)
    {

    }


    public override void OnUI()
    {
        base.OnUI();

        GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;
        GetComponent<Button>().targetGraphic = GetComponent<Image>();

        GetComponent<Image>().sprite = Skin.NormalSprite;
        GetComponent<Button>().spriteState = Skin.WidgetState;
        GetComponent<Image>().type = Image.Type.Sliced;
    }

    public override void Execute()
    {
        _Manager = FindObjectOfType<MenuManager>();
        _Manager.OpenMenu(ToUse);
    }


}
