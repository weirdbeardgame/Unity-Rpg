using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;


public class MenuEditor : EditorWindow
{
    Vector2 Size;
    Vector2 Offset;
    Vector2 Drag;

    List<Connection> Connections;

    List<Widget> Widgets;
    Widget NodeToCreate;

    int DNodeID = 0;
    int TreeID = 0;

    bool Init = false;


    GUIStyle Style;
    GUIStyle InPoint;
    GUIStyle OutPoint;

    ConnectionPoints SelectedInPoint;
    ConnectionPoints SelectedOutPoint;

    string FilePath = "Assets/Dialogue/Dialogue.json";
    string JsonData;

    [MenuItem("Window/Menu")]
    private static void OpenWindow()
    {
        MenuEditor window = GetWindow<MenuEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }

    private void OnClickRemoveNode(Widget WidgetNode)
    {
        if (Connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            for (int i = 0; i < Connections.Count; i++)
            {
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                Connections.Remove(connectionsToRemove[i]);
            }

        }

        Widgets.Remove(WidgetNode);

    }

    public void ReadJson()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);

            Widgets = JsonConvert.DeserializeObject<List<Widget>>(JsonData);

        }
    }


    public void CreateNode(Vector2 position)
    {

        //NodeToCreate = new Widget();

        Style = new GUIStyle();
        Style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        Style.clipping = TextClipping.Clip;
        Style.border = new RectOffset(12, 12, 12, 12);

        InPoint = new GUIStyle();
        InPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        InPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        InPoint.border = new RectOffset(4, 4, 12, 12);

        OutPoint = new GUIStyle();
        OutPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        OutPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        OutPoint.border = new RectOffset(4, 4, 12, 12);


    }

    void OnGUI()
    {
        if (!Init)
        {
            Widgets = new List<Widget>();
            Connections = new List<Connection>();
            ReadJson();
            Init = true;
        }

        if (Widgets != null)
        {
            DrawNodes();
            //ProcessEvents(Event.current);
            ProcessNodeEvents(Event.current);
            DrawConnections();
            DrawConnectionLine(Event.current);
        }

        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
    }

    void DrawNodes()
    {

    }

    /* public void ProcessEvents(Event e)
     {
         switch (e.type)
         {
             case EventType.MouseDown:
                 if (e.button == 1) // right clicky
                 {
                     Debug.Log("Right Clicky");
                     openContextMenu(e.mousePosition);
                 }
                 break;
         }
     }*/

    private void OnClickInPoint(ConnectionPoints InPoint)
    {
        SelectedInPoint = InPoint;

        if (SelectedOutPoint != null)
        {
            if (SelectedOutPoint.Rect != SelectedInPoint.Rect)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoints OutPoint)
    {
        SelectedOutPoint = OutPoint;
        if (SelectedInPoint != null)
        {
            if (SelectedOutPoint.Rect != SelectedInPoint.Rect)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickRemoveConnection(Connection Connection)
    {
        Connections.Remove(Connection);
    }

    private void CreateConnection()
    {
        if (Connections == null)
        {
            Connections = new List<Connection>();
        }

        Connections.Add(new Connection(SelectedInPoint, SelectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        SelectedInPoint = null;
        SelectedOutPoint = null;
    }

    void DrawConnections()
    {
        if (Connections != null)
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                Connections[i].Draw();
            }
        }
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        Offset += Drag * 0.5f;
        Vector3 newOffset = new Vector3(Offset.x % gridSpacing, Offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void ProcessNodeEvents(Event e)
    {

    }

    private void DrawConnectionLine(Event e)
    {
        if (InPoint != null && OutPoint == null)
        {
            Handles.DrawBezier(
                SelectedInPoint.Rect.center,
                e.mousePosition,
                SelectedInPoint.Rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (SelectedOutPoint != null && SelectedInPoint == null)
        {
            Handles.DrawBezier(
                SelectedOutPoint.Rect.center,
                e.mousePosition,
                SelectedOutPoint.Rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    public void openContextMenu(Vector2 position)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => CreateNode(position));
        genericMenu.AddItem(new GUIContent("Save"), false, () => Save());
        genericMenu.ShowAsContext();
    }


    public void Save()
    {

    }
}