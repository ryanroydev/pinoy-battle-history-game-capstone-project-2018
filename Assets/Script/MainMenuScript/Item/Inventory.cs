using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public InventoryUI MyInventoryUi;
	public ItemEquipment RewardItem{ get;private set;}
	public Transform ItemsParent;
	public Transform EquipmentParent; 
	public InventorySlot[] slots;
	public EquipmentItemSlot[] equipSlots;
	public ItemEquipment[] RegisteredInventoryItems;
	public ItemEquipment[] RewardItems;


	public delegate void OnItemChanged();
	public OnItemChanged onItemChangeCallBack;


	private int space = 20;
	private static Inventory instance;
	public static Inventory Instance{get{if (instance == null) {instance = GameObject.FindObjectOfType<Inventory> ();} return instance; }}
	public List<ItemEquipment> Items = new List<ItemEquipment> ();
	void OnEnable() {
		MyInventoryUi.StoreSlots ();
		slots = null;
		slots = ItemsParent.GetComponentsInChildren<InventorySlot> ();
		equipSlots = null;
		equipSlots = EquipmentParent.GetComponentsInChildren<EquipmentItemSlot> ();
	}
	public void  GetRewardItem(){
		List<ItemEquipment> commonItems = new List<ItemEquipment> ();
		List<ItemEquipment> uncommonItems= new List<ItemEquipment> ();
		List<ItemEquipment> rareItems= new List<ItemEquipment> ();
		List<ItemEquipment> veryrareItems= new List<ItemEquipment> ();
		List<ItemEquipment>epicItems= new List<ItemEquipment> ();
		List<ItemEquipment> legendaryItems= new List<ItemEquipment> ();
		foreach (ItemEquipment ie in RewardItems) {

			switch ((int)ie.EquipRarity) {
			case 0:
				commonItems.Add (ie);
				break;

			case 1:
				uncommonItems.Add (ie);
				break;
			case 2:
				rareItems.Add (ie);
				break;
			case 3:
				veryrareItems.Add (ie);
				break;
			case 4:
				epicItems.Add (ie);
				break;
			case 5:
				legendaryItems.Add (ie);
				break;
			}
		
		}
		int raritychances = Random.Range (0, 100);
		ItemEquipment rewardItem = null;

		if (raritychances <= 50) {//50%
			rewardItem = commonItems [Random.Range (0, commonItems.Count)];
		} else if (raritychances > 50 && raritychances <= 80) {//30%
			rewardItem = uncommonItems [Random.Range (0, uncommonItems.Count)];
		} else if (raritychances > 80 && raritychances <= 90) {//10%
			rewardItem = rareItems [Random.Range (0, rareItems.Count)];
		} else if (raritychances > 90 && raritychances <= 96) {//6%
			rewardItem = veryrareItems [Random.Range (0, veryrareItems.Count)];
		} else if (raritychances > 96 && raritychances <= 99) {//3%
			rewardItem = epicItems [Random.Range (0, epicItems.Count)];
		} else {//1%
			rewardItem = legendaryItems [Random.Range (0, legendaryItems.Count)];
		}
		Add (rewardItem);
	
	   RewardItem = rewardItem;
		MyInventoryUi.UpdateInventoryUI ();
		EquipmentManager.Instance.SaveInventoryItem ();

	}
	public  void LoadInventoryItems(){
		string items = PlayerPrefs.GetString ("InventoryItems", "");
		if (items != "") {
			string[] data = items.Split ('%');
			for (int i = 0; i < data.Length; i++) {
				if (data [i] != "") {
					
					Add (RegisteredInventoryItems [int.Parse (data [i])]);
				}
			}


		}
	}
	public  void LoadEquipmentItems(){
		string items = PlayerPrefs.GetString ("EquipmentItems", "");
		if (items != "") {
			string[] data = items.Split ('%');
			for (int i = 0; i < data.Length; i++) {
				if (data [i] != "") {
					ItemEquipment equipmentitem = RegisteredInventoryItems [int.Parse (data [i])];
					int equipSlotIndex = (int)equipmentitem.EquipSlot;

					EquipmentManager.Instance.currentEquipment [equipSlotIndex] = equipmentitem;
				}
			}


		}
	}
	public void Add(ItemEquipment item){
		if (!item.isDefaultItem) {
			if (Items.Count < 20 && item != null) {
				Items.Add (item);
				if (onItemChangeCallBack != null) {
					onItemChangeCallBack.Invoke ();
				}
			}
		}

	}

	public void Remove(ItemEquipment item){
		Items.Remove (item);
		if (onItemChangeCallBack != null) {
			onItemChangeCallBack.Invoke ();
		}

	}
}
