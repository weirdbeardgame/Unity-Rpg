using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using questing;
using menu;

public class QuestWidget : Widget
{
    QuestBook book;
    QuestData data;

    public void setQuest(QuestData quest)
    {
        data = quest;
        Text.GetComponent<TextMeshProUGUI>().text = data.QuestName;
    }
    // Need Description getter?
    public override void Execute()
    {
        base.Execute();
        if (!book.IsActive(data))
        {
            book.Activate(data);
        }
        else if (book.IsActive(data))
        {
            book.DeActivate(data);
        }
    }
}
