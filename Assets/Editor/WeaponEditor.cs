
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class WeaponEditor : EditorWindow
{
    public WeaponData CurrentWeapon;
    Creature creature;
    string JsonData;
    string ItemName;
    string FilePath = "Assets/Weapons/Weapons.json";
    string SkillPath = "Assets/Skill/skills.json";
    public Dictionary<int, WeaponData> Weapons;

    Dictionary<int, SkillData> _Skills;
    List<string> SkillNames;
    string SkillJson;

    int SelectedIndex = 0;
    int ItemID;
    int SkillID;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    SerializedProperty currentProperty;

    bool IsInitalized;

    int ItemIndex;

    [MenuItem("Window/Weapons")]
    public static void ShowWindow()
    {
        GetWindow<WeaponEditor>("New Weapon");
    }
    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };


    public void readJson()
    {
        Weapons = new Dictionary<int, WeaponData>();

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);

            Weapons = JsonConvert.DeserializeObject<Dictionary<int, WeaponData>>(JsonData);

            IsInitalized = true;
        }

        if (File.Exists(SkillPath))
        {
            SkillJson = File.ReadAllText(SkillPath);
            _Skills = JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(SkillJson);
            SkillNames = new List<string>();
        }

        for (int i = 0; i < _Skills.Count; i++)
        {
            SkillNames.Add(_Skills[i].SkillName);
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

        GUI.Box(PropertyPage, "Weapons");
        GUI.Box(ButtonList, "Buttons");

        GUILayout.BeginArea(TopProperties);

        GUILayout.Label("Weapon Name");
        ItemName = EditorGUILayout.TextField(ItemName);

        GUILayout.Label("Weapon ID");
        ItemIndex = EditorGUILayout.IntField(ItemIndex);

        if (GUILayout.Button("New Weapon"))
        {

            if (Weapons == null)
            {
                Weapons = new Dictionary<int, WeaponData>();
            }

            CurrentWeapon = new WeaponData();
            CurrentWeapon.WeaponName = ItemName;
            CurrentWeapon.Buffer = new Buffers();
            CurrentWeapon.Skills = new List<SkillData>();
            Weapons.Add(ItemIndex, CurrentWeapon);
        }
        GUILayout.EndArea();

        if (Weapons != null)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(Weapons[i].WeaponName))
                {
                    SelectedIndex = i;
                    Debug.Log("User Clicked " + Weapons[i].WeaponName);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Weapon ID");
            Weapons[SelectedIndex].WeaponID = EditorGUILayout.IntField(Weapons[SelectedIndex].WeaponID);
            EditorGUILayout.LabelField("Weapon Name");
            Weapons[SelectedIndex].WeaponName = EditorGUILayout.TextField(Weapons[SelectedIndex].WeaponName);
            EditorGUILayout.LabelField("Weapon Description");
            Weapons[SelectedIndex].Description = EditorGUILayout.TextField(Weapons[SelectedIndex].Description);
            EditorGUILayout.LabelField("Appendage");
            Weapons[SelectedIndex].Appendage = (Appendage)EditorGUILayout.EnumPopup(Weapons[SelectedIndex].Appendage);
            EditorGUILayout.LabelField("Equipable Job");
            Weapons[SelectedIndex].Job = (JobSystem)EditorGUILayout.EnumPopup(Weapons[SelectedIndex].Job);
            EditorGUILayout.LabelField("Buffers");
            EditorGUILayout.LabelField("Health");
            //Weapons[SelectedIndex].Buffer.HpBuf = EditorGUILayout.FloatField(Weapons[SelectedIndex].Buffer.HpBuf);
            EditorGUILayout.LabelField("Strength");
            //Weapons[SelectedIndex].Buffer.StrBuf = EditorGUILayout.FloatField(Weapons[SelectedIndex].Buffer.StrBuf);
            EditorGUILayout.LabelField("Magic");
            //Weapons[SelectedIndex].Buffer.MagBuf = EditorGUILayout.FloatField(Weapons[SelectedIndex].Buffer.MagBuf);
            EditorGUILayout.LabelField("Speed");
            //Weapons[SelectedIndex].Buffer.SpdBuf = EditorGUILayout.FloatField(Weapons[SelectedIndex].Buffer.SpdBuf);
            EditorGUILayout.LabelField("Special");
            //Weapons[SelectedIndex].Buffer.SpclBuf = EditorGUILayout.FloatField(Weapons[SelectedIndex].Buffer.SpclBuf);

            EditorGUILayout.LabelField("Learnable Skills");
            if (GUILayout.Button("Add Skill"))
            {
                SkillData SkillToEdit = new SkillData();
                if (Weapons[SelectedIndex].Skills == null)
                {
                    Weapons[SelectedIndex].Skills = new List<SkillData>();
                }

                Weapons[SelectedIndex].Skills.Add(SkillToEdit);

            }

            if (Weapons[SelectedIndex].Skills.Count > 0)
            {
                for (int i = 0; i < Weapons[SelectedIndex].Skills.Count; i++)
                {
                    SkillID = EditorGUILayout.Popup(SkillID, SkillNames.ToArray());
                    Weapons[SelectedIndex].Skills[i] = _Skills[SkillID];
                }
            }


            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            string Data = JsonConvert.SerializeObject(Weapons);
            File.WriteAllText(FilePath, Data);
        }
        GUILayout.EndHorizontal();
    }
}