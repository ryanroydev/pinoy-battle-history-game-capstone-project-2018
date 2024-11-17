using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class OnlinePotionScript : NetworkBehaviour {

	[SerializeField] private float spinSpeed;
	[SerializeField]private Animator myAnimator;
	[SerializeField]private int value;
	[SerializeField]private TextMesh timeText;
	[SerializeField]private int potionType;
	float timeDur = 0;
	float currenttime = 20;

	public bool IsUse;
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
		if (!IsUse) {
			

			myAnimator.SetTrigger ("PickUp");
			switch (potionType) {
			case 0:
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().CmdTellServerPotionPicked (this.gameObject);
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().HealthPotion ();
				break;
			case 1:
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().CmdTellServerPotionPicked (this.gameObject);
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().DamagePotion();
				break;
			case 2:
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().CmdTellServerPotionPicked (this.gameObject);
				Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ().ArmorPotion ();
				break;
			}
			IsUse = true;

		}

	}
	public void PickUp(){
		Destroy (this.gameObject);
	}
}
