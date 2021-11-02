using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class ItemEditor : EditorWindow
{
    public ItemData CurrentItem;
    ItemData temp;
    Creature creature;

    string JsonData;
    string ItemName;
    string FilePath = "Assets/Items.json";

    public Dictionary<int, ItemData> items;

    GameAssetManager manager;

    int SelectedIndex = 0;
    int ItemID;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    SerializedProperty currentProperty;

    bool isInitalized;

    int itemIndex;

    [MenuItem("Window/Item")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("New Item");
    }
    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };


    public void Init()
    {
        manager = GameAssetManager.Instance;
        items = new Dictionary<int, ItemData>();
        if (manager.isFilled())
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value is ItemData)
                {
                    ItemData temp = (ItemData)asset.Value;
                    items.Add(itemIndex, temp);
                    itemIndex += 1;
                }
            }
        }
        isInitalized = true;
    }

    void createPrefab()
    {
        CurrentItem.prefab = new GameObject();
        CurrentItem.prefab.AddComponent<Button>();
        CurrentItem.prefab.AddComponent<Image>(); // To set an icon on each item button
        //CurrentItem.prefab.AddComponent<InvetoryWidget>(); // To actually execute and apply the currently selected item
        //CurrentItem.prefab.GetComponent<Button>().onClick
        CurrentItem.prefab.name = CurrentItem.name;

        if (!Directory.Exists("Assets/Resources/Prefabs/Items/"))
        {
            Directory.CreateDirectory("Assets/Resources/Prefabs/Items/");
        }

        PrefabUtility.SaveAsPrefabAsset(CurrentItem.prefab, ("Assets/Resources/Prefabs/Items/" + CurrentItem.name + ".prefab"));
        CurrentItem.prefabPath = ("Prefabs/Items/" + CurrentItem.name);
        CurrentItem.prefab.SetActive(false);
    }

    void OnGUI()
    {
        if (!isInitalized)
        {
            Init();
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
        itemIndex = EditorGUILayout.IntField(itemIndex);

        if (GUILayout.Button("New Item"))
        {
            if (items == null)
            {
                items = new Dictionary<int, ItemData>();
            }

            CurrentItem = new ItemData();
            CurrentItem.name = ItemName;
            CurrentItem.effect = new ItemBuffer();
            CurrentItem.effect.buff = new Stats();
            items.Add(itemIndex, CurrentItem);
            createPrefab();
            itemIndex += 1;
        }
        GUILayout.EndArea();

        if (items != null && items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(items[i].name))
                {
                    SelectedIndex = i;
                    CurrentItem = items[SelectedIndex];
                    Debug.Log("User Clicked " + items[i].name);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            if (CurrentItem != null)
            {
                EditorGUILayout.LabelField("Item ID");
                CurrentItem.itemID = EditorGUILayout.IntField(CurrentItem.itemID);

                EditorGUILayout.LabelField("Item Name");
                CurrentItem.name = EditorGUILayout.TextField(CurrentItem.name);

                EditorGUILayout.LabelField("Description");
                CurrentItem.description = EditorGUILayout.TextArea(CurrentItem.description);

                EditorGUILayout.LabelField("Buffer Type");
                CurrentItem.effect.type = (ItemType)EditorGUILayout.EnumPopup(CurrentItem.effect.type);

                EditorGUILayout.LabelField("Stat Affected");
                CurrentItem.effect.effect = (AreaOfEffect)EditorGUILayout.EnumPopup(CurrentItem.effect.effect);

                EditorGUILayout.LabelField("Buffer: ");
                CurrentItem.effect.buff.stat = EditorGUILayout.FloatField(CurrentItem.effect.buff.stat);
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            for (int i = 0; i < items.Count; i++)
            manager.AddAsset(items[i], items[i].name);
        }
        GUILayout.EndHorizontal();
    }
}
