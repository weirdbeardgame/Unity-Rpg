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
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.ITEM)
                {
                    ItemData temp = (ItemData)asset.Value.Data;
                    items.Add(temp.itemID, temp);
                }
            }
        }
        isInitalized = true;
    }

    public void readJson()
    {
        items = new Dictionary<int, ItemData>();

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            items = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(JsonData);
            itemIndex = items.Count;

            isInitalized = true;
        }
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

            CurrentItem = ScriptableObject.CreateInstance<ItemData>();
            CurrentItem.name = ItemName;
            CurrentItem.effect = new ItemBuffer();
            CurrentItem.effect.buff = new Stats();
            items.Add(itemIndex, CurrentItem);
            string data = JsonConvert.SerializeObject(items);
            File.WriteAllText(FilePath, data);
        }
        GUILayout.EndArea();

        if (items != null)
        {
            for (int i = 0; i < items.Count; i++)
            {

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

                if (GUILayout.Button(items[i].name))
                {
                    SelectedIndex = i;
                    Debug.Log("User Clicked " + items[i].name);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }
            GUILayout.BeginArea(PropertyPage);
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Item ID");
            items[SelectedIndex].itemID = EditorGUILayout.IntField(items[SelectedIndex].itemID);

            EditorGUILayout.LabelField("Item Name");
            items[SelectedIndex].name = EditorGUILayout.TextField(items[SelectedIndex].name);

            EditorGUILayout.LabelField("Description");
            items[SelectedIndex].description = EditorGUILayout.TextArea(items[SelectedIndex].description);

            EditorGUILayout.LabelField("Buffer Type");
            items[SelectedIndex].effect.type = (ItemType)EditorGUILayout.EnumPopup(items[SelectedIndex].effect.type);

            EditorGUILayout.LabelField("Stat Affected");
            items[SelectedIndex].effect.effect = (AreaOfEffect)EditorGUILayout.EnumPopup(items[SelectedIndex].effect.effect);

            EditorGUILayout.LabelField("Buffer: ");
            items[SelectedIndex].effect.buff.stat = EditorGUILayout.FloatField(items[SelectedIndex].effect.buff.stat);

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            for (int i = 0; i < items.Count; i++)
            manager.AddAsset(new Asset(items[i], AssetType.ITEM), items[i].name);
        }
        GUILayout.EndHorizontal();
    }
}
