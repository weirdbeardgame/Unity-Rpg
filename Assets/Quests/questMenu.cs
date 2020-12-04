using System.Collections;
using System.Collections.Generic;
using questing;
using UnityEngine;
using menu;


public class questApp : AppData
{
    int context = 3;
    PScreen Manager;
    questBook book;
    GameObject slot;

    int index = 0;

    Material shader;

    Messaging questState;

    questMessage message;

    GameObject secondCanvas;
    GameObject secondPanel;

    // Start is called before the first frame update
    void Start()
    {
        book = FindObjectOfType<questBook>();

        questState = FindObjectOfType<Messaging>();

        Manager = FindObjectOfType<PScreen>();
    }

    public void Open()
    {

        if (book.Quests.Count > 0)
        {
            for (int i = 0; i < book.Quests.Count; i++)
            {
                Instantiate(slot);
            }
        }

        else
        {
            Instantiate(slot);
        }
    }

    public void Use(int i)
    {

    }

    public void SetShader(Material s)
    {
        shader = s;
    }

    public Material GetShader()
    {
        return shader;
    }

    void Enable()
    {
        message = new questMessage();
        book.Activate(index);
    }

    void Disable()
    {
        message = new questMessage();
        book.DeActivate(index);
    }

    void empty()
    {
        // A true void, but hey it's not null right?
    }

    public void ViewQuest()
    {
    }
}
