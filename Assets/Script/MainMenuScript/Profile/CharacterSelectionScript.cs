using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectionScript : MonoBehaviour {

	[SerializeField]
	private Text charNameText;
	private int characterIndex;
	void OnEnable() {
		checkCharacterSelectionPrefabs ();

	}
	private void Update(){
		if (CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex] != null) {
			CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].transform.Rotate (new Vector3 (0f, 10f * Time.deltaTime, 0f));
		}
	}

	private void checkCharacterSelectionPrefabs(){
		characterIndex = PlayerPrefs.HasKey ("CharacterIndex") ? PlayerPrefs.GetInt ("CharacterIndex") : 0;
		CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].SetActive (true);
		CharacterSelectionManager.Instance.MainCamera.SetActive (false);
		CharacterSelectionManager.Instance.CharacterSelectionCamera.SetActive (true);
		charNameText.text = CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].GetComponent<CharacterName> ().CharName;

	}
	public void NextCharacter(){
		characterIndex = PlayerPrefs.HasKey ("CharacterIndex") ? PlayerPrefs.GetInt ("CharacterIndex") : 0;
		CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].SetActive (false);
		if (characterIndex == CharacterSelectionManager.Instance.characterSelectionPrefabs.Length - 1) {
			characterIndex = 0;
		} else {
			characterIndex++;
		}
		SaveCharacterIndex (characterIndex);
		CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].SetActive (true);
		charNameText.text = CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].GetComponent<CharacterName> ().CharName;
	}
	public void PrevCharacter(){
		characterIndex = PlayerPrefs.HasKey ("CharacterIndex") ? PlayerPrefs.GetInt ("CharacterIndex") : 0;
		CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].SetActive (false);
		if (characterIndex == 0) {
			characterIndex = CharacterSelectionManager.Instance.characterSelectionPrefabs.Length - 1;

		} else {
			characterIndex--;
		}
		SaveCharacterIndex (characterIndex);
		CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].SetActive (true);
		charNameText.text = CharacterSelectionManager.Instance.characterSelectionPrefabs [characterIndex].GetComponent<CharacterName> ().CharName;
	}
	private void SaveCharacterIndex(int newIndex){
		PlayerPrefs.SetInt ("CharacterIndex", newIndex);
	}

	public void DisableCharacterSelectionCamera(){
	
	
		CharacterSelectionManager.Instance.CharacterSelectionCamera.SetActive (false);
		CharacterSelectionManager.Instance.MainCamera.SetActive (true);
	}
}
