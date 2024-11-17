using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : MonoBehaviour {
	public ItemEquipment MyChestItem;
	private bool isUse;
	void OnMouseDown () {
		if (!isUse) {

			Inventory.Instance.Add (MyChestItem);
			Inventory.Instance.MyInventoryUi.UpdateInventoryUI ();
			EquipmentManager.Instance.SaveInventoryItem ();
			this.gameObject.GetComponent<Animator>().SetTrigger ("PickUp");
			StartCoroutine(PickUp ());
			isUse = true;
			Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("You Got :" + MyChestItem.name + "!");
		}
	}
	IEnumerator PickUp(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
	public void Convert(){

		GetComponent<MeshFilter> ().sharedMesh = MyChestItem.mesh.sharedMesh;
		GetComponent<MeshRenderer> ().sharedMaterials = MyChestItem.mesh.sharedMaterials;
		gameObject.AddComponent<MeshCollider> ();
	}
}
