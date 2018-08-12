using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{

    public Color BaseColor;
    public Color HighlightColor;
    public bool Highlight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Highlight == true)
        {
            GetComponent<Image>().color = HighlightColor;

        }
        else
        {
            GetComponent<Image>().color = BaseColor;
        }

        Highlight = false;
    }
}
