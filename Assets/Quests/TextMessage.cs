using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct TextMessages
{
    int ID;
    string SenderName;
    string Message;

}



public class TextMessage : MonoBehaviour
{
    // A future system involving Musungo's phone. He'll recieve text's based on progress of story
    // To progress of side quests. The messages can be hints to new objectives.

    List<TextMessages> texts;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
