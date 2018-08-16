using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[ExecuteInEditMode]
public class VRToggle : MonoBehaviour
{

	public bool VRMode;

	void Update()
	{
		if (VRMode == false)
		{
			//XRSettings.enabled = false;
			UnityEditor.PlayerSettings.SetVirtualRealitySupported(UnityEditor.BuildTargetGroup.Standalone, false);
		}
		else
		{
			//XRSettings.enabled = true;
			UnityEditor.PlayerSettings.SetVirtualRealitySupported(UnityEditor.BuildTargetGroup.Standalone, true);
		}
	}
}

