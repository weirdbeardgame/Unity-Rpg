using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using questing;
using System.IO;
using TMPro;


//The bulk of the action is based around reading a simplistic file and rendering it
// We need buttions yes no okay maybe etc. and a way to act upon those, like a tree structure for instance.

// Let's break this down. I have a JSON file that get's parsed into I run through the Tree per Conversation or Question left or right etc.
// For Events I need to just have a text buffer that can be passed to manually. "I think I heard a switch somewhere!"

enum DialogueType { CONVERSATION, QUESTION, EVENT };

public class DialogueManager : MonoBehaviour
{
    string FilePath;
    string JsonParsed;
    string CurrentLine;

    int Index;

    int NpcId;
    int QuestDialogue; // For Quest events IE. Tutorials

    int TreeID = 0;

    gameStateMessage stateMessage;
    bool Talking = false;

    public TextMeshProUGUI rendering;
    public TextMeshProUGUI Name;
    public GameObject Speaker;


    public Canvas canvas;
    List<BinarySearchTree<DialogueMessage>> DialougeData;
    BinarySearchTree<DialogueMessage> ScratchPad;
    BinarySearchTree<DialogueMessage> DialogueTree;

    List<List<DialogueMessage>> Temp;

    NPCManager NPC;

    QuestFlag _QuestFlag;

    private StateMachine _Machine;

    QuestManager Quests;
    questBook Book;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        FilePath = "Assets/Dialogue/Dialogue.json";
        stateMessage = ScriptableObject.CreateInstance<gameStateMessage>() as gameStateMessage;
        DialougeData = new List<BinarySearchTree<DialogueMessage>>();
        _Machine = FindObjectOfType<StateMachine>();
        NPC = FindObjectOfType<NPCManager>();
        Quests = FindObjectOfType<QuestManager>();
        Book = FindObjectOfType<questBook>();

        if (File.Exists(FilePath))
        {

            Temp = new List<List<DialogueMessage>>();

            JsonParsed = File.ReadAllText(FilePath);

            if (File.Exists(FilePath))
            {
                JsonParsed = File.ReadAllText(FilePath);

                Temp = JsonConvert.DeserializeObject<List<List<DialogueMessage>>>(JsonParsed, new TreeSerialize<List<List<DialogueMessage>>>());

                for (int i = 0; i < Temp.Count; i++)
                {
                    DialogueTree = new BinarySearchTree<DialogueMessage>();

                    List<DialogueMessage> LoopThrough = Temp[i];

                    for (int j = 0; j < LoopThrough.Count; j++)
                    {
                        DialogueTree.Insert(LoopThrough[j]); // An attempt to construct the tree itself.
                    }

                    TreeID = i;
                    DialougeData.Add(DialogueTree); // Saving the currently constructed tree
                }
            }
        }
    }

    IEnumerator AnimateText()
    {
        rendering.text = "";

        if (CurrentLine != null)
        {
            foreach (char Item in CurrentLine.ToCharArray())
            {
                rendering.text += Item;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void NextLine(DialogueMessage Node)
    {
        StopAllCoroutines();
        CurrentLine = null;
        rendering.text = null;

        if (Node == null)
        {
            Close();
        }

        else if (Node != null)
        {
            CurrentLine = Node.Line;
            StartCoroutine(AnimateText());
        }
    }

    public void Close()
    {
        rendering.enabled = false;
        rendering.text = null;
        Name.text = null;
        canvas.enabled = false;
        Talking = false;
        stateMessage.construct(States.MAIN);
    }

    public void OpenDialogueBox(int Location)
    {
        Index = Location;

        ScratchPad = new BinarySearchTree<DialogueMessage>();

        ScratchPad.Tree = DialougeData[Index].Tree;

        if (ScratchPad.Tree.Data.NodeT == NodeType.FLAG)
        {
            ScratchPad.Tree = ScratchPad.Tree.Right;
            NextLine(ScratchPad.Tree.Data);
        }

        rendering.enabled = true;
        canvas.enabled = true;
        Name.enabled = true;
        Talking = true;
        NextLine(ScratchPad.Tree.Data);
    }

    public void Talk(int ID) // Flags?
    {
        for (int i = 0; i < DialougeData.Count; i++)
        {
            if (NPC.NPC[ID].HasQuest)
            {
                OpenDialogueBox(NPC.NPC[ID].QuestID);
                Book.Give(Quests.Get(NPC.NPC[ID].QuestID));
                Debug.Log("Quest " + NPC.NPC[ID].QuestID.ToString() + " Given");
                NPC.NPC[ID].HasQuest = false;
            }

            if (DialougeData[i].Tree.Data.NodeT == NodeType.FLAG)
            {
                if (DialougeData[i].Tree.Data.NpcId == ID && DialougeData[i].Tree.Data.Flag.Flag == _Machine.CurrrentFlag.Flag)
                {
                    Debug.Log("Incolent fool. Submit!");

                    OpenDialogueBox(i);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Talking)
        {
            if (Input.GetButtonDown("Submit"))
            {
                CurrentLine = null;
                rendering.text = null;
                ScratchPad.Tree = ScratchPad.Tree.Right;

                Debug.Log("Incolent fool. Submit!");

                if (ScratchPad.Tree == null)
                {
                    Close();
                }
                else
                {
                    NextLine(ScratchPad.Tree.Data);
                }
            }
        }

    }
}
