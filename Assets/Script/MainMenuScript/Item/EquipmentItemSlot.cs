using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentItemSlot : MonoBehaviour {
	public Image icon;
	public ItemEquipment item;
	public Button UnEquipBtn;
	public Button InfoBtn;
	public bool isSelect = false;
	public void AddItem(ItemEquipment newItem){
		item = newItem;
		icon.gameObject.SetActive (true);
		icon.sprite = item.icon;

	}
	public void SelectSlot(){
		if (item != null) {
			isSelect = !isSelect;
			InfoBtn.gameObject.SetActive (isSelect);
			UnEquipBtn.gameObject.SetActive (isSelect);

		}
		Inventory myInventory = Inventory.Instance;
		for (int i = 0; i < myInventory.equipSlots.Length; i++) {
			if (myInventory.equipSlots [i] != this) {
				myInventory.equipSlots [i].UnSelectSlot ();
			}
		}  
		for (int i = 0; i < myInventory.slots.Length; i++) {
			if (myInventory.slots [i] != this) {
				myInventory.slots [i].UnSelectSlot ();
			}
		}
	}

	public void UnSelectSlot(){
		isSelect = false;
		InfoBtn.gameObject.SetActive (false);
		UnEquipBtn.gameObject.SetActive (false);
	}
	public void ClearSlot(){
		item = null;
		icon.sprite = null;
		icon.gameObject.SetActive (false);

	}
	public void OpenInfoPanel(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (item != null) {
			bool isTagalog = languageIndex != 0;
			EquipmentManager.Instance.ItemInfoPanelScript.OpenInfoPanel (!isTagalog ? item.EqupInfo : item.TagalogEqupInfo, item.DamageModifier.ToString (), item.StrengthModifier.ToString (), item.ArmorModifier.ToString (), !isTagalog ? item.EnglishName : item.tagalogName, item.EquipRarity.ToString ());
		}
	}
	public void RemoveItem(){
		Inventory.Instance.Remove (item);
	}

	public void UnEquipItem(){
		if (item != null) {
			EquipmentManager.Instance.UneuEquipItem ((int)item.EquipSlot);
			UnSelectSlot ();
		}
	
	}
}
