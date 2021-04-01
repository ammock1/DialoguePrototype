using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueNamespace;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Transform dialoguePanel;
    [SerializeField] Text textbox;
    [SerializeField] Text namebox;
    [SerializeField] Transform buttonPanel;
    List<Button> dynaButtons = new List<Button>();
    Dialogue dialogue;
    Line curLine;
    private bool isDialogueInProgress;

    // Start is called before the first frame update

    void Start()
    {
        /*loadDialogue();
        continueButton.onClick.AddListener(displayLine);
        displayLine();*/
        isDialogueInProgress = false;
        Debug.Log("Dialogue Controller active");
    }

   /* void loadDialogue()
    {
        dialogue = new JSONReader("Dialogues/bean").read();
        curLine = dialogue.getLine(0);
    }*/

    public void playDialogue(string filename)
    {
        if (isDialogueInProgress)
        {
            Debug.Log("Dialogue already in progress, must complete it first!");
            return;
        }
        isDialogueInProgress = true;
        Debug.Log("Playing dialogue: " + filename);
        dialogue = new JSONReader(filename).read();
        curLine = dialogue.getLine(0);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(displayLine);
        displayLine();
        dialoguePanel.gameObject.SetActive(true);
    }
    void displayLine()
    {
        //display current line
        textbox.text = curLine.getContent();
        namebox.text = curLine.getSpeaker();

        //prepare next line
        List<int> links = curLine.getNextLines();
        if (links.Count == 1)
        {
            //get next line ready
            curLine = dialogue.getLine(links[0]);
            continueButton.gameObject.SetActive(true);
        }
        else if(links.Count > 1)
        {
            //set up buttons to choose next line
            Debug.Log("Branch reached");
            for(int i = 0; i < links.Count; i++)
            {
                int j = links[i];
                Line opt = dialogue.getLine(j);
                //Debug.Log("Option " + (i + 1) + ": " + opt.getTip());
                Button tmpButton = Instantiate(continueButton, buttonPanel);
                Debug.Log("Added a button");
                tmpButton.GetComponentInChildren<Text>().text = opt.getTip();
                tmpButton.onClick.RemoveAllListeners();
                tmpButton.onClick.AddListener(() => { selectLine(j); });
                dynaButtons.Add(tmpButton);
            }
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            //no next line
            Debug.Log("Dialogue complete");
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(stopDialogue);
            //continueButton.gameObject.SetActive(false);
        }
    }

    void selectLine(int i)
    {
        Debug.Log("Should display line #" + i);
        curLine = dialogue.getLine(i);
        for(int j = 0; j < dynaButtons.Count; j++)
        {
            Destroy(dynaButtons[j].gameObject);
            Debug.Log("Destroyed a button");
        }
        dynaButtons.Clear();
        displayLine();
    }

    public void stopDialogue()
    {
        dialoguePanel.gameObject.SetActive(false);
        isDialogueInProgress = false;
    }
    // Update is called once per frame
 /*   void Update()
    {
        
    }*/
}
