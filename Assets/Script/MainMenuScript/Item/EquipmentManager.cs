using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
	public ItemInfoPanel ItemInfoPanelScript;


	public Transform ItemsParent;

	InventorySlot[] slots;


	private static EquipmentManager instance;
	public static EquipmentManager Instance{get{if (instance == null) {instance = GameObject.FindObjectOfType<EquipmentManager> ();} return instance;}}

	public ItemEquipment[] currentEquipment;

	public delegate void OnEquipmentChanged (ItemEquipment newItem, ItemEquipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;

	void OnEnable () {
		
		int itemSlot = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment = new ItemEquipment[itemSlot];

	
		slots = null;
		slots = ItemsParent.GetComponentsInChildren<InventorySlot> ();
		Inventory.Instance.LoadEquipmentItems ();
		Inventory.Instance.LoadInventoryItems ();
		EquipmentManager.Instance.SaveInventoryItem ();
		EquipmentManager.Instance.SaveEquipmentItem ();
		EquipmentManager.Instance.GetSaveEquipStats ();
	
		//Inventory.Instance.MyInventoryUi.UpdateInventoryUI ();
		//Inventory.Instance.MyInventoryUi.UpdateAllEquipUI ();
		//StatsManager.Instance.addGold (100000);
	}

	void OnDisable(){
		//EquipmentManager.Instance.SaveInventoryItem ();
		//EquipmentManager.Instance.SaveEquipmentItem ();
		//EquipmentManager.Instance.GetSaveEquipStats ();
	}
	public void Equip(ItemEquipment newEquipment){




		int equipSlotIndex = (int)newEquipment.EquipSlot;
		ItemEquipment oldEquipment = null;
		if (currentEquipment [equipSlotIndex] != null) {
			oldEquipment = currentEquipment [equipSlotIndex];
			Inventory.Instance.Add (oldEquipment);
		
		}
		if (onEquipmentChanged != null) {
			onEquipmentChanged.Invoke (newEquipment, oldEquipment);
		}
		currentEquipment [equipSlotIndex] = newEquipment;
		Inventory.Instance.MyInventoryUi.PlayInventorySound ();
	}

	public void UneuEquipItem(int slotIndex){
		if (Inventory.Instance.Items.Count >= 20) {
			Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText("Inventory Full, Unable to Unequip item!!");
			return;
		}
		if (currentEquipment [slotIndex] != null) {
			ItemEquipment oldEquipment = currentEquipment [slotIndex];
			Inventory.Instance.Add (oldEquipment);
			currentEquipment [slotIndex] = null;
			if (onEquipmentChanged != null) {
				onEquipmentChanged.Invoke (null, oldEquipment);
			}
		}
		Inventory.Instance.MyInventoryUi.PlayInventorySound ();
	}
	public void UnEquipAllItem(){
		for (int i = 0; i < currentEquipment.Length; i++) {
			if (Inventory.Instance.Items.Count >= 20) {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("Inventory Full, Unable to Unequip item!!");
				break;
			} else {
				UneuEquipItem (i);

			}
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
		Inventory.Instance.MyInventoryUi.PlayInventorySound ();
	}
	void Update(){


	


	}
	public void SaveInventoryItem(){
		string data = "";
		Inventory myInventory = Inventory.Instance;
		for (int i = 0; i < slots.Length; i++) {
			if (slots [i].item != null) {
				for (int n = 0; n < myInventory.RegisteredInventoryItems.Length; n++) {
					
						if (myInventory.RegisteredInventoryItems [n].name == slots [i].item.name) {
							data += n.ToString () + "%";

						}

				}
			}
		}
		if (data != "") {
			PlayerPrefs.SetString ("InventoryItems", data);

		} else {
			PlayerPrefs.SetString ("InventoryItems", "");

		}
	}





	public void SaveEquipmentItem(){
		string data = "";
		Inventory myInventory = Inventory.Instance;
		for (int i = 0; i < currentEquipment.Length; i++) {
			if (currentEquipment [i] != null) {
				for (int n = 0; n < myInventory.RegisteredInventoryItems.Length; n++) {
		
					if (myInventory.RegisteredInventoryItems [n].name == currentEquipment[i].name) {
						data += n.ToString() + "%";

					}
			
				}
			}
		}
		if (data != "") {
			PlayerPrefs.SetString ("EquipmentItems", data);
		} else {
			PlayerPrefs.SetString ("EquipmentItems", "");
		}
	}
	public void GetSaveEquipStats(){
		int addedDmg = 0;
		int addedStr = 0;
		int addedDps = 0;
		foreach (ItemEquipment myequip in currentEquipment) {
			if (myequip != null) {
				addedDmg += myequip.DamageModifier;
				addedDps += myequip.ArmorModifier;
				addedStr += myequip.StrengthModifier;
				if (myequip.VisibleToCharacter) {
				//add  display equipment to character dress
				}else{
				//display normal Dress
				}
			}
		}
		//Debug.Log ("dmg : " + addedDmg + " armor : " + addedDps + " str : " + addedStr);
		PlayerPrefs.SetInt ("AddedDamage", addedDmg);
		PlayerPrefs.SetInt ("AddedArmor", addedDps);
		PlayerPrefs.SetInt ("AddedStregth", addedStr);
	}
}
