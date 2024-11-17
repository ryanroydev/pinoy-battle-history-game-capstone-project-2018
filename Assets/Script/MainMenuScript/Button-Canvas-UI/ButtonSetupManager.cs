using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSetupManager : MonoBehaviour {
	[SerializeField]
	private AudioSource buttonSound;
	[SerializeField]
	private Vector3[] cameraTargetsPos;
	[SerializeField]
	private LoadSceneScript myLoadSceneScript;


	[SerializeField]
	private CharacterSelectionScript myCharacterSelectionScript;
	[SerializeField]
	private ChangeNameScript myChangeNameScript;
	[SerializeField]
	private GuidePanelScript myGuidePanelScript;
	[SerializeField]
	private LobbyMainMenu myLobbyMainMenuScript;

	[SerializeField]
	private RectTransform MainMenuTrophyPanel,MainMenuCrownPanel,MainMenuChestPanel,MainMenuBookPanel,MainMenuProfilePanel,MainMenuSettingPanel,MainMenuGameModePanel,MainMenuStartPanel,MainMenuOnlineModePanel,MainMenuOfflineModePanel,MainMenuProfileChangeNamePanel,MainMenuProfileChangeCharacterPanel,MainMenuCreditsPanel,MainMenuGuidePanel,MainMenuEquiItemsPanel,MainMenuStorePanel;
	[SerializeField]
	private RectTransform offlineLobbyPanel,offlineCreateJoinLobbyPanel,offlineCreateLobbyPanel;
	[SerializeField]
	private RectTransform onlineServerListPanel,onlineCreateLobbyPanel,onlineCreateJoinLobbyPanel;

	[SerializeField]
	private Button offlineJoinLobbyBtn,offlineCreateLobbyBtn,offlineCreateBtn,offlineCreateLobbyBackBtn,offlineCreateJoinlobbyBackBtn;
	[SerializeField]
	private Button onlineJoinLobbyBtn,onlineCreateLobbyBtn,onlineCreateBtn,onlineCreateLobbyBackBtn,onlineCreateJoinlobbyBackBtn,onlineJoinLobbyBackBtn;
	[SerializeField]
	private Text onlineLobbyNameInput,offlineLobbyNameInput;
	[SerializeField]
	private Dropdown onlineCategoryDropdown,offlineCategoryDropdown;
	[SerializeField]
	private string[] catergories;

	[SerializeField]
	private Button ChestBtn,BackChestBtn,OpenChestBtn,BackToChestPanelBtn,CrownBtn,BackCrownBtn,TrophyBtn,BackTrophyBtn;
	[SerializeField]
	private Button ProfileChangeCharacterBtn, profileEquipItemsBtn,ProfileChangeNameBtn,ProfileBackBtn,creditsBtn,creditsBackBtn,guideBtn,guideBackBtn,guideNextBtn,guidePrevBtn;
	[SerializeField]
	private  Button SinglePlayerExitButton,offlineSinglePlayerButton,offlineLobbyButton,offlineBackButton,onlineLobbyButton,onlineFindMatchButton;
	[SerializeField]
	private Button BookBtn,BackBookBtn,playButton, settingButton,backSettingBtn, storeButton, creditsButton, guideButton, profileButton, gameModeBackButton, offlineButton, onlineButton,onlineModeBackButton;
	[SerializeField]
	private Button nextCharacterBtn, prevCharacterButton, backCharacterSelectionButton,changeNameButton,backChangeNameButton,backEquipItemsButton,backStoreButton;
	private int currentCharacterIndex;
	void OnEnable(){
		
		SetUpMainMenuButton ();
		SetUpCharacterSelectionButton ();
		SetUpStoreBtn ();
		SetUpChangeNameButton ();
		SetUpProfileButton ();
		SetUpCreditsBtn ();
		SetUpGuideBtn ();
		SetUpOfflineLobbyButtons ();
		SetUpOnlineLobbyButtons ();
	}


	void OnLevelWasLoaded(int sceneIndex){
		if (sceneIndex == 0) {
			SetUpMainMenuButton ();
			SetUpStoreBtn ();
			SetUpCharacterSelectionButton ();
			SetUpChangeNameButton ();
			SetUpProfileButton ();
			SetUpCreditsBtn ();
			SetUpGuideBtn ();
			SetUpOfflineLobbyButtons ();
			SetUpOnlineLobbyButtons ();
		} else if (sceneIndex == 1) {
			

		}
		Time.timeScale = 1;
	}

	//private void SetUpButtonExit(){
		
		//SinglePlayerExitButton.onClick.RemoveAllListeners ();
		//SinglePlayerExitButton.onClick.AddListener (delegate {myLoadSceneScript.LoadScene(0,MainMenuStartPanel);});

	//}
	private void SetUpStoreBtn (){
		storeButton.onClick.RemoveAllListeners ();
		storeButton.onClick.AddListener (delegate{buttonSetUp (MainMenuStorePanel, cameraTargetsPos [3]);});
		backSettingBtn.onClick.RemoveAllListeners ();
		backSettingBtn.onClick.AddListener (delegate{buttonSetUp (MainMenuStartPanel, cameraTargetsPos [0]);});
		backStoreButton.onClick.RemoveAllListeners ();
		backStoreButton.onClick.AddListener (delegate{buttonSetUp (MainMenuStartPanel, cameraTargetsPos [0]);});
	}
	private void SetUpOnlineLobbyButtons(){
		onlineLobbyButton.onClick.RemoveAllListeners ();
		onlineLobbyButton.onClick.AddListener(delegate{buttonSetUp (onlineCreateJoinLobbyPanel, cameraTargetsPos [3]);});

		onlineCreateLobbyBtn.onClick.RemoveAllListeners ();
		onlineCreateLobbyBtn.onClick.AddListener (delegate{buttonSetUp (onlineCreateLobbyPanel, cameraTargetsPos [5]);});

		onlineJoinLobbyBtn.onClick.RemoveAllListeners ();
		onlineJoinLobbyBtn.onClick.AddListener (myLobbyMainMenuScript.JoinLobbyMatchMakingBtn);


		onlineCreateBtn.onClick.RemoveAllListeners ();
		onlineCreateBtn.onClick.AddListener (StartOnlineLobbyHost);

		onlineCreateJoinlobbyBackBtn.onClick.RemoveAllListeners ();
		onlineCreateJoinlobbyBackBtn.onClick.AddListener (delegate{buttonSetUp (MainMenuOnlineModePanel, cameraTargetsPos [3]);});

		onlineCreateLobbyBackBtn.onClick.RemoveAllListeners ();
		onlineCreateLobbyBackBtn.onClick.AddListener (delegate {buttonSetUp (onlineCreateJoinLobbyPanel, cameraTargetsPos [4]);});

		onlineJoinLobbyBackBtn.onClick.RemoveAllListeners ();
		onlineJoinLobbyBackBtn.onClick.AddListener (delegate {buttonSetUp (onlineCreateJoinLobbyPanel, cameraTargetsPos [4]);});
	}
	private void SetUpOfflineLobbyButtons(){
		offlineLobbyButton.onClick.RemoveAllListeners ();
		offlineLobbyButton.onClick.AddListener (delegate{buttonSetUp (offlineCreateJoinLobbyPanel, cameraTargetsPos [5]);});

		offlineCreateLobbyBtn.onClick.RemoveAllListeners ();
		offlineCreateLobbyBtn.onClick.AddListener (delegate{buttonSetUp (offlineCreateLobbyPanel, cameraTargetsPos [5]);});

		offlineJoinLobbyBtn.onClick.RemoveAllListeners ();
		offlineJoinLobbyBtn.onClick.AddListener (myLobbyMainMenuScript.StartLobbyClientBtn);

		offlineCreateBtn.onClick.RemoveAllListeners ();
		offlineCreateBtn.onClick.AddListener (StartOfflineLobbyHost);

		offlineCreateJoinlobbyBackBtn.onClick.RemoveAllListeners ();
		offlineCreateJoinlobbyBackBtn.onClick.AddListener (delegate{buttonSetUp (MainMenuOfflineModePanel, cameraTargetsPos [3]);});

		offlineCreateLobbyBackBtn.onClick.RemoveAllListeners ();
		offlineCreateLobbyBackBtn.onClick.AddListener (delegate {buttonSetUp (offlineCreateJoinLobbyPanel, cameraTargetsPos [4]);});
	}
	private void StartOnlineLobbyHost(){
		Custom_NetworkLobbyManager._LMSingleton.lobbyName = onlineLobbyNameInput.text;
		Custom_NetworkLobbyManager._LMSingleton.category = catergories [onlineCategoryDropdown.value];
		myLobbyMainMenuScript.CreateLobbyMatchMakingBtn ();
	}
	private void StartOfflineLobbyHost(){
		Custom_NetworkLobbyManager._LMSingleton.lobbyName = offlineLobbyNameInput.text;
		Custom_NetworkLobbyManager._LMSingleton.category = catergories [offlineCategoryDropdown.value];
		Custom_NetworkLobbyManager._LMSingleton.CurrentCategoryIndex = offlineCategoryDropdown.value;
		Custom_NetworkLobbyManager._LMSingleton.playScene = "2PlayerBattleStage" + offlineCategoryDropdown.value.ToString ();
		myLobbyMainMenuScript.StartLobbyHostBtn ();
	}
	void OpenEquipPanel(){
		Inventory.Instance.MyInventoryUi.GetComponent<Animator> ().SetTrigger ("Open");
	}
	private void SetUpProfileButton(){



		profileEquipItemsBtn.onClick.RemoveAllListeners ();
		profileEquipItemsBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuEquiItemsPanel, cameraTargetsPos [3]);});
		profileEquipItemsBtn.onClick.AddListener (OpenEquipPanel);

		ProfileBackBtn.onClick.RemoveAllListeners ();
		ProfileBackBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuStartPanel,cameraTargetsPos[0]);});

		ProfileChangeCharacterBtn.onClick.RemoveAllListeners ();
		ProfileChangeCharacterBtn.onClick.AddListener (delegate{Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel(MainMenuProfileChangeCharacterPanel);});

		ChestBtn.onClick.RemoveAllListeners ();
		ChestBtn.onClick.AddListener (OpenChestPanel);

		CrownBtn.onClick.RemoveAllListeners ();
		CrownBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuCrownPanel,cameraTargetsPos[2]);});

		BackCrownBtn.onClick.RemoveAllListeners ();
		BackCrownBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuStartPanel,cameraTargetsPos[0]);});

		BackToChestPanelBtn.onClick.RemoveAllListeners ();
		BackToChestPanelBtn.onClick.AddListener (ChestManager.Instance.BackToChestPanel);

		OpenChestBtn.onClick.RemoveAllListeners ();
		OpenChestBtn.onClick.AddListener (ChestManager.Instance.OpenCurrentChest);

		BackChestBtn.onClick.RemoveAllListeners ();
		BackChestBtn.onClick.AddListener (BackChestBtnF);

		ProfileChangeNameBtn.onClick.RemoveAllListeners ();
		ProfileChangeNameBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuProfileChangeNamePanel,cameraTargetsPos[1]);});
	}
	private void OpenChestPanel(){
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel(MainMenuChestPanel);
		if (ChestManager.Instance.IsInventoryEmpty()) {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			if (languageIndex == 0) {
				Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Chests Storage is Empty!!");
			} else {
				Custom_NetworkLobbyManager._LMSingleton.CreateWarningText ("Walang Laman ang Imabakan ng Kayamanan!!");
			}
		}
	}
	private void SetUpGuideBtn(){
		guideBtn.onClick.RemoveAllListeners ();
		guideBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuGuidePanel,cameraTargetsPos[5]);});

		guideBackBtn.onClick.RemoveAllListeners ();
		guideBackBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuStartPanel,cameraTargetsPos[0]);});


		guideNextBtn.onClick.RemoveAllListeners ();
		guideNextBtn.onClick.AddListener (myGuidePanelScript.NextBtn);

		guidePrevBtn.onClick.RemoveAllListeners ();
		guidePrevBtn.onClick.AddListener (myGuidePanelScript.PrevBtn);
			
	}
	private  void  SetUpCreditsBtn(){
		creditsBtn.onClick.RemoveAllListeners ();
		creditsBtn.onClick.AddListener (delegate {buttonSetUp(MainMenuCreditsPanel,cameraTargetsPos[5]);});
	
		creditsBackBtn.onClick.RemoveAllListeners ();
		creditsBackBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuStartPanel, cameraTargetsPos [0]);});
	}
	private void SetUpChangeNameButton(){
		backEquipItemsButton.onClick.RemoveAllListeners ();
		backEquipItemsButton.onClick.AddListener (delegate {buttonSetUp(MainMenuProfilePanel,cameraTargetsPos[1]);});

		changeNameButton.onClick.RemoveAllListeners ();
		changeNameButton.onClick.AddListener (ChangetheName);

		backChangeNameButton.onClick.RemoveAllListeners ();
		backChangeNameButton.onClick.AddListener (delegate {buttonSetUp(MainMenuProfilePanel,cameraTargetsPos[1]);});
	}
	private void SetUpCharacterSelectionButton(){
		backCharacterSelectionButton.onClick.RemoveAllListeners ();
		backCharacterSelectionButton.onClick.AddListener (backCharacterSelection);

		nextCharacterBtn.onClick.RemoveAllListeners ();
		nextCharacterBtn.onClick.AddListener (myCharacterSelectionScript.NextCharacter);

		prevCharacterButton.onClick.RemoveAllListeners ();
		prevCharacterButton.onClick.AddListener (myCharacterSelectionScript.PrevCharacter);
	}
	private void backCharacterSelection(){
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel(MainMenuProfilePanel);
		myCharacterSelectionScript.DisableCharacterSelectionCamera ();
	}

	private  void SetUpMainMenuButton(){
		BookBtn.onClick.RemoveAllListeners ();
		BookBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuBookPanel,cameraTargetsPos [2]);});

		BackBookBtn.onClick.RemoveAllListeners ();
		BackBookBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuStartPanel,cameraTargetsPos [0]);});

		TrophyBtn.onClick.RemoveAllListeners ();
		TrophyBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuTrophyPanel,cameraTargetsPos [4]);});

		BackTrophyBtn.onClick.RemoveAllListeners ();
		BackTrophyBtn.onClick.AddListener (delegate {buttonSetUp (MainMenuCrownPanel,cameraTargetsPos [2]);});

		playButton.onClick.RemoveAllListeners ();
		playButton.onClick.AddListener (delegate {buttonSetUp (MainMenuOfflineModePanel, cameraTargetsPos [2]);});

		settingButton.onClick.RemoveAllListeners ();
		settingButton.onClick.AddListener (delegate {buttonSetUp(MainMenuSettingPanel,cameraTargetsPos[2]);});

		profileButton.onClick.RemoveAllListeners ();
		profileButton.onClick.AddListener (delegate {buttonSetUp(MainMenuProfilePanel,cameraTargetsPos[1]);});

		gameModeBackButton.onClick.RemoveAllListeners ();
		gameModeBackButton.onClick.AddListener (delegate {buttonSetUp(MainMenuStartPanel,cameraTargetsPos[0]);});
		 
		onlineButton.onClick.RemoveAllListeners ();
		onlineButton.onClick.AddListener (delegate {buttonSetUp(MainMenuOnlineModePanel,cameraTargetsPos[3]);});


		offlineButton.onClick.RemoveAllListeners ();
		offlineButton.onClick.AddListener (delegate {buttonSetUp (MainMenuOfflineModePanel, cameraTargetsPos [4]);});

		onlineModeBackButton.onClick.RemoveAllListeners ();
		onlineModeBackButton.onClick.AddListener (delegate {buttonSetUp(MainMenuGameModePanel,cameraTargetsPos[1]);});

		offlineSinglePlayerButton.onClick.RemoveAllListeners ();
		offlineSinglePlayerButton.onClick.AddListener (delegate {myLoadSceneScript.LoadScene(1,Custom_NetworkLobbyManager._LMSingleton.SinglePlayerPanel);});

		offlineBackButton.onClick.RemoveAllListeners ();
		offlineBackButton.onClick.AddListener(delegate {buttonSetUp(MainMenuStartPanel,cameraTargetsPos[0]);});
	 
	
	}
	private void buttonSetUp(RectTransform targetPanel,Vector3 targetCamera){
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (targetPanel);
		Camera.main.GetComponent<CameraMovemmentStartMenu> ().SetTarget (targetCamera);
		buttonSound.Play ();
	}
	private void BackChestBtnF(){
		
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (MainMenuStartPanel);
		ChestManager.Instance.ChestPanelScript.DisableChestPanel ();
	}
	private void ChangetheName(){
		myChangeNameScript.ChangetheName ();
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (MainMenuProfilePanel);
	
	}

}
