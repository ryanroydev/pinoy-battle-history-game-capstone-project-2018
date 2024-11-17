using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreSlot : MonoBehaviour {



	[SerializeField]private bool isChests;
	[SerializeField]private TreasureChestScript Chests;


	[SerializeField]
	private ItemEquipment[] itemsStore;
	private ItemEquipment item;
	[SerializeField]
	private Button infoBtn;
	[SerializeField]
	private Text priceText, nameText;
	[SerializeField]
	private Image icon;

	void Start(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		bool isTagalog = languageIndex != 0;
		if (isChests) {
			if (Chests != null) {
				icon.sprite = Chests.Icon;
				nameText.text = !isTagalog ? Chests.ChestName : Chests.ChestTagalogName;
				priceText.text = Chests.Price.ToString ();
			}
				
		} else {
			item = itemsStore [Random.Range (0, itemsStore.Length)];
			if (item != null) {
				icon.sprite = item.icon;
				nameText.text = !isTagalog ? item.EnglishName : item.tagalogName;
				priceText.text = item.Price.ToString ();
			}
		}
	}
	private void OnEnable(){

	}
	public void BuyItem(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (isChests) {
			if (StatsManager.Instance.GetGold () >= Chests.Price) {
				if (ChestManager.Instance.IsInventoryNotFull()) {
					if (languageIndex == 0) {
						Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (Chests.ChestName + " has Purchased!!");
					} else {
						Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (Chests.ChestName + " ay Nabili!!");
					}
					StatsManager.Instance.addGold (-Chests.Price);
					switch (Chests.ChestRegisteredIndex) {
					case 0:
						ChestManager.Instance.CreateYamahitaTreasure ();
						break;
					case 1:
						ChestManager.Instance.CreateMagellanTreasure ();
						break;
					}

				} else {
					if (languageIndex == 0) {
						Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Chests Storage Full!!");
					} else {
						Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Puno ang Imabakan ng Kayamanan!!");
					}
				}
			} else {
				if (languageIndex == 0) {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("You have Insuficient gold to buy this item!!");
				} else {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Di sapat ang iyong ginto upang mabili ang kagamitang ito!");
				}
			}


		} else {
			if (StatsManager.Instance.GetGold () >= item.Price) {
				if (Inventory.Instance.Items.Count < 20) {
					if (languageIndex == 0) {
						Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (item.name + " has Purchased!!");
					} else {
						Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText (item.name + " ay nabili!!");
					}
					Inventory.Instance.Add (item);
					Inventory.Instance.MyInventoryUi.UpdateInventoryUI ();
					EquipmentManager.Instance.SaveInventoryItem ();
					StatsManager.Instance.addGold (-item.Price);

				} else {
					if (languageIndex == 0) {
						Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Inventory slot full!!");
					} else {
						Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Puno ang Imbentaryo!!");
					}
				}
			} else {
				if (languageIndex == 0) {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("You have Insuficient gold to buy this item!!");
				} else {
					Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Di sapat ang iyong ginto upang mabili ang kagamitang ito!");
				}
			}
		}

	}
	public void CloseInfoPanel(){
		StoreManager.Instance.ItemInfoPanelScript.CloseInfoPanel ();
	}
	public void OpenInfoPanel(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		bool isTagalog = languageIndex != 0;
		if (isChests) {
			if (Chests != null) {
				StoreManager.Instance.ItemInfoPanelScript.OpenInfoPanel (!isTagalog ? Chests.Description : Chests.TagalogDescription, "", "", "", !isTagalog ? Chests.ChestName : Chests.ChestTagalogName, "");
				StoreManager.Instance.ItemInfoPanelScript.SetChestText ();
					}
		} else {
			if (item != null) {
				StoreManager.Instance.ItemInfoPanelScript.SetItemText ();
				StoreManager.Instance.ItemInfoPanelScript.OpenInfoPanel (!isTagalog ? item.EqupInfo : item.TagalogEqupInfo, item.DamageModifier.ToString (), item.StrengthModifier.ToString (), item.ArmorModifier.ToString (), !isTagalog ? item.EnglishName : item.tagalogName, item.EquipRarity.ToString ());
			
			}
		}
	}

}
