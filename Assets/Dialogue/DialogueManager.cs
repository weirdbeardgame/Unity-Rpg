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

    gameStateMessage StateMessage;
    Messaging Messenger;
    bool Talking = false;

    public TextMeshProUGUI rendering;
    public TextMeshProUGUI Name;

    GameObject SpeakerProfile;
    GameObject Speaker;


    public Canvas canvas;
    List<BinarySearchTree<DialogueMessage>> DialougeData;
    BinarySearchTree<DialogueMessage> ScratchPad;
    BinarySearchTree<DialogueMessage> DialogueTree;

    List<List<DialogueMessage>> Temp;

    QuestFlag _QuestFlag;

    private StateMachine _Machine;

    QuestManager Quests;
    questBook Book;

    NPCManager manager;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        FilePath = "Assets/Dialogue/Dialogue.json";
        StateMessage = new gameStateMessage();
        DialougeData = new List<BinarySearchTree<DialogueMessage>>();
        _Machine = FindObjectOfType<StateMachine>();
        Quests = FindObjectOfType<QuestManager>();
        Book = FindObjectOfType<questBook>();
        manager = FindObjectOfType<NPCManager>();

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

    public void NextNode(TNode<DialogueMessage> Node)
    {
        StopAllCoroutines();
        CurrentLine = null;
        rendering.text = null;

        if (Node == null)
        {
            Close();
        }
        else
        {
            switch (Node.Data.NodeT)
            {
                case NodeType.FLAG:
                    switch (Node.Data.FlagType)
                    {
                        case FlagReqSet.REQUIRED:
                            if (Node.Data.Flag == _Machine.CurrrentFlag)
                            {
                                //Debug.Log("Correct FLAG");
                                NextNode(Node.Right); // Assume all normal Dialouge will be to the right.
                            }

                            if (Node.Data.Flag != _Machine.CurrrentFlag)
                            {
                                //Debug.Log("Wrong FLAG);
                                return; // Nothing
                            }
                            break;
                        case FlagReqSet.SET: 
                            StateMessage.construct(_Machine.State, Node.Data.Flag);
                            Messenger = FindObjectOfType<Messaging>();
                            Messenger.Enqueue(StateMessage);
                            break;
                    }
                    break;

                case NodeType.DIALOUGE:
                    CurrentLine = Node.Data.Line;
                    // Get next Speaker
                    GetNextSpeaker(Node);

                    StartCoroutine(AnimateText());
                    break;

                case NodeType.CHOICE:
                    // Open associated Dialouge then draw button choices. Traverse tree from there
                    break;

                case NodeType.EVENT:
                    //Play animation, Control basic events like Give Item etc.
                    break;

            }
        }
    }

    void GetNextSpeaker(TNode<DialogueMessage> N)
    {
        Name.text = N.Data.SpeakerID.NpcName;

        if (Speaker != null)
        {
            Destroy(Speaker);
        }

        GameObject Canvas = GameObject.Find("Dialogue");

        SpeakerProfile = GameObject.Find("Speaker");
        
        Speaker = Instantiate(manager.ConstructedNPC[N.Data.SpeakerID.NpcID].CurrentSpeaker);
        Speaker.transform.SetParent(Canvas.transform);
        Speaker.transform.localPosition = SpeakerProfile.transform.localPosition;
        Speaker.transform.localScale = new Vector2(200, 200);
        Speaker.GetComponent<SpriteRenderer>().sortingOrder = 3; 
    }

    public void Close()
    {
        rendering.enabled = false;
        rendering.text = null;
        Name.text = null;
        canvas.enabled = false;
        Talking = false;
        Destroy(Speaker);
        StateMessage.construct(States.MAIN, _Machine.CurrrentFlag);
        Messenger = FindObjectOfType<Messaging>();
        Messenger.Enqueue(StateMessage);
    }

    public void OpenDialogueBox(int Location)
    {
        Index = Location;

        ScratchPad = new BinarySearchTree<DialogueMessage>();

        ScratchPad.Tree = DialougeData[Index].Tree;

        rendering.enabled = true;
        canvas.enabled = true;
        Name.enabled = true;
        Talking = true;
        NextNode(ScratchPad.Tree);
    }
    public void OpenDialogueBox(BinarySearchTree<DialogueMessage> Tree)
    {         
        rendering.enabled = true;            
        canvas.enabled = true;        
        Name.enabled = true;                    
        Talking = true;
        ScratchPad = Tree;     
        NextNode(ScratchPad.Tree);
        return;
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

                if (ScratchPad.Tree.Right == null)
                {
                    Close();
                }

                ScratchPad.Tree = ScratchPad.Tree.Right;

                //Debug.Log("Incolent fool. Submit!");

                if (ScratchPad.Tree == null)
                {
                    Close();
                }
                else
                {
                    NextNode(ScratchPad.Tree);
                }
            }
        }

    }
}
