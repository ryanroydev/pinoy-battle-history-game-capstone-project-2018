using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestManager : MonoBehaviour {
	int currentSpawnNum;
	public TreasureChestScript[] ChestPrefabs;

	[SerializeField]
	private Transform[] ChestSpawnPos;
	[SerializeField]
	private Transform[] ChestCameraPos;


	[SerializeField]private Button OpenChestBtn;
	public List<TreasureChestScript> Chests = new List<TreasureChestScript> ();

	public TreasureChestScript CurrentChestSelected;
	public ChestPanelScript ChestPanelScript;
	public ChestViewerCamera ChestCamera;
	private static ChestManager instance;
	public TreasureChestScript CurrentTreasureOpen;

	public ItemEquipment[] RegisteredItems;
	void OnEnable(){
		LoadChests ();
	}
	public static ChestManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<ChestManager> ();
			}
			return instance;
		}
	}


	public void BackToChestPanel(){
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.MainMenuChestPanel);
		ChestCamera.SetRealCameraPos ();
		if (CurrentTreasureOpen != null) {
			Destroy (CurrentTreasureOpen.gameObject);
		}
	}
	public bool IsInventoryEmpty(){
		bool isChestInevetoryEmpty = true;
		for (int i = 0; i < ChestSpawnPos.Length; i++) {

			if (Chests [i] != null) {

				isChestInevetoryEmpty &= false;
			}
		}
		return isChestInevetoryEmpty;
	}
	public bool IsInventoryNotFull(){
		bool isChestInevetoryFull = true;
		for (int i = 0; i < ChestSpawnPos.Length; i++) {
	
			if (Chests [i] == null) {

				isChestInevetoryFull &= false;
			}
		}
		return !isChestInevetoryFull;
	}
	public void CreateChest(GameObject ChestPrefab){


		if (IsInventoryNotFull()) {

			bool isCreated = false;
	
			for (int i = 0; i < ChestSpawnPos.Length; i++) {
				//	if (Chests [i] != null && !isCreated) {
				if (!isCreated && Chests [i] == null) {
				
					GameObject go = Instantiate (ChestPrefab, ChestSpawnPos [i].position, ChestSpawnPos [i].rotation);
					go.GetComponent<TreasureChestScript> ().CameraPos = ChestCameraPos [i];
					go.GetComponent<TreasureChestScript> ().ChestIndex = i;
					currentSpawnNum++;
					go.GetComponent<OfflineSpawnUniqueId> ().ObjectID = "Chest" + currentSpawnNum;
					AddChest (go.GetComponent<TreasureChestScript> ());
					isCreated = true;
				}
				//}
			}

		} else {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			if (languageIndex == 0) {
				Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Chest Storage Full!!");
			} else {
				Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Puno ang Imabakan ng Kayamanan!!");
			}
		}
	}
	public void SaveChests(){
		string data = "";

		for (int i = 0; i < Chests.Count; i++) {
			if (Chests [i] != null) {
				data += Chests [i].ChestRegisteredIndex.ToString () + "%";

	
			}
		}
		if (data != "") {
			PlayerPrefs.SetString ("Chests", data);
		} else {
			PlayerPrefs.SetString ("Chests", "");
		}

	}
	public void LoadChests(){
		string chestsdata = PlayerPrefs.GetString ("Chests", "");
		if (chestsdata != "") {
			string[] data = chestsdata.Split ('%');
			for (int i = 0; i < data.Length; i++) {
				if (data [i] != "") {
					CreateChest (ChestPrefabs [int.Parse (data [i])].gameObject);
			
				}
			}


		}
	}
	public void AddChest(TreasureChestScript chestAdded){
		if (chestAdded != null) {
			Chests [chestAdded.ChestIndex] = chestAdded;
		}
	}
	public void CreateMagellanTreasure(){
		CreateChest (ChestPrefabs[1].gameObject);
		SaveChests ();
	}
	public void CreateYamahitaTreasure(){
		CreateChest (ChestPrefabs[0].gameObject);
		SaveChests ();
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			CreateYamahitaTreasure ();

		}

		if (CurrentChestSelected != null && Custom_NetworkLobbyManager._LMSingleton.OpenChestBtn.interactable != true) {
			Custom_NetworkLobbyManager._LMSingleton.OpenChestBtn.interactable = true;

		} else if (CurrentChestSelected == null && Custom_NetworkLobbyManager._LMSingleton.OpenChestBtn.interactable != false) {
			Custom_NetworkLobbyManager._LMSingleton.OpenChestBtn.interactable = false;

		}
	}
	public void OpenCurrentChest(){
		if (CurrentChestSelected != null) {
			CurrentChestSelected.OpenChest ();
			for (int i = 0; i < Chests.Count; i++) {
				if (Chests [i] != null && CurrentChestSelected != null) {
					if (CurrentChestSelected.name == Chests [i].name) {
						Chests [i] = null;
					}
				}
			}
			CurrentChestSelected = null;
			SaveChests ();

		}
	}
	
}
