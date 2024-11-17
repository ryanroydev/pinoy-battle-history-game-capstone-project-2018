using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrphicsSetting : MonoBehaviour {
	[SerializeField]
	private Dropdown graphicsDropDown;
	void OnEnable(){
		graphicsDropDown.onValueChanged.RemoveAllListeners();
		graphicsDropDown.onValueChanged.AddListener (delegate {setGraphics ();});

	}
	// Use this for initialization
	void Start () {
		
		int value = PlayerPrefs.GetInt ("CurrentGraphics", 4);
		graphicsDropDown.value = value;
		if (value == 0) {
			QualitySettings.SetQualityLevel (graphicsDropDown.value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void setGraphics(){

		PlayerPrefs.SetInt ("CurrentGraphics", graphicsDropDown.value);

		QualitySettings.SetQualityLevel (graphicsDropDown.value);
	}
}
