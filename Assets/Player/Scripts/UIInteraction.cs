using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;



public class UIInteraction : MonoBehaviour
{
	GameManager gameManager;

	int layerUI;

	public Transform NonVRLeftHand;
	public Transform NonVRRightHand;
	public Transform VRLeftHand;
	public Transform VRRightHand;

	public GameObject leftHand;
	public GameObject rightHand;

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		layerUI = LayerMask.GetMask("UI");

		if (gameManager.VRMode == true) //vr controls
		{
			XRSettings.enabled = true;
			transform.Find("NonVR").transform.gameObject.SetActive(false);
			transform.Find("VR").transform.gameObject.SetActive(true);
			leftHand.transform.parent = VRLeftHand;
			rightHand.transform.parent = VRRightHand;
			leftHand.transform.localPosition = Vector3.zero;
			rightHand.transform.localPosition = Vector3.zero;
		}
		else // non vr controls
		{
			XRSettings.enabled = false;
			transform.Find("NonVR").transform.gameObject.SetActive(true);
			transform.Find("VR").transform.gameObject.SetActive(false);
			leftHand.transform.parent = NonVRLeftHand;
			rightHand.transform.parent = NonVRRightHand;
			leftHand.transform.localPosition = Vector3.zero;
			rightHand.transform.localPosition = Vector3.zero;
			leftHand.transform.Find("Gun").Find("ModelCenter").Find("Model").transform.rotation = Quaternion.Euler(Vector3.zero);
			rightHand.transform.Find("Gun").Find("ModelCenter").Find("Model").transform.rotation = Quaternion.Euler(Vector3.zero);
		}

	}

	// Update is called once per frame
	void Update()
	{

		if (gameManager.VRMode == true) //vr controls
		{

			Transform LrayOrgin;
			LrayOrgin = leftHand.transform.Find("Gun").Find("ModelCenter").Find("Model").Find("BarrelEnd").transform;
			RaycastHit Lhit;
			if (Physics.Raycast(LrayOrgin.position, LrayOrgin.forward, out Lhit, 100f, layerUI))
			{
				if (Lhit.transform.tag == "Button")
				{
					ButtonHighlight buttonHighlight = Lhit.transform.GetComponent<ButtonHighlight>();
					buttonHighlight.Highlight = true;
				}

				if ( SteamVR_Controller.Input(1).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) == true )
				{

					if (Lhit.transform.name == "StartGame")
					{
						gameManager.StartGame();
					}
					if (Lhit.transform.name == "NextLevel")
					{
						gameManager.StartLevel();
					}
					if (Lhit.transform.name == "RestartLevel")
					{
						gameManager.RestartLevel();
					}
					if (Lhit.transform.name == "EndGame")
					{
						gameManager.NewGame();
					}
				}
			}

			Transform RrayOrgin;
			RrayOrgin = rightHand.transform.Find("Gun").Find("ModelCenter").Find("Model").Find("BarrelEnd").transform;
			RaycastHit Rhit;
			if (Physics.Raycast(RrayOrgin.position, RrayOrgin.forward, out Rhit, 100f, layerUI))
			{
				if (Rhit.transform.tag == "Button")
				{
					ButtonHighlight buttonHighlight = Rhit.transform.GetComponent<ButtonHighlight>();
					buttonHighlight.Highlight = true;
				}

				if ( SteamVR_Controller.Input(2).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) == true )
				{
					if (Rhit.transform.name == "StartGame")
					{
						gameManager.StartGame();
					}
					if (Rhit.transform.name == "NextLevel")
					{
						gameManager.StartLevel();
					}
					if (Rhit.transform.name == "RestartLevel")
					{
						gameManager.RestartLevel();
					}
					if (Rhit.transform.name == "EndGame")
					{
						gameManager.NewGame();
					}
				}
			}

		}
		else // non vr controls
		{

			Transform rayOrgin;
			rayOrgin = transform.Find("NonVR").Find("Head").transform;
			RaycastHit hit;
			if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out hit, 100f, layerUI))
			{
				if (hit.transform.tag == "Button")
				{
					ButtonHighlight buttonHighlight = hit.transform.GetComponent<ButtonHighlight>();
					buttonHighlight.Highlight = true;
				}

				if (Input.GetButtonDown("Fire1"))
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
}
