using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class FlagEditor : EditorWindow
{

    List<Flags> Flags;
    string FilePath = "Assets/Flags.json";
    string JsonData;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    Flags Create;

    bool Initalized = false;

    int ClickedIndex = 0;
    int ID;

    [MenuItem("Window/Flags")]
    public static void ShowWindow()
    {
        GetWindow<FlagEditor>("Create Flags");
    }

    void ReadJson()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Flags = JsonConvert.DeserializeObject<List<Flags>>(JsonData);
        }

        else if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
        }

        Initalized = true;
    }


    void OnGUI()
    {

        if (!Initalized)
        {
            Flags = new List<Flags>();

            ButtonList = new Rect(0, 0, 150, position.height);
            TopProperties = new Rect(500, 0, 1000, 150);
            PropertyPage = new Rect(500, 100, 1000, position.height);
            ReadJson();
            ID = Flags.Count;

        }

        GUI.Box(PropertyPage, "Items");
        GUI.Box(ButtonList, "Buttons");

        GUILayout.BeginArea(TopProperties);

        if (GUILayout.Button("New Flag"))
        {
            if (Flags == null)
            {
                Flags = new List<Flags>();
            }
            Create = new Flags();
            Create.Flag = "Temp";

            if (Flags.Count == 0)
            {
                ID = 0;
                Create.ID = 0;
            }
            else
            {
                Create.ID = ID;
                ID++;
            }

            Flags.Add(Create);
        }

        GUILayout.EndArea();

        if (Flags != null && Flags.Count > 0)
        {
            for (int i = 0; i < Flags.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(Flags[i].Flag))
                {
                    ClickedIndex = i;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }

            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Flag ID: ");
            Flags[ClickedIndex].ID = EditorGUILayout.IntField(Flags[ClickedIndex].ID);
            EditorGUILayout.LabelField("Flag");
            Flags[ClickedIndex].Flag = EditorGUILayout.TextField(Flags[ClickedIndex].Flag);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save"))
            {
                Save();
            }
            GUILayout.EndHorizontal();
        }
    }

    void Save()
    {
        string Data = JsonConvert.SerializeObject(Flags);
        File.WriteAllText(FilePath, Data);

    }

}
