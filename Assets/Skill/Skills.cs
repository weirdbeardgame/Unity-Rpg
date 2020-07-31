using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


public class Skills : MonoBehaviour
{
    Dictionary<int, SkillData> _Skills;

    string FilePath = "Assets/Skill/skills.json";
    string JsonData;

    // So, What I need is a method of determining time to learning.
    // I need a method of tracking of who Skill can be applied to.
    // Do Blueflames or anything under that type fit under the guise of Magic?
    int TimeToLearn = 0; // This should be based on level in all actuallity like it is in Final Fantasy IX.


    // Start is called before the first frame update
    void Start()
    {
        _Skills = new Dictionary<int, SkillData>();
        instantiate();
    }

    void instantiate()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            _Skills = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(JsonData);
        }
    }

    public SkillData GetSkill(int i)
    {
        return _Skills[i];
    }


    // Update is called once per frame
    void Update()
    {
        // Check for mastery if it's learnt add to main skill tree else it's temporary.
    }
}
