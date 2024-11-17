using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPanelScript : MonoBehaviour {

	void OnEnable(){
		CharacterSelectionManager.Instance.MainCamera.SetActive (false);
		CharacterSelectionManager.Instance.ChestViewerCamera.SetActive (true);

	}

	public void DisableChestPanel(){
		CharacterSelectionManager.Instance.MainCamera.SetActive (true);
		CharacterSelectionManager.Instance.ChestViewerCamera.SetActive (false);
	}
}
