using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour {

	public Sprite Icon;
	public int Price;
	public string Description;
	public string TagalogDescription;
	public string ChestName;
	public string ChestTagalogName;
	public int ChestRegisteredIndex;
	public int ChestIndex;
	[SerializeField]
	private ChestItem[] ChestItemPrefabs;

	[SerializeField]
	private ChestPisoCoin ChestCoin;

	[SerializeField]
	private Animator treasureAnimator;
	public Transform CameraPos;

	private string TreasureItemRange;
	public ChestType TreasureType;
	private ItemEquipment[] ChestItems;



	public GameObject GoldenLights;
	public void Selected(bool isSelect){
		GoldenLights.SetActive (isSelect);
	}
	public void DestroyTreasure(){

		Destroy (gameObject);
	

	}
	void OnMouseDown(){
		foreach (TreasureChestScript chest in ChestManager.Instance.Chests) {
			if (chest != this) {
				if (chest != null)
					chest.Selected (false);
			}

		}
		this.Selected (true);
		ChestManager.Instance.CurrentChestSelected = this;
	}
	public void OpenChest(){
		
		treasureAnimator.SetTrigger ("Open");
		ChestManager.Instance.ChestCamera.SetCameraNewTargetPos (CameraPos);
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.EmptyPanel);
		Convert ();
		GetComponent<BoxCollider> ().enabled = false;
		ChestManager.Instance.CurrentTreasureOpen = this;
		AddValueCoin ();

	}
	void AddValueCoin(){
		ChestCoin.gameObject.GetComponent<BoxCollider> ().enabled = true;
		switch ((int)TreasureType) {
		case 0:
			int ordinayValue = Random.Range (50, 100);
			ChestCoin.Value = ordinayValue;
			break;
		case 1:
			int extraordinayValue = Random.Range (100, 300);
			ChestCoin.Value = extraordinayValue;
			break;
		}
	}
	void OnEnable(){

		ChestItems = new ItemEquipment[4];
		List<ItemEquipment> commonItems = new List<ItemEquipment> ();
		List<ItemEquipment> uncommonItems= new List<ItemEquipment> ();
		List<ItemEquipment> rareItems= new List<ItemEquipment> ();
		List<ItemEquipment> veryrareItems= new List<ItemEquipment> ();
		List<ItemEquipment>epicItems= new List<ItemEquipment> ();
		List<ItemEquipment> legendaryItems= new List<ItemEquipment> ();
		foreach (ItemEquipment ie in ChestManager.Instance.RegisteredItems) {
			if (ie.VisibleToCharacter) {
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
		}
	


		int treasureItemLength = 0;
		int treasureItemLowest = 0;
		switch ((int)TreasureType) {
		case 0:
			treasureItemLowest = 1;
			treasureItemLength = 2;
			break;
		case 1:
			treasureItemLowest = 1;
			treasureItemLength = 3;
			break;
		case 2:
			treasureItemLowest = 2;
			treasureItemLength = 3;

			break;
		case 3:
			treasureItemLowest = 3;
			treasureItemLength = 4;
			break;

		}
		int RandomItemLenght = Random.Range (treasureItemLowest, treasureItemLength);
		for (int i = 0; i <= RandomItemLenght; i++) {
			switch ((int)TreasureType) {
			case 0:
				if (Random.Range (1, 100) >= 50) {
					ItemEquipment itemGet2 = commonItems [Random.Range (0, commonItems.Count)];
					ChestItems [i] = itemGet2;
				} else {
					ItemEquipment itemGet2 = uncommonItems [Random.Range (0, uncommonItems.Count)];
					ChestItems [i] = itemGet2;
				}
				break;
			case 1:

				if (Random.Range (1, 100) >= 50) {
					ItemEquipment itemGet2 = commonItems [Random.Range (0, commonItems.Count)];
					ChestItems [i] = itemGet2;
				} else if (Random.Range (1, 100) >= 50) {
					ItemEquipment itemGet2 = uncommonItems [Random.Range (0, uncommonItems.Count)];
					ChestItems [i] = itemGet2;
				} else {
					ItemEquipment itemGet2 = rareItems [Random.Range (0, rareItems.Count)];
					ChestItems [i] = itemGet2;
				}
				break;
			}
		}


	}

	void Convert(){
		if (ChestItems != null) {
			int counter = 0;
			foreach (ItemEquipment itemssss in ChestItems) {
				if (itemssss != null) {
					ChestItemPrefabs[counter].MyChestItem = itemssss;
					ChestItemPrefabs[counter].Convert ();
				}
				counter++;
			}
		}
		DisableMesh ();
	}
	void DisableMesh(){
		int counter = 0;
		foreach (ChestItem itemssss in ChestItemPrefabs) {
			if (itemssss != null) {
				if (itemssss.MyChestItem == null) {
					itemssss.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				}
			}
			counter++;
		}
	}
}
public enum ChestType{Ordinary,ExtraOrdinary,Golden,Diamond}
