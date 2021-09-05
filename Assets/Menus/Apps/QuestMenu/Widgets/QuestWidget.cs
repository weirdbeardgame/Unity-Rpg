using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Questing;
using menu;

public class QuestWidget : Widget
{
    QuestBook book;
    QuestData data;

    public void setQuest(QuestData quest)
    {
        data = quest;
        Text.GetComponent<TextMeshProUGUI>().text = data.questName;
    }
    // Need Description getter?
    public override void Execute()
    {
        base.Execute();
    }
}
