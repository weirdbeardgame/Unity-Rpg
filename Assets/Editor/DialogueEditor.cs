using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class DialogueEditor : EditorWindow
{
    Vector2 Size;
    Vector2 Offset;
    Vector2 Drag;

    BinarySearchTree<DialogueMessage> DialogueTree;
    BinarySearchTree<DialogueNode> DialogueDisplay;

    List<BinarySearchTree<DialogueMessage>> Trees;
    List<string> ItemList;
    DialogueNode NodeToCreate;

    List<Flags> FlagData;
    List<string> FlagString;

    public List<List<DialogueMessage>> Temp;

    DialogueMessage Message;

    int NodeID = 0;
    int TreeID = 0;

    bool Init = false;

    int SelectedTree;
    int SelectedPrevious;

    GUIStyle Style;
    GUIStyle RightPoint;
    GUIStyle LeftPoint;

    ConnectionPoints SelectedInPoint;
    ConnectionPoints SelectedOutPoint;

    string FilePath = "Assets/Dialogue/Dialogue.json";
    string FlagPath = "Assets/Flags.json";
    [JsonConverter(typeof(TreeSerialize<DialogueMessage>))]
    public string JsonData;

    [MenuItem("Window/Dialogue")]
    private static void OpenWindow()
    {
        DialogueEditor window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }


    public void OpenWindowEditor(BinarySearchTree<DialogueMessage> Tree, int Quest)
    {
        ReadJson();
        DialogueEditor window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
        Init = true;
    }


    private void OnClickRemoveNode(DialogueMessage node)
    {
        DialogueTree.Remove(node, DialogueTree.Tree);
    }

    public void ReadJson()
    {
        List<BinarySearchTree<DialogueMessage>> ScratchPadTrees = new List<BinarySearchTree<DialogueMessage>>();
        ItemList = new List<string>();

        Temp = new List<List<DialogueMessage>>();

        if (!File.Exists(FilePath))
        {
            DialogueTree = new BinarySearchTree<DialogueMessage>();
            Trees.Add(DialogueTree);
            ItemList.Add("Tree " + TreeID.ToString() + ": ");
        }

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);

            Temp = JsonConvert.DeserializeObject<List<List<DialogueMessage>>>(JsonData, new TreeSerialize<List<List<DialogueMessage>>>());

            for (int i = 0; i < Temp.Count; i++)
            {
                DialogueTree = new BinarySearchTree<DialogueMessage>();

                List<DialogueMessage> LoopThrough = Temp[i];

                for (int j = 0; j < LoopThrough.Count; j++)
                {
                    DialogueTree.Insert(LoopThrough[j]); // An attempt to construct the tree itself.
                }

                TreeID = i;
                Trees.Add(DialogueTree); // Saving the currently constructed tree
                ItemList.Add("Tree " + TreeID.ToString() + ": "); // And making it selectable
            }


            foreach (var Node in Trees[SelectedTree])
            {
                if (Node != null)
                {

                    NodeToCreate = new DialogueNode();
                    NodeToCreate.CreateNode("", new Vector2(0, 0), 250, 150, Style, LeftPoint, RightPoint, OnClickRemoveNode, ref NodeID, Node, Node.NodeT);
                    AddNode(NodeToCreate, Node.NodeT);
                }

                else
                {
                    break;
                }
            }

            TreeID++;

            if (File.Exists(FlagPath))
            {
                JsonData = File.ReadAllText(FlagPath);
                FlagData = JsonConvert.DeserializeObject<List<Flags>>(JsonData);

                FlagString = new List<string>();

                for (int i = 0; i < FlagData.Count; i++)
                {
                    FlagString.Add(FlagData[i].Flag);
                }
            }
        }
    }

    public void CreateNode(Vector2 position, NodeType Type)
    {
        NodeToCreate = new DialogueNode();

        Style = new GUIStyle();
        Style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        Style.clipping = TextClipping.Clip;
        Style.border = new RectOffset(12, 12, 30, 12);

        RightPoint = new GUIStyle();
        RightPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        RightPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        RightPoint.border = new RectOffset(4, 4, 12, 12);

        LeftPoint = new GUIStyle();
        LeftPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        LeftPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        LeftPoint.border = new RectOffset(4, 4, 12, 12);

        Message = new DialogueMessage();
        Message.ID = NodeID;
        NodeToCreate.CreateNode("", position, 320, 200, Style, RightPoint, LeftPoint, OnClickRemoveNode, ref NodeID, Message, Type);

        DialogueTree.Insert(Message);
        DialogueDisplay.Insert(NodeToCreate);
        NodeID += 1;
    }


    void AddNode(DialogueNode Node, NodeType Type)
    {

        Style = new GUIStyle();
        Style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        Style.clipping = TextClipping.Clip;
        Style.border = new RectOffset(12, 12, 12, 12);
        RightPoint = new GUIStyle();
        RightPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        RightPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        RightPoint.border = new RectOffset(4, 4, 12, 12);

        LeftPoint = new GUIStyle();
        LeftPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        LeftPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        LeftPoint.border = new RectOffset(4, 4, 12, 12);

        Size = new Vector2(400, 150);
        Node.DNode.Flag = new Flags();
        Message = new DialogueMessage();
        Message.ID = NodeID;
        Node.CreateNode("", new Vector2(Node.PosX, Node.PosY), 250, 150, Style, LeftPoint, RightPoint, OnClickRemoveNode, ref NodeID, Node.DNode, Type);

        DialogueDisplay.Insert(Node);
        NodeID += 1;
    }


    void OnGUI()
    {
        if (!Init)
        {

            DialogueTree = new BinarySearchTree<DialogueMessage>();
            Trees = new List<BinarySearchTree<DialogueMessage>>();

            DialogueDisplay = new BinarySearchTree<DialogueNode>();

            ReadJson();

            Init = true;
        }

        if (Init)
        {
            DrawNodes();

            ProcessEvents(Event.current);
            ProcessNodeEvents(Event.current);

            ChangeTree();

            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);
        }
    }

    void DrawNodes()
    {
        BinarySearchTree<DialogueNode> ScratchPad = DialogueDisplay;

        foreach (var DrawNode in ScratchPad)
        {
            DrawNode.Draw(FlagString.ToArray(), FlagData);
        }

    }

    void ChangeTree()
    {
        SelectedPrevious = SelectedTree;
        SelectedTree = EditorGUILayout.Popup(SelectedTree, ItemList.ToArray());

        if (SelectedTree != SelectedPrevious)
        {
            DialogueDisplay.Clear();

            foreach (var Node in Trees[SelectedTree])
            {
                if (Node != null)
                {
                    NodeToCreate = new DialogueNode();
                    //.NodeToCreate.CreateNode("", new Vector2(0, 0), 250, 150, Style, LeftPoint, RightPoint, OnClickRemoveNode, ref NodeID, Node, Node.NodeT);
                    AddNode(NodeToCreate, Node.NodeT);
                }

                else
                {
                    break;
                }
            }

            SelectedPrevious = SelectedTree;

        }

    }

    public void ProcessEvents(Event e)
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

        BinarySearchTree<DialogueNode> ScratchPad = DialogueDisplay;

        if (ScratchPad != null)
        {
            foreach (var EventNode in ScratchPad)
            {
                bool guiChanged = EventNode.ProcessNodeEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }


    public void openContextMenu(Vector2 position)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("New Tree"), false, () => NewTree());
        genericMenu.AddItem(new GUIContent("Add Flag"), false, () => CreateNode(position, NodeType.FLAG));
        genericMenu.AddItem(new GUIContent("Add Dialogue"), false, () => CreateNode(position, NodeType.DIALOUGE));
        genericMenu.AddItem(new GUIContent("Add Choice"), false, () => CreateNode(position, NodeType.CHOICE));
        genericMenu.AddItem(new GUIContent("Add Event"), false, () => CreateNode(position, NodeType.EVENT));
        genericMenu.AddItem(new GUIContent("Save Tree"), false, () => SaveTree());
        genericMenu.AddItem(new GUIContent("Save"), false, () => Save());
        genericMenu.ShowAsContext();
    }

    public void NewTree()
    {
        if (Trees == null)
        {
            Trees = new List<BinarySearchTree<DialogueMessage>>();
        }

        // Creating a new tree. Save previous in a list my dude
        BinarySearchTree<DialogueMessage> Temp = new BinarySearchTree<DialogueMessage>();
        Trees.Add(Temp);
        ItemList.Add("Tree " + TreeID.ToString() + ": ");
        TreeID += 1;
    }

    public void SaveTree()
    {
        if (!Trees.Contains(DialogueTree))
        {
            Trees.Add(DialogueTree);
        }

    }

    public void Save()
    {
        string Data = " ";

        if (!Trees.Contains(DialogueTree))
        {
            Trees.Add(DialogueTree);
        }

        List<List<DialogueMessage>> TempSave = new List<List<DialogueMessage>>();

        List<DialogueMessage> TempData;
        for (int i = 0; i < Trees.Count; i++)
        {
            TempData = new List<DialogueMessage>();
            TempData = Trees[i].GetData();
            TempSave.Add(TempData);
        }

        Data = JsonConvert.SerializeObject(TempSave, Formatting.Indented);

        File.WriteAllText(FilePath, Data);

        // I need to save link directions as well as each specific node in here

    }
}
