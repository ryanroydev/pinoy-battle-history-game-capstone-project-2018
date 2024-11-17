using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyPlayerList : MonoBehaviour {
	public List<LobbyPlayer> _Players = new List<LobbyPlayer> ();
	public static LobbyPlayerList Instance;
	[SerializeField]
	private RectTransform playerListContentTransform;
	[SerializeField]
	private Text VersusText;
	public RectTransform PlayerListContentTransform{get{ return playerListContentTransform;}set{ this.playerListContentTransform = value;}}
	void OnEnable(){
		Instance = this;
	}
	public void AddPlayer(LobbyPlayer newPlayer){
		if (_Players.Contains (newPlayer)) {
			return;
		}
		_Players.Add (newPlayer);

		newPlayer.transform.SetParent (playerListContentTransform, false);

		playerListModified ();
	}
	public void RemovePlayer(LobbyPlayer player){
		_Players.Remove (player);
		playerListModified ();
	}
	private void playerListModified(){

		int i = 0;
		foreach (LobbyPlayer lb in _Players) {

			lb.OnPlayerListChanged(i);
			++i;
		}
		if (i == 2) {
			VersusText.gameObject.SetActive (true);
		} else {
			VersusText.gameObject.SetActive (false);
		}
	}
}
