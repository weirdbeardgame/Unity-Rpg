using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class SpeakerEditor : EditorWindow
{

    List<SpeakerData> Speakers;
    string FilePath = "Assets/Speakers.json";
    string JsonData;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    SpeakerData Create;

    bool Initalized = false;

    int ClickedIndex = 0;
    int ID;

    [MenuItem("Window/Speakers")]
    public static void ShowWindow()
    {
        GetWindow<SpeakerEditor>("Create Speakers");
    }

    void ReadJson()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Speakers = JsonConvert.DeserializeObject<List<SpeakerData>>(JsonData);
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
            Speakers = new List<SpeakerData>();

            ButtonList = new Rect(0, 0, 150, position.height);
            TopProperties = new Rect(500, 0, 1000, 150);
            PropertyPage = new Rect(500, 100, 1000, position.height);
            ReadJson();
            ID = Speakers.Count;

        }

        GUI.Box(PropertyPage, "Items");
        GUI.Box(ButtonList, "Buttons");

        GUILayout.BeginArea(TopProperties);

        if (GUILayout.Button("New Speaker"))
        {
            if (Speakers == null)
            {
                Speakers = new List<SpeakerData>();
            }
            Create = new SpeakerData();



            if (Speakers.Count == 0)
            {
                ID = 0;
                Create.SpeakerID = 0;
            }
            else
            {
                Create.SpeakerID = ID;
                ID++;
            }

            Speakers.Add(Create);
        }

        GUILayout.EndArea();

        if (Speakers != null && Speakers.Count > 0)
        {
            for (int i = 0; i < Speakers.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(Speakers[i].SpeakerName))
                {
                    ClickedIndex = i;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }

            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Speaker ID: ");
            Speakers[ClickedIndex].SpeakerID = EditorGUILayout.IntField(Speakers[ClickedIndex].SpeakerID);
            EditorGUILayout.LabelField("Speaker Name");
            Speakers[ClickedIndex].SpeakerName = EditorGUILayout.TextField(Speakers[ClickedIndex].SpeakerName);
            //EditorGUILayout.LabelField("Is Sub Flag: ");
            //Flags[ClickedIndex].SubFlag = EditorGUILayout.Toggle(Flags[ClickedIndex].SubFlag);
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
        string Data = JsonConvert.SerializeObject(Speakers);
        File.WriteAllText(FilePath, Data);

    }

}
