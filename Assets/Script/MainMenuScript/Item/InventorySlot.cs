using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour {
	public Image icon;
	public Button RemoveBtn;
	public ItemEquipment item;
	public Button EquipBtn;
	public Button InfoBtn;
	public bool isSelect = false;
	public void SelectSlot(){
		if (item != null) {
			isSelect = !isSelect;
			RemoveBtn.gameObject.SetActive (isSelect);
			InfoBtn.gameObject.SetActive (isSelect);
			EquipBtn.gameObject.SetActive (isSelect);

		}
		Inventory myInventory = Inventory.Instance;
		for (int i = 0; i < myInventory.slots.Length; i++) {
			if (myInventory.slots [i] != this) {
				myInventory.slots [i].UnSelectSlot ();
			}
		}
		for (int i = 0; i < myInventory.equipSlots.Length; i++) {
			if (myInventory.equipSlots [i] != this) {
				myInventory.equipSlots [i].UnSelectSlot ();
			}
		}
	}

	public void CloseInfoPanel(){
		EquipmentManager.Instance.ItemInfoPanelScript.CloseInfoPanel ();
	}
	public void OpenInfoPanel(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (item != null) {
			bool isTagalog = languageIndex != 0;
			EquipmentManager.Instance.ItemInfoPanelScript.OpenInfoPanel (!isTagalog ?item.EqupInfo:item.TagalogEqupInfo, item.DamageModifier.ToString (), item.StrengthModifier.ToString (), item.ArmorModifier.ToString (), !isTagalog ? item.EnglishName : item.tagalogName, item.EquipRarity.ToString ());
		}
	}
	public void UnSelectSlot(){
		isSelect = false;
		RemoveBtn.gameObject.SetActive (false);
		InfoBtn.gameObject.SetActive (false);
		EquipBtn.gameObject.SetActive (false);
	}
	public void AddItem(ItemEquipment newItem){
		item = newItem;
		icon.gameObject.SetActive (true);
		icon.sprite = item.icon;
		RemoveBtn.onClick.RemoveAllListeners ();
		RemoveBtn.onClick.AddListener (RemoveItem);
	}
	
	public void ClearSlot(){
		item = null;
		icon.sprite = null;
		icon.gameObject.SetActive (false);
		RemoveBtn.gameObject.SetActive (false);
	}
	public void RemoveItem(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (item.name + " has sold!!");
		} else {
			Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (item.name + " ay nabenta!!");
		}
		StatsManager.Instance.addGold (item.Price / 2);
	
		SelectSlot ();
		Inventory.Instance.Remove (item);


	}
	public void UseItem(){
		
		if (item != null) {

			if (PlayerPrefs.GetInt ("PlayerLevel", 1) < item.equipLevel) {
				int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
				if (languageIndex == 0) {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText (item.name + " equippable only at level " + item.equipLevel + " and up!!");
				} else {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText (item.name + " ay nasusuot lamang kapag ikaw ay nasa Antas " + item.equipLevel + " pataas!!");
				}
				return;
			}

		//	Debug.Log (item.name);
			SelectSlot ();
		     item.Use ();

		}
	}
}
