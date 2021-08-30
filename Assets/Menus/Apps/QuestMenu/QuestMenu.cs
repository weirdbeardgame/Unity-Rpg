using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;
using questing;

// This could either be a grid or a list!

public class QuestMenu : AppData
{
    QuestBook book;
    QuestWidget questWidget;
    // Start is called before the first frame update
    public override void Init()
    {
        appID = 3;
        appName = "QuestBook";

        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.APP);
        properties.Add(MenuProperties.INPUT);

        display = MenuDisplay.LIST;

        for (int i = 0; i < book.obtainedQuests.Count; i++)
        {
            // Summon widgets based on Quests in book
            questWidget = (QuestWidget)Instantiate(widgetsToAdd[0]);
            questWidget.setQuest(book.obtainedQuests[i]);
            AddWidget(questWidget);
        }
    }
}
