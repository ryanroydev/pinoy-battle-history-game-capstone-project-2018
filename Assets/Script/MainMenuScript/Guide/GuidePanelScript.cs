using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePanelScript : MonoBehaviour {

	[SerializeField]
	private float[] ContentPosX; 
	[SerializeField]
	private float[] minimumX, maximumX;
	private int ContentIndex = 0;
	[SerializeField]
	private RectTransform content;
	private void checkIndex(){
		for (int i = 0; i < ContentPosX.Length; i++) {
			if (content.gameObject.transform.position.x <= minimumX [i] && content.gameObject.transform.position.x >= maximumX [i]) {
				ContentIndex = i;
			}
		}
	}
	public void NextBtn(){
		checkIndex ();

		if (ContentIndex == ContentPosX.Length - 1) {
			ContentIndex  = 0;
		} else {
			ContentIndex ++;
		}

		SetContentPosition (ContentIndex);
	}
	public void PrevBtn(){
		checkIndex ();

		if (ContentIndex == 0) {
			ContentIndex = ContentPosX.Length - 1;

		} else {
			ContentIndex--;
		}
		SetContentPosition (ContentIndex);
	}
	private void SetContentPosition(int index){
		content.position = new Vector3 (ContentPosX [index], content.position.y, content.position.z);
	

	}
}
