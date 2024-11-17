using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {

	[SerializeField] private float spinSpeed;
	[SerializeField]private Animator myAnimator;
	[SerializeField]private int value;
	[SerializeField]private TextMesh timeText;
	[SerializeField]private int potionType;
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
			if (!SinglePlayer_GameManager.Instance.IsGameOver) {
				
				myAnimator.SetTrigger ("PickUp");
				switch(potionType){
				case 0:
					SinglePlayer_GameManager.Instance.Player.GetComponent<SinglePlayer_PlayerStats> ().HealthPotion ();
					break;
				case 1:
					SinglePlayer_GameManager.Instance.Player.GetComponent<SinglePlayer_PlayerStats> ().DamagePotion ();
					break;
				case 2:
					SinglePlayer_GameManager.Instance.Player.GetComponent<SinglePlayer_PlayerStats> ().ArmorPotion ();
					break;
				}
				isUse = true;
			}
		}

	}
	public void PickUp(){
		Destroy (this.gameObject);
	}
}
