using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class SkillEditor : EditorWindow
{

    public SkillData CurrentSkill;
    Creature creature;
    string JsonData;
    string SkillName;
    string FilePath = "Assets/Skill/skills.json";
    public Dictionary<int, SkillData> Skills;


    int SelectedIndex = 0;
    int SkillID;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    SerializedProperty currentProperty;

    bool IsInitalized;

    int SkillIndex;

    [MenuItem("Window/Skill")]
    public static void ShowWindow()
    {
        GetWindow<SkillEditor>("New Skill");
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };


    public void readJson()
    {

        Skills = new Dictionary<int, SkillData>();

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);

            Skills = JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(JsonData);

            IsInitalized = true;
        }
    }

    void OnGUI()
    {

        if (!IsInitalized)
        {
            readJson();
        }

        ButtonList = new Rect(0, 0, 150, position.height);
        TopProperties = new Rect(500, 0, 1000, 150);
        PropertyPage = new Rect(500, 100, 1000, position.height);

        GUI.Box(PropertyPage, "Skills");
        GUI.Box(ButtonList, "Buttons");

        GUILayout.BeginArea(TopProperties);

        GUILayout.Label("Skill Name");
        SkillName = EditorGUILayout.TextField(SkillName);

        GUILayout.Label("Skill ID");
        SkillIndex = EditorGUILayout.IntField(SkillIndex);

        if (GUILayout.Button("New Skill"))
        {

            if (Skills == null)
            {
                Skills = new Dictionary<int, SkillData>();
            }

            CurrentSkill = ScriptableObject.CreateInstance<SkillData>();
            CurrentSkill.SkillName = SkillName;
            Skills.Add(SkillIndex, CurrentSkill);
        }
        GUILayout.EndArea();

        if (Skills != null)
        {
            for (int i = 0; i < Skills.Count; i++)
            {

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(Skills[i].SkillName))
                {
                    SelectedIndex = i;
                    Debug.Log("User Clicked " + Skills[i].SkillName);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }
            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Item ID");
            Skills[SelectedIndex].SkillID = EditorGUILayout.IntField(Skills[SelectedIndex].SkillID);
            EditorGUILayout.LabelField("Item Name");
            Skills[SelectedIndex].SkillName = EditorGUILayout.TextField(Skills[SelectedIndex].SkillName);
            EditorGUILayout.LabelField("Buffer Type");
            Skills[SelectedIndex].SkillType = (SkillTypes)EditorGUILayout.EnumPopup(Skills[SelectedIndex].SkillType);
            EditorGUILayout.LabelField("Effect Type");
            Skills[SelectedIndex].Attribute = (AttributeType)EditorGUILayout.EnumPopup(Skills[SelectedIndex].Attribute);
            EditorGUILayout.LabelField("Effect: ");
            Skills[SelectedIndex].Effect = EditorGUILayout.FloatField(Skills[SelectedIndex].Effect);
            EditorGUILayout.LabelField("Buffs / Debuffs: ");
            if (GUILayout.Button("Add Buffer"))
            {
                Skills[SelectedIndex].Buffer = new Buffers();
            }

            if (Skills[SelectedIndex].Buffer != null)
            {
                EditorGUILayout.LabelField("Type of Effect: ");
                Skills[SelectedIndex].Buffer.Effect = (BufferEffect)EditorGUILayout.EnumPopup(Skills[SelectedIndex].Buffer.Effect);
                EditorGUILayout.LabelField("Buffer Effect: ");
                Skills[SelectedIndex].Buffer.Type = (BuffType)EditorGUILayout.EnumPopup(Skills[SelectedIndex].Buffer.Type);
                EditorGUILayout.LabelField("Time of Buffer");
                Skills[SelectedIndex].Buffer.EffectTimer = EditorGUILayout.FloatField(Skills[SelectedIndex].Buffer.EffectTimer);
                EditorGUILayout.LabelField("Buffer to Apply");
                Skills[SelectedIndex].Buffer.Buff = EditorGUILayout.FloatField(Skills[SelectedIndex].Buffer.Buff);
            }


            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            string Data = JsonConvert.SerializeObject(Skills);
            File.WriteAllText(FilePath, Data);
        }
        GUILayout.EndHorizontal();
    }
}
