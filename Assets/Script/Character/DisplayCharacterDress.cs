using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacterDress : MonoBehaviour {
	bool StoreCurrentMeshes = true;
	[SerializeField]private SkinnedMeshRenderer DefaultHead,DefaultBody,DefaultArms,Defaultfeet,DefaultWeapon,DefaultShield;
	[SerializeField]private SkinnedMeshRenderer TargetMesh;
	[SerializeField]private Transform TargetMeshParent;
	[SerializeField]private SkinnedMeshRenderer[] CurrentMeshes;
	[SerializeField]private int CharacterIndexId;
	// Use this for initialization
	void Start () {
		
		StartCoroutine (CheckItemDress ());
		ClearAllMeshes ();

	}
	void OnEnable() {
		//Inventory.Instance.onItemChangeCallBack += UpdateInventoryUI;


		EquipmentManager.Instance.onEquipmentChanged += DelegateDelayDisplayItem;
		if (StoreCurrentMeshes) {
			int itemSlot = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
			CurrentMeshes = new SkinnedMeshRenderer[itemSlot];
			StoreCurrentMeshes = false;
		}
		DisplayDefaultItems (null,null);

	}
	void OnDestroy(){
		EquipmentManager.Instance.onEquipmentChanged -= DelegateDelayDisplayItem;
	}
	void OnDisable(){
		EquipmentManager.Instance.onEquipmentChanged -= DelegateDelayDisplayItem;
	}
	void ClearAllMeshes(){
		int counter = 0;
		foreach (SkinnedMeshRenderer mesh in CurrentMeshes) {
			if (mesh != null) {
				Destroy (mesh.gameObject);
				CurrentMeshes [counter] = null;
				counter++;
			
			}
		
		}
	}
	IEnumerator CheckItemDress(){//CheckAndUpdate Item Dress
		for (;;) {
			float delayTimer = 0.2f;
			yield return new WaitForSeconds (delayTimer);
			if (EquipmentManager.Instance.currentEquipment != null) {
				DisplayDefaultItems (null, null);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
	void DelegateDelayDisplayItem(ItemEquipment newItem, ItemEquipment oldItem){
		if (this.gameObject.activeSelf)
			StartCoroutine (DelayDisplayItem (newItem, oldItem));
	}
	IEnumerator DelayDisplayItem(ItemEquipment newItem, ItemEquipment oldItem){
		yield return new WaitForSeconds (0.1f);
		DisplayDefaultItems(newItem, oldItem);
	}
	void EnableDefaultItem(SkinnedMeshRenderer defaultRenderer,bool isEnable){
		if (defaultRenderer != null)
			defaultRenderer.gameObject.SetActive (isEnable);
	}
	public void DisplayDefaultItems(ItemEquipment newItem, ItemEquipment oldItem){
		int counter = 0;

		foreach (ItemEquipment myequip in EquipmentManager.Instance.currentEquipment) {
			
			if (myequip != null) {
				int equipSlotIndex = (int)myequip.EquipSlot;

				if (myequip.VisibleToCharacter==true && myequip.CharacterIndex == CharacterIndexId) {
					
					if (CurrentMeshes [equipSlotIndex] == null && CurrentMeshes [equipSlotIndex] != myequip.mesh) {

						SetMeshDress (myequip.mesh, equipSlotIndex);

						switch (counter) {
						case 0:
							//set head
							EnableDefaultItem (DefaultHead, false);

							break;
						case 1:
							//setbody
							EnableDefaultItem (DefaultBody, false);
							break;
						case 2:
							//setarms
							EnableDefaultItem (DefaultArms, false);
							break;
						case 3:
							//setfeet
							EnableDefaultItem (Defaultfeet, false);
							break;
						case 4:
							//setweapon
							EnableDefaultItem (DefaultWeapon, false);
							break;
						case 5:
							//setshield
							EnableDefaultItem (DefaultShield, false);
							break;
						}
					} else if (CurrentMeshes [equipSlotIndex] != myequip.mesh) {
						//if (CurrentMeshes [equipSlotIndex] != null) {
							//Destroy (CurrentMeshes [counter].gameObject);
						//}
						SetMeshDress (myequip.mesh, equipSlotIndex);
					}
					//add  display equipment to character dress
				} else {
					//display normal Dress
					//Debug.Log (equipSlotIndex);
					if (CurrentMeshes [equipSlotIndex] != null) {
						Destroy (CurrentMeshes [equipSlotIndex].gameObject);
					}
					switch (equipSlotIndex) {
					case 0:
						//set head
						EnableDefaultItem (DefaultHead,true);
						break;
					case 1:
						//setbody
						EnableDefaultItem (DefaultBody,true);
						break;
					case 2:
						//setarms
						EnableDefaultItem (DefaultArms,true);
						break;
					case 3:
						//setfeet
						EnableDefaultItem (Defaultfeet,true);
						break;
					case 4:
						//setweapon
						EnableDefaultItem (DefaultWeapon,true);
						break;
					case 5:
						//setshield
						EnableDefaultItem (DefaultShield,true);
						break;
					}

				}
			} else {
				switch (counter) {
				case 0:
					//set head
					EnableDefaultItem (DefaultHead,true);

					break;
				case 1:
					//setbody
					EnableDefaultItem (DefaultBody,true);
					break;
				case 2:
					//setarms
					EnableDefaultItem (DefaultArms,true);
					break;
				case 3:
					//setfeet
					EnableDefaultItem (Defaultfeet,true);
					break;
				case 4:
					//setweapon
					EnableDefaultItem (DefaultWeapon,true);
					break;
				case 5:
					//setshield
					EnableDefaultItem (DefaultShield,true);
					break;
				}
				if (CurrentMeshes [counter] != null) {
					Destroy (CurrentMeshes [counter].gameObject);
				}
			}
			counter++;
		}
	}
	void SetMeshDress(SkinnedMeshRenderer newItemMesh,int SlotIndex){
		if (CurrentMeshes [SlotIndex] != null) {
			Destroy (CurrentMeshes [SlotIndex].gameObject);
		}
		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer> (newItemMesh);
	
		newMesh.transform.SetParent (TargetMesh.transform, false);
		newMesh.bones = TargetMesh.bones;
		newMesh.rootBone = TargetMesh.rootBone;
		CurrentMeshes [SlotIndex] = newMesh;
	}

}
