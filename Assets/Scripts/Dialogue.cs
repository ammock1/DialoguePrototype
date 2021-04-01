using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueNamespace
{
    [System.Serializable]
    public class Dialogue
    {
        public List<Line> lines = new List<Line>();
        //private Line firstLine = new Line();

        public Dialogue()
        {

        }
        public void pointlessMethod()
        {
            Debug.Log("beep boop shedoop");
        }

        public void setFirstLine(Line line)
        {
            //firstLine = line;
        }

        public void addLine(Line line)
        {
            if(lines.Count != line.getId())
            {
                Debug.Log("Error: line id mismatch");
                return;
            }
            lines.Add(line);
        }

        public Line getLine(int x)
        {
            return lines[x];
        }

        /*public Line getFirstLine()
        {
            return firstLine;
        }*/

        public string getJSON()
        {
            string result = "";
            foreach(Line line in lines)
            {
                result = result + JsonUtility.ToJson(line);
            }
            return result;
        }
    }

    [System.Serializable]
    public class Line
    {
        public int id;
        //tip is presented when player is making a choice
        public string tip = "";
        //who's saying the line
        public string speaker = "";
        //the line itself
        public string content = "";
        //links to the next lines
        public List<int> nextLines = new List<int>();
        //private List<Line> nextLines = new List<Line>();

        public Line() { }

        public Line(int id, string speaker, string content)
        {
            this.id = id;
            this.speaker = speaker;
            this.content = content;
        }

        public Line(int id, string speaker, string content, string tip)
        {
            this.id = id;
            this.speaker = speaker;
            this.content = content;
            this.tip = tip;
        }

        public void addLineLink(int link)
        {
            nextLines.Add(link);
        }

        public int getId()
        {
            return this.id;
        }

        public string getTip()
        {
            return this.tip;
        }

        public string getSpeaker()
        {
            return this.speaker;
        }

        public string getContent()
        {
            return this.content;
        }

        public bool hasNext()
        {
            return nextLines.Count > 0;
        }

       /* public Line getNext()
        {
            return nextLines[0];
        }*/

        public List<int> getNextLines()
        {
            return nextLines;
        }
    }
}
