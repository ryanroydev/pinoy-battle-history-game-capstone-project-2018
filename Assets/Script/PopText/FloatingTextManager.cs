using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour {
	public bool isOnline;
	private static FloatingTextManager instance;
	public static FloatingTextManager Instance{get{ if (instance == null) {instance = GameObject.FindObjectOfType<FloatingTextManager> ();}return instance;}}
	[SerializeField]
	private FloatingText floatingText;
	[SerializeField]
	private FloatingText healthfloatingText,dmgfloatingText,armorfloatingText;
	public void CreatePoppUpText(string dmg,Transform location){
		FloatingText ft = Instantiate (floatingText);
		Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
		ft.transform.SetParent (this.transform, false);
		ft.transform.position = screenPos;
		ft.SetText (dmg);
	}
	public void CreatePoppUpHealthPotionText(string dmg,Transform location){
		if (isOnline) {

			FloatingText ft = Instantiate (healthfloatingText);
			Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
			ft.transform.SetParent (this.transform, false);
			ft.transform.position = screenPos;
			ft.SetText (dmg);

		} else {
			if (SinglePlayer_GameManager.Instance.GameStart) {
				FloatingText ft = Instantiate (healthfloatingText);
				Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
				ft.transform.SetParent (this.transform, false);
				ft.transform.position = screenPos;
				ft.SetText (dmg);
			}
		}
	}
	public void CreatePoppUpArmorPotionText(string dmg,Transform location){
		if (isOnline) {
		
			FloatingText ft = Instantiate (armorfloatingText);
			Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
			ft.transform.SetParent (this.transform, false);
			ft.transform.position = screenPos;
			ft.SetText (dmg);
		
		} else {
			if (SinglePlayer_GameManager.Instance.GameStart) {
				FloatingText ft = Instantiate (armorfloatingText);
				Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
				ft.transform.SetParent (this.transform, false);
				ft.transform.position = screenPos;
				ft.SetText (dmg);
			}
		}
	}
	public void CreatePoppUpDamagePotionText(string dmg,Transform location){
		if (isOnline) {

			FloatingText ft = Instantiate (dmgfloatingText);
			Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
			ft.transform.SetParent (this.transform, false);
			ft.transform.position = screenPos;
			ft.SetText (dmg);

		} else {
			if (SinglePlayer_GameManager.Instance.GameStart) {
				FloatingText ft = Instantiate (dmgfloatingText);
				Vector2 screenPos = Camera.main.WorldToScreenPoint (location.position);
				ft.transform.SetParent (this.transform, false);
				ft.transform.position = screenPos;
				ft.SetText (dmg);
			}
		}
	
	}
}
