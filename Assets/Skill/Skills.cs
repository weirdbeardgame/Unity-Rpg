using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


public class Skills : MonoBehaviour
{
    Dictionary<int, SkillData> skills;

    string filePath = "Assets/Skill/skills.json";
    string jsonData;

    // So, What I need is a method of determining time to learning.
    // I need a method of tracking of who Skill can be applied to.
    // Do Blueflames or anything under that type fit under the guise of Magic?
    int TimeToLearn = 0; // This should be based on level in all actuallity like it is in Final Fantasy IX.


    // Start is called before the first frame update
    void Start()
    {
        skills = new Dictionary<int, SkillData>();
        instantiate();
    }

    void instantiate()
    {
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            skills = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(jsonData);
        }
    }

    public SkillData GetSkill(int i)
    {
        return skills[i];
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mastery if it's learnt add to main skill tree else it's temporary.
    }
}
