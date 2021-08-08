using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum Link { LEFT, RIGHT }

[System.Serializable]
public class ConnectionPoints
{
    private Link _Link;
    private DialogueNode _Node;

    private NodeType _NType;


    [System.NonSerialized]
    private GUIStyle _Style;
    [System.NonSerialized]
    private string _LinkLable;
    [System.NonSerialized]
    public Rect Rect;

    [System.NonSerialized]
    Action<ConnectionPoints> OnClickConnectionPoint;

    public ConnectionPoints()
    {
        _Node = null;
        _Style = null;
        OnClickConnectionPoint = null;
        Rect = new Rect(0, 0, 10f, 20f);
    }


    public ConnectionPoints(DialogueNode N, Link L, GUIStyle Style, Action<ConnectionPoints> Action)
    {
        _Node = N;
        _Link = L;
        _NType = N.dNode.NodeT;
        _Style = Style;
        OnClickConnectionPoint = Action;
        Rect = new Rect(0, 0, 10f, 20f);
    }

    public void Draw()
    {

        switch (_NType)
        {
            case NodeType.DIALOUGE:

                Rect.y = _Node.node.y + (_Node.node.height * 0.5f) - Rect.height * 0.5f;

                switch (_Link)
                {
                    case Link.LEFT:
                        Rect.x = _Node.node.x - Rect.width + 0f;
                        break;

                    case Link.RIGHT:
                        Rect.x = _Node.node.x + _Node.node.width - 0f;
                        break;
                }

                if (GUI.Button(Rect, "", _Style))
                {
                    if (OnClickConnectionPoint != null)
                    {
                        OnClickConnectionPoint(this);
                    }
                }
                break;

            case NodeType.CHOICE:

                switch (_Link)
                {
                    case Link.LEFT:
                        Rect.x = _Node.node.x - Rect.width + 0f;
                        Rect.y = _Node.node.y - Rect.height + 0f;
                        break;

                    case Link.RIGHT:
                        Rect.x = _Node.node.x + _Node.node.width - 0f;
                        Rect.y = _Node.node.y - _Node.node.height - 0f;
                        break;

                }

                if (GUI.Button(Rect, "", _Style))
                {
                    if (OnClickConnectionPoint != null)
                    {
                        OnClickConnectionPoint(this);
                    }
                }

                break;
        }
    }
}

