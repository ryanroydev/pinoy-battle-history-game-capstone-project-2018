using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyMainMenu : MonoBehaviour {
	private Custom_NetworkLobbyManager myLobbyManager;
	[SerializeField]
	private InputField inputLobbyName;
	[SerializeField]
	private RectTransform LobbyServerListPanel;
	[SerializeField]
	private RectTransform ExitPanel;


	// Use this for initialization
	void OnEnable () {
		if (myLobbyManager == null) {
			myLobbyManager = GameObject.FindObjectOfType<Custom_NetworkLobbyManager> ();
		}
	}
	
	public void StartLobbyHostBtn(){
		myLobbyManager.StartHost ();

	}
	public void StartLobbyClientBtn(){
		//myLobbyManager.ChangeToPanel (myLobbyManager.LobbyPanel);
		myLobbyManager.StartClient ();
	}
	public void CreateLobbyMatchMakingBtn(){
		myLobbyManager.StartMatchMaker ();
		myLobbyManager.matchMaker.CreateMatch (Custom_NetworkLobbyManager._LMSingleton.lobbyName, (uint)myLobbyManager.maxPlayers, true, "", "", "", 0, 0, myLobbyManager.OnMatchCreate);
	}
	public void JoinLobbyMatchMakingBtn(){
		myLobbyManager.StartMatchMaker ();
		myLobbyManager.ChangeToPanel (LobbyServerListPanel);
	}
	public void ConfirmationPanel(){
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerSettingPanel.gameObject.SetActive (false);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerButtonSettingPanel.gameObject.SetActive (false);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerConfirmationPanel.gameObject.SetActive (true);
	}
	public void NoToDisconnect(){
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerSettingPanel.gameObject.SetActive (true);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerButtonSettingPanel.gameObject.SetActive (false);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerConfirmationPanel.gameObject.SetActive (false);
	}
	public void Disconnect(){
		Custom_NetworkLobbyManager._LMSingleton.StopHost ();
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.StartPanel);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerConfirmationPanel.gameObject.SetActive (false);
	}
	public void Surrender(){
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerConfirmationPanel.gameObject.SetActive (false);
		if (Custom_NetworkLobbyManager._LMSingleton.YourPlayer != null) {
			Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerScript> ().EnemyWon ();
		} else {
			Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerScript> ().ClearPlayerList ();
			Disconnect ();
		}
	}
	public void OpenSetting(bool isOpen){

		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerSettingPanel.gameObject.SetActive (isOpen);
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerButtonSettingPanel.gameObject.SetActive (!isOpen);
	}
	public void YesToExit(){
		Application.Quit ();
	}
	public void NoToExit(){
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.StartPanel);
	}
	public void OpenExitPanel(){

		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (ExitPanel);
	}
	//insert string listener
}
