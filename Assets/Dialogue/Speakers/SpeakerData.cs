using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "New Speaker")]
public class SpeakerData : ScriptableObject
{
    public int SpeakerID;
    public string SpeakerName;
    public GameObject Prefab;
}
