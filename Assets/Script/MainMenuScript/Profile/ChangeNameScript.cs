using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeNameScript : MonoBehaviour {

	[SerializeField]
	private Text myInputText;
	private void OnEnable(){
		CheckName ();
	
	}
	private void CheckName(){
		if (PlayerPrefs.HasKey ("PlayerName")) {
			myInputText.text = PlayerPrefs.GetString ("PlayerName");
		
		}
	}
	public void ChangetheName(){
		PlayerPrefs.SetString ("PlayerName", myInputText.text);
	}
}
