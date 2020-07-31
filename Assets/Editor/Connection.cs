using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[System.Serializable]
public class Connection
{

    public ConnectionPoints InPoint; // one rect
    public ConnectionPoints OutPoint; // one rect
    Action<Connection> OnClickRemoveConnection;

    public bool Click;

    public Connection(ConnectionPoints L_In, ConnectionPoints L_Out, Action<Connection> RemoveConnection)
    {
        InPoint = L_In;
        OutPoint = L_Out;
        OnClickRemoveConnection = RemoveConnection;
    }

    public void Draw() // On click. This is why left can't draw a brezire line
    {
        Handles.DrawBezier(
        InPoint.Rect.center,
        OutPoint.Rect.center,
        InPoint.Rect.center + Vector2.left * 50f,
        OutPoint.Rect.center - Vector2.left * 50f,
        Color.white,
        null,
        2f);

        Click = Handles.Button((InPoint.Rect.center + OutPoint.Rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap);
    }


    public void ProcessEvents(Event e)
    {
        if (Click)
        {
            if (OnClickRemoveConnection != null)
            {
                OnClickRemoveConnection(this);
            }
        }
    }
}
