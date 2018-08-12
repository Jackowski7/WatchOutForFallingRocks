using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{

    GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        int layerUI = LayerMask.GetMask("UI");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerUI))
        {
            if (hit.transform.tag == "Button")
            {
                ButtonHighlight buttonHighlight = hit.transform.GetComponent<ButtonHighlight>();
                buttonHighlight.Highlight = true;
            }

            if (Input.GetMouseButtonDown(0) == true)
            {
                if (hit.transform.name == "StartGame")
                {
                    gameManager.StartGame();
                }
                if (hit.transform.name == "NextLevel")
                {
                    gameManager.StartLevel();
                }
                if (hit.transform.name == "RestartLevel")
                {
                    gameManager.RestartLevel();
                }
                if (hit.transform.name == "EndGame")
                {
                    gameManager.NewGame();
                }
            }
        }


    }
}
