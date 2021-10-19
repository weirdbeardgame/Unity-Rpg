using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The below is for normal Dialogue in scene. Signs to be read. Explanation of scene IE. The door appears to be locked
// Does this even need the Dialogue editor? I don't imagine this'll get to some complex converstations.
public class SceneDialogue : MonoBehaviour
{
    DialogueManager manager;
    // Need to call Dialogue editor on this here boy
    BinarySearchTree<DialogueMessage> dialogue;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown("Submit"))
            {
                // Open Dialogue
                manager.OpenDialogueBox(dialogue);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
