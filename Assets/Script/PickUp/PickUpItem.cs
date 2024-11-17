using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickUpItem : MonoBehaviour {
	bool isUse;
	[SerializeField]
	private Animator myAnimator;
	[SerializeField]
	private ItemEquipment pickUpItem;
	[SerializeField]
	private ItemEquipment[] randomItems;
	[SerializeField]
	private TextMesh timeText;

	float timeDur = 0;
	float currenttime = 20;
	// Use this for initialization
	void OnEnable(){
		GetItemEquipment ();

	}
	void Update(){
		//this.transform.Rotate (0, 20 * Time.deltaTime, 0, Space.World);
		//timeText.transform.Rotate (0, -20 * Time.deltaTime, 0, Space.World);



		if (currenttime <= 4) {
			timeText.color = Color.red;
			timeText.text = Mathf.FloorToInt (currenttime).ToString ();
		} else if (currenttime <= 6) {
			
			timeText.text = Mathf.FloorToInt (currenttime).ToString ();
		} else {
			timeText.text = "";
		}


		currenttime -= Time.deltaTime;



		if (currenttime <= timeDur) {
			Destroy (this.gameObject);
		}
	}
	void Convert(){
		
		GetComponent<MeshFilter> ().sharedMesh = pickUpItem.mesh.sharedMesh;
		GetComponent<MeshRenderer> ().sharedMaterials = pickUpItem.mesh.sharedMaterials;
		gameObject.AddComponent<MeshCollider> ();
	}
	void GetItemEquipment(){

		List<ItemEquipment> commonItems = new List<ItemEquipment> ();
		List<ItemEquipment> uncommonItems= new List<ItemEquipment> ();
		List<ItemEquipment> rareItems= new List<ItemEquipment> ();
		List<ItemEquipment> veryrareItems= new List<ItemEquipment> ();
		List<ItemEquipment>epicItems= new List<ItemEquipment> ();
		List<ItemEquipment> legendaryItems= new List<ItemEquipment> ();
		foreach (ItemEquipment ie in randomItems) {

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

		if (raritychances <= 50) {//50%
			pickUpItem = commonItems [Random.Range (0, commonItems.Count)];
		} else if (raritychances > 50 && raritychances <= 80) {//30%
			pickUpItem = uncommonItems [Random.Range (0, uncommonItems.Count)];
		} else if (raritychances > 80 && raritychances <= 90) {//10%
			pickUpItem = rareItems [Random.Range (0, rareItems.Count)];
		} else if (raritychances > 90 && raritychances <= 96) {//6%
			pickUpItem = veryrareItems [Random.Range (0, veryrareItems.Count)];
		} else if (raritychances > 96 && raritychances <= 99) {//3%
			pickUpItem = epicItems [Random.Range (0, epicItems.Count)];
		} else {//1%
			pickUpItem = legendaryItems [Random.Range (0, legendaryItems.Count)];
		}



		this.gameObject.name = pickUpItem.name;
		Convert ();
	}
	// Update is called once per frame
	
	void OnMouseDown () {
		if (!isUse) {
			
			Inventory.Instance.Add (pickUpItem);
			Inventory.Instance.MyInventoryUi.UpdateInventoryUI ();
			EquipmentManager.Instance.SaveInventoryItem ();
			myAnimator.SetTrigger ("PickUp");
			StartCoroutine(PickUp ());
			isUse = true;
		}
	}
	IEnumerator PickUp(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
