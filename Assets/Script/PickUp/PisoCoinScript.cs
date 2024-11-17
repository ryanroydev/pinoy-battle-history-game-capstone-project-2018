using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PisoCoinScript : MonoBehaviour {
	[SerializeField] private float spinSpeed;
	[SerializeField]private Animator myAnimator;
	[SerializeField]private int value;
	[SerializeField]private TextMesh timeText;
	float timeDur = 0;
	float currenttime = 20;

	bool isUse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, spinSpeed * Time.deltaTime, 0, Space.World);
		timeText.transform.Rotate (0, -spinSpeed * Time.deltaTime, 0, Space.World);


		if (currenttime <= 4) {
			timeText.color = Color.red;
			timeText.text = Mathf.FloorToInt (currenttime).ToString ();
		} else if (currenttime <= 6) {

			timeText.text = Mathf.FloorToInt (currenttime).ToString ();
		} else {
			timeText.text = "";
		}

		currenttime -= Time.deltaTime;

	
		if (currenttime <= timeDur || Time.timeScale == 0) {
			Destroy (this.gameObject);
		}

	}
	void OnMouseDown(){
		if (!isUse) {
			myAnimator.SetTrigger ("PickUp");
			StatsManager.Instance.addGold (value);
			isUse = true;
		}

	}
	public void PickUp(){
		Destroy (this.gameObject);
	}
}
