using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContoller : MonoBehaviour
{
    // Buttom Press & Not Press
    private Image Line;
    private Color Not_PressColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color PressColor = new Color(100.0f / 255.0f, 100.0f / 255.0f, 100.0f / 255.0f, 1.0f);
    public List<RectTransform> Notes = new List<RectTransform>();
    public RectTransform RT;

    // Key Down
    public KeyCode CheckPressKey;

    void Start()
    {
        Line = GetComponent<Image>();
    }

    void Update()
    {
        // Check Line Data is Null
        if (Line != null)
        {
            // Check Key Down
            if (Input.GetKeyDown(CheckPressKey))
            {
                Line.color = PressColor;
                if(Notes.Count >= 1)
                    switch (Notes[0].anchoredPosition.y)
                    {
                        case 1.0f:
                            break;
                        default:
                            break;
                    }
            }

            // Check Key Up
            if (Input.GetKeyUp(CheckPressKey))
                Line.color = Not_PressColor;
        }
        else
            Line = GetComponent<Image>();
    }
}
