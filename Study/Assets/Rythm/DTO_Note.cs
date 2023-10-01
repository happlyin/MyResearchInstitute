using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DTO_Note : MonoBehaviour
{
    public List<int> spwanLines = new List<int>();
    public float SpwanTime = 0;

    public DTO_Note() { }

    public DTO_Note(int spwanLine, int SpwanTime)
    {
        this.spwanLines = new List<int>() { spwanLine };
        this.SpwanTime = SpwanTime;
    }

    public DTO_Note(List<int> spwanLines, int SpwanTime)
    {
        this.spwanLines = spwanLines;
        this.SpwanTime = SpwanTime;
    }

    public DTO_Note(string NoteData)
    {
        string[] PressTime = NoteData.Split(':');
        this.SpwanTime = int.Parse(PressTime[1]);
        string[] Lines = PressTime[0].Split('/');
        foreach (string Line in Lines)
            spwanLines.Add(int.Parse(Line) - 1);
    }
}
