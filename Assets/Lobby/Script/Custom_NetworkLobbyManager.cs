using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;
using UnityEngine .UI;
using UnityEngine.SceneManagement;
public class Custom_NetworkLobbyManager : NetworkLobbyManager {
	public Image PlayerTrophyImage;
	public Image OpponentTrophyImage;
	[SerializeField]
	private Sprite[] TrophySprite;

	public RectTransform MainMenuChestPanel;
	public RectTransform EmptyPanel;
	public Button OpenChestBtn;
	public string CoinPicked = "";

	[SerializeField]
	private Transform PopTextPos;
	[SerializeField]
	private Text goldText;
	[SerializeField]
	private FloatingText warningfloatingText,sucessfloatingText;
	[SerializeField]
	private GameObject LoadPanel;
	[SerializeField]
	private Slider LoadBar;
	[SerializeField]
	private Text LoadPercentText;



    public Vector3[] characterSpawnPos;
	[SerializeField]
	private Quaternion[] characterSpawnRot;

	private Transform hostNormalCameraPos, hostBattleCameraPos, clientNormalCameraPos, clientBattleCameraPos;

	private TwoPlayerBattleCameraMovement twoPlayerBattle_PlayerCameraScript;
	public LobbyCountDownPanel CountDownPanel;

	//set trget
	public GameObject YourPlayer; 
	public GameObject TargetPlayer;

	//target

	public Bar PlayerHealthBar;
	public Bar OpponentHealthBar;
	public RectTransform LobbyPanel;
	public RectTransform TwoPlayerBattlePanel;
	public RectTransform CreateAndJoinLobbyPanel;
	public Text PlayerNameText;
	public Text OpponentNameText;

	public Button LobbyBackBtn;
	public RectTransform TwoPlayerConfirmationPanel;
	public Button TwoPlayerYesBtn;
	public Button TwoPlayerNoBtn;
	public TwoPlayer_QuizManager TwoPlayerQuizManager;
	public RectTransform TwoPlayerCountDownPanel;
	public Text TwoPlayerCountDownText;
	public RectTransform TwoPlayerSettingPanel;
	public RectTransform TwoPlayerStatPanel;
	public RectTransform TwoPlayerButtonSettingPanel;
	public Text TwoPlayerTimerText;
	public RectTransform WaitingPanel;

	public short MsgKicked = MsgType.Highest + 1;
	[SerializeField]
	private float countDownTime = 5;
	private bool DisconnectServer;

	public string category = "";
	public string lobbyName = ""; 
	public Text lobbyNameText;
	public Text categoryText;

	private int _playersNumber;
	private ulong currentMatchId;



	//Panels  
	[SerializeField]
	private RectTransform startPanel;
	public RectTransform StartPanel{get{ return startPanel;}set{ this.startPanel = value;}}
	[SerializeField]
	private RectTransform singlePlayerPanel;
	public RectTransform SinglePlayerPanel{get{ return singlePlayerPanel;}set{ this.singlePlayerPanel = value;}}
	private RectTransform currentPanel;

	private int myCharacterIndex;
	public int MyCharacterIndex{get{ return myCharacterIndex;}set{ this.myCharacterIndex = value;}}
	public static Custom_NetworkLobbyManager _LMSingleton;
	public TwoPlayerBattleCameraMovement TwoPlayerBattle_PlayerCameraScript{get{ return twoPlayerBattle_PlayerCameraScript;}set{ this.twoPlayerBattle_PlayerCameraScript = value;}}


	//cameraPosPublic
	public Transform HostNormalCameraPos{get{return hostNormalCameraPos;}set{this.hostNormalCameraPos=value;}}
	public Transform HostBattleCameraPos{get{return hostBattleCameraPos;}set{this.hostBattleCameraPos=value;}}
	public Transform ClientNormalCameraPos{get{return clientNormalCameraPos;}set{this.clientNormalCameraPos=value;}}
	public Transform ClientBattleCameraPos{get{return clientBattleCameraPos;}set{this.clientBattleCameraPos=value;}}
	public int CurrentCategoryIndex;
	void OnLevelWasLoaded(int sceneIndex){



		Custom_NetworkLobbyManager._LMSingleton.LobbyBackBtn.interactable = true;
		if (sceneIndex >= 11) {//2playerBattleScenes
			hostBattleCameraPos = GameObject.Find ("HostBattleCameraPos").transform;
			hostNormalCameraPos = GameObject.Find ("HostNormalCameraPos").transform;
			clientBattleCameraPos = GameObject.Find ("ClientBattleCameraPos").transform;
			clientNormalCameraPos = GameObject.Find ("ClientNormalCameraPos").transform;
			twoPlayerBattle_PlayerCameraScript = Camera.main.GetComponent<TwoPlayerBattleCameraMovement> ();
		} 
		Time.timeScale = 1;
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			StatsManager.Instance.addGold (1000);
		}
	}
	public void SetPlayerTrophyImage(int TrophyIndex){
		PlayerTrophyImage.sprite = TrophySprite [TrophyIndex];
	}
	public void SetOpponentTrophyImage(int TrophyIndex){
		OpponentTrophyImage.sprite = TrophySprite [TrophyIndex];
	}
	public void CreateWarningText(string infoText){
		FloatingText ft = Instantiate (warningfloatingText);

		ft.transform.SetParent (this.transform, false);
		ft.transform.position = PopTextPos.position;
		ft.SetText (infoText);
	}
	public void CreatePoppUpText(string infoText){
		FloatingText ft = Instantiate (sucessfloatingText);

		ft.transform.SetParent (this.transform, false);
		ft.transform.position = PopTextPos.position;
		ft.SetText (infoText);
	}
	private void OnEnable(){
		_LMSingleton = this;

		int value = PlayerPrefs.GetInt ("CurrentGraphics", 4);
		QualitySettings.SetQualityLevel (value);

	}

	public override void OnStopHost ()
	{
		base.OnStopHost ();
		ChangeToPanel (startPanel);
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			CreateWarningText ("Server Disconnected!!");
		} else {
			CreateWarningText ("Nawalan ng Koneksyon sa Server!!");
		}
	}
	public override void OnStartClient (NetworkClient lobbyClient)
	{
		base.OnStartClient (lobbyClient);
		ChangeToPanel (LobbyPanel);
	}
	public override void OnLobbyClientAddPlayerFailed ()
	{
		base.OnLobbyClientAddPlayerFailed ();
		ChangeToPanel (startPanel);
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			CreateWarningText ("Failed to Add LobbyPlayer!!");
		} else {
			CreateWarningText ("Failed to Add LobbyPlayer!!");
		}
	}
	public override void OnStartHost ()
	{
		base.OnStartHost ();
		ChangeToPanel (LobbyPanel);
	}

	public override void OnMatchCreate (bool success, string extendedInfo, UnityEngine.Networking.Match.MatchInfo matchInfo)
	{
		base.OnMatchCreate (success, extendedInfo, matchInfo);
		currentMatchId = (ulong)matchInfo.networkId;
	}

	public override void OnDestroyMatch (bool success, string extendedInfo)
	{
		base.OnDestroyMatch (success, extendedInfo);
		if (DisconnectServer) {
			StopMatchMaker ();
			StopHost ();
		}

	}

	public override GameObject OnLobbyServerCreateLobbyPlayer (NetworkConnection conn, short playerControllerId)
	{
		GameObject obj = Instantiate (lobbyPlayerPrefab.gameObject) as GameObject;
		LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer> ();
	
		newPlayer.ToggleJoinButton (numPlayers + 1 >= minPlayers);
		newPlayer.UpdateLobbyName ();
		for (int i = 0; i < lobbySlots.Length; i++) {
			LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;
			if (lp != null) {
				lp.RpcUpdateRemoveButton ();
				lp.ToggleJoinButton (numPlayers + 1 >= minPlayers);
				lp.UpdateYourNameANdLevel ();

				lp.UpdateLobbyName ();

			}
		}

		return obj;
		//return base.OnLobbyServerCreateLobbyPlayer (conn, playerControllerId);
	}

	public override void OnLobbyClientEnter ()
	{
		//base.OnLobbyClientEnter ();

		for (int i = 0; i < lobbySlots.Length; i++) {
			LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;

			if (lp != null) {
				
				lp.RpcUpdateRemoveButton ();
				lp.ToggleJoinButton (numPlayers + 1 >= minPlayers);

			}
		}
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			CreatePoppUpText ("You are Connected!!");
		} else {
			CreatePoppUpText ("Ikaw ay Nakakonekta!!");
		}
	}
	public override void OnLobbyServerPlayerRemoved (NetworkConnection conn, short playerControllerId)
	{
		for (int i = 0; i < lobbySlots.Length; i++) {
			LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;
			if (lp != null) {
				lp.RpcUpdateRemoveButton ();
				lp.ToggleJoinButton (numPlayers + 1 >= minPlayers);
			}
		}

	}
	public override void OnLobbyClientDisconnect (NetworkConnection conn)
	{
		
		for (int i = 0; i < lobbySlots.Length; i++) {
			LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;
			if (lp != null) {
				lp.RpcUpdateRemoveButton ();
				lp.ToggleJoinButton (numPlayers + 1 >= minPlayers);
			}
		}

	}
	public override void OnLobbyServerDisconnect (NetworkConnection conn)
	{
		for (int i = 0; i < lobbySlots.Length; i++) {
			LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;
			if (lp != null) {
				lp.RpcUpdateRemoveButton ();
				lp.ToggleJoinButton (numPlayers + 1 >= minPlayers);
			}
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			if (languageIndex == 0) {
				CreateWarningText ("Server Disconnected!!");
			} else {
				CreateWarningText ("Nawalan ng Koneksyon sa Server!!");
			}
		}

	}
	public override void OnLobbyServerPlayersReady ()
	{
		bool allReady = true;
		for (int i = 0; i < lobbySlots.Length; i++) {
			if (lobbySlots [i] != null) {
				allReady &= lobbySlots [i].readyToBegin;
			
			}
		}
		if (allReady) {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			if (languageIndex == 0) {
				CreatePoppUpText ("Players are All Ready!!");
			} else {
				CreatePoppUpText ("Lahat ng Manlalaro ay Nakahanda na!!");
			}
			for (int i = 0; i < lobbySlots.Length; i++) {
				if (lobbySlots [i] != null) {
					LobbyPlayer lp = lobbySlots [i] as LobbyPlayer;
					lp.RpcDisableKickPlayerBtn ();
					lp.RpcDisableBackBtn ();
				}
			}

			StartCoroutine (ServerCountDown ());
		}

	}


	public IEnumerator ServerCountDown(){
		float remainingTime = countDownTime;
		int floorTime = Mathf.FloorToInt (remainingTime);
		while (remainingTime > 0) {
			yield return null;
			remainingTime -= Time.deltaTime;
			int newFloorTime = Mathf.FloorToInt (remainingTime);
			if (newFloorTime != floorTime) {
				floorTime = newFloorTime;
				for (int i = 0; i < lobbySlots.Length; i++) {
					if (lobbySlots [i] != null) {
						(lobbySlots [i] as LobbyPlayer).RpcUpdateCountDownTime (floorTime);
					}
				}
			}
		}
		for (int i = 0; i < lobbySlots.Length; i++) {
			if (lobbySlots [i] != null) {
				(lobbySlots [i] as LobbyPlayer).RpcUpdateCountDownTime (0);
			}
		}
		ServerChangeScene (playScene);
				
	}

	public override void OnClientDisconnect (NetworkConnection conn)
	{
		base.OnClientDisconnect (conn);
		ChangeToPanel (StartPanel);
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			CreateWarningText ("Server Disconnected!!");
		} else {
			CreateWarningText ("Nawalan ng Koneksyon sa Server!!");
		}
	}
	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
		ChangeToPanel (StartPanel);
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			CreateWarningText ("Server Disconnected!!");
		} else {
			CreateWarningText ("Nawalan ng Koneksyon sa Server!!");
		}
	}
	public override void OnLobbyClientConnect (NetworkConnection conn)
	{

		base.OnLobbyClientConnect (conn);
		conn.RegisterHandler (MsgKicked, KickedMessageHandler);
		if (!NetworkServer.active)
			ChangeToPanel (LobbyPanel);
	}
	//alow to handle add button player(+)
	public void OnPlayersNumberModified(int count){
		_playersNumber += count;
		int LocalPlayerCount = 0;
		foreach (PlayerController pc in ClientScene.localPlayers) {
			LocalPlayerCount += (pc == null || pc.playerControllerId == -1) ? 0 : 1;
		}
	}
	public void ChangeToPanel(RectTransform newPanel){
		if (currentPanel != null) {
			currentPanel.gameObject.SetActive (false);
		}
		if (newPanel != null) {
			currentPanel = newPanel;
		}
		currentPanel.gameObject.SetActive (true);
	}
	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		int playerIndex = 0;

		if (extraMessageReader != null) {
			IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage> ();
			playerIndex = i.value;
		}
		GameObject playerPrefab = spawnPrefabs [playerIndex];

		GameObject player;
		//Debug.Log (conn.connectionId);
		Vector3 startPos = characterSpawnPos[conn.connectionId];
		Quaternion startRot = characterSpawnRot [conn.connectionId];
		if (startPos != null) {
			player = (GameObject)Instantiate (playerPrefab, startPos, startRot);
		} else {
			player = (GameObject)Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		}

		NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
	}
	public override void OnServerSceneChanged (string sceneName)
	{
		base.OnServerSceneChanged (sceneName);
		//StartCoroutine (StartTimer ());
		//set loading screen here
	}

	public override void OnClientSceneChanged (NetworkConnection conn)
	{
		
		base.OnClientSceneChanged (conn);
		ChangeToPanel (TwoPlayerBattlePanel);
	
		int playerIndex = PlayerPrefs.HasKey ("CharacterIndex") ? PlayerPrefs.GetInt ("CharacterIndex") : 0;
		IntegerMessage msg = new IntegerMessage (playerIndex);



		ClientScene.AddPlayer (conn,1 , msg);



	}

	public override void OnLobbyClientSceneChanged (NetworkConnection conn)
	{
		base.OnLobbyClientSceneChanged (conn);
	}

	public override void ServerChangeScene(string newSceneName)
	{
		//if (string.IsNullOrEmpty(newSceneName))
	//	{
		//	if (LogFilter.logError) { Debug.LogError("ServerChangeScene empty scene name"); }
		//	return;
	//	}

	//	if (LogFilter.logDebug) { Debug.Log("ServerChangeScene " + newSceneName); }
	//	NetworkServer.SetAllClientsNotReady();
	//	networkSceneName = newSceneName;

	//	AsyncOperation s_LoadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);
	//	RectTransform newPanel = LobbyPanel;
	//	LobbyPanel.gameObject.SetActive (false);
	//	StartCoroutine (SyncLoadScene (s_LoadingSceneAsync, newPanel));
		//StringMessage msg = new StringMessage(networkSceneName);
		//NetworkServer.SendToAll(MsgType.Scene, msg);
		base.ServerChangeScene (newSceneName);

	}
	IEnumerator SyncLoadScene(AsyncOperation SceneOperation,RectTransform newPanel){
		AsyncOperation operation = SceneOperation;
		LoadPanel.SetActive (true);


		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			LoadBar.value = progress;
			LoadPercentText.text = Mathf.Round (progress * 100f) + "%";


			yield return null;
		}
		if (operation.isDone) {
			LoadPanel.SetActive (false);

			//Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (newPanel);
		}
	}
	class KickMessage:MessageBase{}
	public void KickPlayer(NetworkConnection conn){
		conn.Send (MsgKicked, new KickMessage ());
		Debug.Log ("kicked");
		if (Custom_NetworkLobbyManager._LMSingleton != null)
	
			Custom_NetworkLobbyManager._LMSingleton.OnPlayersNumberModified (-1);
		
	}
	public void KickedMessageHandler(NetworkMessage netmsg){
		//Debug.Log ("kick");
		netmsg.conn.Disconnect ();
		StopHost ();
	}
	//server Management
	public void RemovePlayer(LobbyPlayer _player){

		_player.RemovePlayer ();
	}
}
