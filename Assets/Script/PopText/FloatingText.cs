using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingText : MonoBehaviour {
	private Animator myAnimator;

	public Text dmgText;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponentInChildren<Animator> ();
		AnimatorClipInfo[] clip = myAnimator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clip [0].clip.length);

	}
	void Update(){
		if (Time.timeScale == 0) {
			Destroy (this.gameObject);
		}
	}
	public void SetText(string dmg){
		dmgText.text = dmg;
	}

}
