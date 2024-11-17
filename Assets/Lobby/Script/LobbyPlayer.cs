using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LobbyPlayer : NetworkLobbyPlayer {
	[SerializeField]
	private Text nameText, teamText,levelText;
	[SerializeField]
	private Button kickPlayerBtn, readyBtn,waitingBtn;

	public string playerName = "";

	private string category="";
	private string lobbyName = "";
	private string playerLevel = "";

	static Color joinColor = new Color (1, 0, 0.4f, 1);
	static Color notReadyColor = new Color (233f, 233f, 233f, 255f);
	static Color readyColor = new Color (0, 0.8f, 0.8f, 1.0f);
	static Color transparentColor = new Color (0, 0, 0, 0);



	public override void OnClientEnterLobby ()
	{
		base.OnClientEnterLobby ();
		if (Custom_NetworkLobbyManager._LMSingleton != null) {
			Custom_NetworkLobbyManager._LMSingleton.OnPlayersNumberModified (1);
		}
		LobbyPlayerList.Instance.AddPlayer (this);

		SetUpOtherPlayer ();

		if (Custom_NetworkLobbyManager._LMSingleton != null) {
			Custom_NetworkLobbyManager._LMSingleton.OnPlayersNumberModified (0);
		}


	}


	public void ToggleJoinButton(bool c){
		
		readyBtn.gameObject.SetActive (c);
		waitingBtn.gameObject.SetActive (!c);
	}

	private void SetUpLocalPlayer(){
		string yourName = (PlayerPrefs.HasKey ("PlayerName")) ? PlayerPrefs.GetString ("PlayerName") : "Player" + (LobbyPlayerList.Instance.PlayerListContentTransform.childCount);
		int yourLevel = (PlayerPrefs.HasKey ("PlayerLevel")) ? PlayerPrefs.GetInt ("PlayerLevel") : 1;
		CmdChangeNameLevel (yourName, yourLevel.ToString ());

		readyBtn.interactable = true;

		checkRemoveButton ();
		changeReadyButtonColor (joinColor);

		readyBtn.onClick.RemoveAllListeners ();
		readyBtn.onClick.AddListener (OnReadyPlayer);


		kickPlayerBtn.onClick.RemoveAllListeners ();
		kickPlayerBtn.onClick.AddListener (onKickPlayerBtn);
	}
	private void OnReadyPlayer(){
		if(isLocalPlayer){
		 SendReadyToBeginMessage ();
			string yourName = (PlayerPrefs.HasKey ("PlayerName")) ? PlayerPrefs.GetString ("PlayerName") : "Player" + (LobbyPlayerList.Instance.PlayerListContentTransform.childCount);
			int yourLevel = (PlayerPrefs.HasKey ("PlayerLevel")) ? PlayerPrefs.GetInt ("PlayerLevel") : 1;
			CmdChangeNameLevel (yourName, yourLevel.ToString ());

		}	
	}
	private void onKickPlayerBtn(){
		if (isLocalPlayer) {

			RemovePlayer ();
			Custom_NetworkLobbyManager._LMSingleton.StopHost ();
		} else if (isServer) {

			Custom_NetworkLobbyManager._LMSingleton.KickPlayer (connectionToClient);

		} else {

		}
	}

	public void UpdateYourNameANdLevel(){
		string yourName = (PlayerPrefs.HasKey ("PlayerName")) ? PlayerPrefs.GetString ("PlayerName") : "Player" + (LobbyPlayerList.Instance.PlayerListContentTransform.childCount);
		int yourLevel = (PlayerPrefs.HasKey ("PlayerLevel")) ? PlayerPrefs.GetInt ("PlayerLevel") : 1;
		CmdChangeNameLevel (yourName, yourLevel.ToString ());
	
	}
	public  void UpdateLobbyName(){
		ServerGetLobbyName ();

	}
	[ClientRpc]
	public void RpcDisableKickPlayerBtn(){
		DisableKickPlayerBtn ();
	
	}
	[ClientRpc]
	public void RpcDisableBackBtn(){
		Custom_NetworkLobbyManager._LMSingleton.LobbyBackBtn.interactable = false;
	}
	private void DisableKickPlayerBtn(){
		kickPlayerBtn.interactable = false;
	}

	[Server]
	private void ServerGetLobbyName(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		lobbyName =	Custom_NetworkLobbyManager._LMSingleton.lobbyName;
		category = Custom_NetworkLobbyManager._LMSingleton.category;
		Custom_NetworkLobbyManager._LMSingleton.lobbyNameText.text = lobbyName;
		if (languageIndex == 0) {
			Custom_NetworkLobbyManager._LMSingleton.categoryText.text = "Category : " + category;
		} else {
			Custom_NetworkLobbyManager._LMSingleton.categoryText.text = "Kategorya : " + category;
		}
		RpcUpdateClientLobbyNameAndCategory (lobbyName, category);

	}
	[ClientRpc]
	private void RpcUpdateClientLobbyNameAndCategory(string newLobbyName,string newCategory){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		lobbyName =	newLobbyName;
		category = newCategory;
		Custom_NetworkLobbyManager._LMSingleton.lobbyNameText.text = newLobbyName;
		if (languageIndex == 0) {
			Custom_NetworkLobbyManager._LMSingleton.categoryText.text = "Category : " + newCategory;
		} else {
			Custom_NetworkLobbyManager._LMSingleton.categoryText.text = "Kategorya : " + newCategory;
		}
	}
	[Command]
	public void CmdChangeNameLevel(string name,string level){
		playerName = name;
		playerLevel = "Level " + level;
		RpcUpdateNameLevel (name,level);
	}

	[ClientRpc]
	public void RpcUpdateRemoveButton(){
		checkRemoveButton ();
	}

	[ClientRpc]
	public void RpcUpdateNameLevel(string name,string level){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);

		playerName = name;
		if (languageIndex == 0) {
			playerLevel = "Level " + level;
			levelText.text = "Level " + level;
		} else {
			playerLevel = "Antas " + level;
			levelText.text = "Antas " + level;
		}
		nameText.text = name;
		if (isLocalPlayer) {
			Custom_NetworkLobbyManager._LMSingleton.PlayerNameText.text = name + " lvl" + level;
		} else {
			Custom_NetworkLobbyManager._LMSingleton.OpponentNameText.text = name + " lvl" + level;
		}
	}

	[ClientRpc]
	public void RpcUpdateLevel(string level){
		playerLevel = level;
		levelText.text = level;

	}

	[ClientRpc]
	public void RpcUpdateCountDownTime (int floorTime){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			Custom_NetworkLobbyManager._LMSingleton.CountDownPanel.CountDownText.text = "Match Start In " + floorTime.ToString ();
		} else {
			Custom_NetworkLobbyManager._LMSingleton.CountDownPanel.CountDownText.text = "Magsisimula na ang laban sa " + floorTime.ToString ();
		}
		Custom_NetworkLobbyManager._LMSingleton.CountDownPanel.gameObject.SetActive (floorTime != 0);
		
	}


	private void checkRemoveButton (){
		if (isServer) {
		     //int localPlayerCount = 0;
			//	foreach (PlayerController pc in ClientScene.localPlayers) {
				//localPlayerCount += (pc == null || pc.playerControllerId == -1) ? 0 : 1;
			//}
			//kickPlayerBtn.interactable = localPlayerCount > 1;
			kickPlayerBtn.interactable = true;
		} else if (isLocalPlayer) {
			kickPlayerBtn.interactable = true;
			
		} else {
			kickPlayerBtn.interactable = false;
		}
	}
	private void changeReadyButtonColor (Color c){
		ColorBlock b = readyBtn.colors;
		b.normalColor = c;
		b.pressedColor = c;
		b.highlightedColor = c;
		b.disabledColor = c;
		readyBtn.colors = b;

	}

	private void SetUpOtherPlayer(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		kickPlayerBtn.onClick.RemoveAllListeners ();
		kickPlayerBtn.onClick.AddListener (onKickPlayerBtn);

	
		kickPlayerBtn.interactable = NetworkServer.active;
		changeReadyButtonColor (notReadyColor);

		readyBtn.transform.GetChild (0).GetComponent<Text> ().text = (languageIndex == 0) ? "Ready" : "Handa";
		readyBtn.interactable = false;
	
		//OnClientReady (false);
	}


	public override void OnClientReady (bool readyState)
	{
		
		if (readyState) {

			changeReadyButtonColor (transparentColor);
			Text textComponent = readyBtn.transform.GetChild (0).GetComponent<Text> ();
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			textComponent.text = (languageIndex == 0) ? "Ready" : "Handa";
			textComponent.color = readyColor;
			readyBtn.interactable = false;
			//kickPlayerBtn.interactable = false;
		} else {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			changeReadyButtonColor (isLocalPlayer ? joinColor : notReadyColor);
			Text textComponent = readyBtn.transform.GetChild (0).GetComponent<Text> ();
			if (languageIndex == 0) {
				textComponent.text = isLocalPlayer ? "Join" : "....";
			} else {
				textComponent.text = isLocalPlayer ? "Sali" : "....";
			}
			textComponent.color = Color.white;
			readyBtn.interactable = isLocalPlayer;
			if (isServer) {
				kickPlayerBtn.interactable = true;
			} else if (isLocalPlayer) {
				kickPlayerBtn.interactable = isLocalPlayer;
			}
		}
	}
	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		readyBtn.transform.GetChild (0).GetComponent<Text> ().color = Color.white;
		SetUpLocalPlayer ();
	}
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
	
	}
	public void OnPlayerListChanged(int i){
		teamText.text = (i == 0) ? "Player1" : "Player2";


	}
	public void OnDestroy(){
		LobbyPlayerList.Instance.RemovePlayer (this);
		if (Custom_NetworkLobbyManager._LMSingleton != null) {
			Custom_NetworkLobbyManager._LMSingleton.OnPlayersNumberModified (-1);

		}
	}
}
