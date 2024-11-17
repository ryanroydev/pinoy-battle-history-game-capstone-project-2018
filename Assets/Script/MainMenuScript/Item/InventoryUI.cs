using UnityEngine;

public class InventoryUI : MonoBehaviour {
	public Transform ItemsParent;
	public Transform EquipmentParent; 
	InventorySlot[] slots;
	EquipmentItemSlot[] equipSlots;
	public AudioSource inventorySound;
	void OnEnable() {
		
		Inventory.Instance.onItemChangeCallBack += UpdateInventoryUI;

		EquipmentManager.Instance.onEquipmentChanged += UpdateeEquipUI;
	

	}
	public void PlayInventorySound(){
		inventorySound.Play ();
	}
	public void StoreSlots(){
		if (slots == null) {
			slots = ItemsParent.GetComponentsInChildren<InventorySlot> ();
		}
		if (equipSlots == null) {
			equipSlots = EquipmentParent.GetComponentsInChildren<EquipmentItemSlot> ();
		}
	}
	void OnDisable(){
		Inventory.Instance.onItemChangeCallBack -= UpdateInventoryUI;

		EquipmentManager.Instance.onEquipmentChanged -= UpdateeEquipUI;
		EquipmentManager.Instance.SaveInventoryItem ();
		EquipmentManager.Instance.SaveEquipmentItem ();
		EquipmentManager.Instance.GetSaveEquipStats ();
	}

	private void Start(){
		//Inventory.Instance.LoadInventoryItems ();
		//Inventory.Instance.LoadEquipmentItems ();
		//EquipmentManager.Instance.SaveInventoryItem ();
		//EquipmentManager.Instance.SaveEquipmentItem ();
		//EquipmentManager.Instance.GetSaveEquipStats ();
		UpdateAllEquipUI ();
		UpdateInventoryUI ();
	}


	public void UpdateInventoryUI(){

		for (int i = 0; i < slots.Length; i++) {
			slots [i].UnSelectSlot ();
			if (i < Inventory.Instance.Items.Count) {
				if (Inventory.Instance.Items [i] != null) {
					slots [i].AddItem (Inventory.Instance.Items [i]);
				}
			} else {
				slots [i].ClearSlot ();
			}
		
		}
	
	}
	void UpdateeEquipUI(ItemEquipment newItem, ItemEquipment oldItem){
		if (newItem != null) {
			equipSlots [(int)newItem.EquipSlot].AddItem (newItem);
		} else {
			equipSlots [(int)oldItem.EquipSlot].ClearSlot ();
		}

	}
	public void UpdateAllEquipUI(){
		
			
			for (int i = 0; i < equipSlots.Length; i++) {
				//Debug.Log (equipSlots [i]);
				if (i < EquipmentManager.Instance.currentEquipment.Length) {

					ItemEquipment equip = EquipmentManager.Instance.currentEquipment [i];

					if (equip != null) {
						//	Debug.Log ((int)equip.EquipSlot+"heyeyyeyey");
						equipSlots [(int)equip.EquipSlot].AddItem (equip);

					}
				} else {
					ItemEquipment equip = EquipmentManager.Instance.currentEquipment [i];
					equipSlots [(int)equip.EquipSlot].ClearSlot ();
				}
			}

	}
}
