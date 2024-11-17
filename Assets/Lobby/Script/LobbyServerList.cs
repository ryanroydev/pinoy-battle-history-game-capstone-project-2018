using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
public class LobbyServerList : MonoBehaviour {
	private Custom_NetworkLobbyManager myLobbyManager;
	[SerializeField]
	private GameObject ServerListItem;
	[SerializeField]
	private RectTransform ServerListRect;

	private int currentPage, previousPage;
	// Use this for initialization

	void OnEnable(){
		if (myLobbyManager == null) {
			myLobbyManager = GameObject.FindObjectOfType<Custom_NetworkLobbyManager> ();
		}
		currentPage = 0;
		previousPage = 0;
		foreach (Transform t in ServerListRect) {
			Destroy (t.gameObject);
		}
		RequestPage (0);
	}

	
	public void OnMatchList(bool success,string extendedinfo,List<MatchInfoSnapshot> matches){
		if (matches.Count == 0) {
			return;
		}
		for (int i = 0; i < matches.Count; i++) {
			GameObject go = Instantiate (ServerListItem) as GameObject;
		
			go.GetComponent<LobbyServerEntry> ().Populate (matches [i], myLobbyManager, Custom_NetworkLobbyManager._LMSingleton.category);
			go.transform.SetParent (ServerListRect, false);
		}
	}
	public void RequestPage(int newPage){
		previousPage = currentPage;
		currentPage = newPage;
		myLobbyManager.matchMaker.ListMatches (newPage, 6, "", true, 0, 0, OnMatchList);
	}
}
