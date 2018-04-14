using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set60FPS : MonoBehaviour {
	void Awake ()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
