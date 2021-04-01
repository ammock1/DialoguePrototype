using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueNamespace;

public class JSONReader
{
    public TextAsset jsonFile;
    // Start is called before the first frame update

    public JSONReader (string filename)
    {
        this.jsonFile = Resources.Load<TextAsset>(filename);

    }

    public Dialogue read()
    {
        Debug.Log("Raw text: " + jsonFile.text);
       
        Dialogue dialogue = JsonUtility.FromJson<Dialogue>(jsonFile.text);
        /*foreach (Line line in dialogue.lines)
        {
            Debug.Log("Found line: " + line.getContent());
        }*/
        return dialogue;
    }
}
