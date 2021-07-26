using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class ItemEditor : EditorWindow
{

    public ItemData CurrentItem;
    Creature creature;
    string JsonData;
    string ItemName;
    string FilePath = "Assets/Items.json";
    public Dictionary<int, ItemData> Items;


    int SelectedIndex = 0;
    int ItemID;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    SerializedProperty currentProperty;

    bool IsInitalized;

    int ItemIndex;

    [MenuItem("Window/Item")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("New Item");
    }
    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };


    public void readJson()
    {
        Items = new Dictionary<int, ItemData>();

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Items = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(JsonData);
            ItemIndex = Items.Count;

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

        GUI.Box(PropertyPage, "Items");
        GUI.Box(ButtonList, "Buttons");

        GUILayout.BeginArea(TopProperties);

        GUILayout.Label("Item Name");
        ItemName = EditorGUILayout.TextField(ItemName);

        GUILayout.Label("Item ID");
        ItemIndex = EditorGUILayout.IntField(ItemIndex);

        if (GUILayout.Button("New Item"))
        {

            if (Items == null)
            {
                Items = new Dictionary<int, ItemData>();
            }

            CurrentItem = ScriptableObject.CreateInstance<ItemData>();
            CurrentItem.ItemName = ItemName;
            CurrentItem.Effect = new ItemBuffer();
            CurrentItem.Effect.Buff = new Stats();
            Items.Add(ItemIndex, CurrentItem);
            string Data = JsonConvert.SerializeObject(Items);
            File.WriteAllText(FilePath, Data);
        }
        GUILayout.EndArea();

        if (Items != null)
        {
            for (int i = 0; i < Items.Count; i++)
            {

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(Items[i].ItemName))
                {
                    SelectedIndex = i;
                    Debug.Log("User Clicked " + Items[i].ItemName);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }
            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Item ID");
            Items[SelectedIndex].ItemID = EditorGUILayout.IntField(Items[SelectedIndex].ItemID);
            EditorGUILayout.LabelField("Item Name");
            Items[SelectedIndex].ItemName = EditorGUILayout.TextField(Items[SelectedIndex].ItemName);
            EditorGUILayout.LabelField("Description");
            Items[SelectedIndex].ItemDescription = EditorGUILayout.TextArea(Items[SelectedIndex].ItemDescription);
            EditorGUILayout.LabelField("Buffer Type");
            Items[SelectedIndex].Effect.Type = (ItemType)EditorGUILayout.EnumPopup(Items[SelectedIndex].Effect.Type);
            EditorGUILayout.LabelField("Stat Affected");
            Items[SelectedIndex].Effect.Effect = (AreaOfEffect)EditorGUILayout.EnumPopup(Items[SelectedIndex].Effect.Effect);
            EditorGUILayout.LabelField("Buffer: ");
            Items[SelectedIndex].Effect.Buff.stat = EditorGUILayout.FloatField(Items[SelectedIndex].Effect.Buff.stat);
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            string Data = JsonConvert.SerializeObject(Items);
            File.WriteAllText(FilePath, Data);
        }
        GUILayout.EndHorizontal();
    }
}
