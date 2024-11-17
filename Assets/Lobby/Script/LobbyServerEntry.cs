using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
public class LobbyServerEntry : MonoBehaviour {
	[SerializeField]
	private Text lobbyNameText,lobbyMaxMinPlayerText,lobbyCategoryText;
	[SerializeField]
	private Button joinBtn;
	// Use this for initialization
	void Start () {
		
	}
	
	public void Populate(MatchInfoSnapshot match,Custom_NetworkLobbyManager myLobbyManager,string category){
		lobbyNameText.text = match.name;
		lobbyMaxMinPlayerText.text = match.currentSize.ToString () + "/" + match.maxSize;

		lobbyCategoryText.text = category;

		joinBtn.onClick.RemoveAllListeners ();
		joinBtn.onClick.AddListener (() => joinMatch (match.networkId,myLobbyManager));
	}
	void joinMatch (NetworkID netId,Custom_NetworkLobbyManager lobbyManager){
		lobbyManager.matchMaker.JoinMatch (netId, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
		//add more function
	}
}
