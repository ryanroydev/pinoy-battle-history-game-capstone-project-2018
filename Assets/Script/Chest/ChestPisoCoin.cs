using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPisoCoin : MonoBehaviour {
	[SerializeField]
	private Animator myAnimator;
	private bool isUse;
	public int Value;
	void OnMouseDown(){
		if (!isUse) {
			myAnimator.SetTrigger ("PickUp");
			StatsManager.Instance.addGold (Value);
			isUse = true;
			Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("You Got :" + Value + "Gold Piso Coin!");
		}

	}
	public void PickUp(){
		Destroy (this.gameObject);
	}
}
