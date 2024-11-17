using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfoPanel : MonoBehaviour {
	[SerializeField]
	private Text infoText,dmgText,strText,armorText,itemNameText,rarityText;
	[SerializeField]
	private Text cdmgText,cstrText,carmorText,crarityText;
	public void CloseInfoPanel(){
		infoText.text = "none";
		dmgText.text = "0";
		strText.text = "0";
		armorText.text = "0";
		this.gameObject.SetActive (false);
	}
	public void SetItemText(){
		cdmgText.gameObject.SetActive (true);
		cstrText.gameObject.SetActive (true);
		carmorText.gameObject.SetActive (true);
		crarityText.gameObject.SetActive (true);
	}
	public void SetChestText(){
		cdmgText.gameObject.SetActive (false);
		cstrText.gameObject.SetActive (false);
		carmorText.gameObject.SetActive (false);
		crarityText.gameObject.SetActive (false);
	}

	public void OpenInfoPanel(string info,string dmg,string str,string armor,string itemname,string rarity){
		this.gameObject.SetActive (true);
		infoText.text = (info == "") ? "none" : info;
		dmgText.text = dmg;
		strText.text = str;
		armorText.text = armor;
		itemNameText.text = itemname;
		rarityText.text = rarity;
	}
}
