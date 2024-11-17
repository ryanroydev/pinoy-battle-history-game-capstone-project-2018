using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementSlot : MonoBehaviour {
	private int CurrentCrown;
	[SerializeField]
	private Text CountText;
	[SerializeField]
	private Text QuestText,RewardText;
	[SerializeField]
	private Button ClaimBtn;
	[SerializeField]
	private int volumeMonster;
	[SerializeField]
	private MonsterType monster;
	[SerializeField]
	private ItemEquipment rewardItems;
	[SerializeField]
	private int[] rewardGold;

	private int CrownOneClaimed;
	private int CrownTwoClaimed;
	private int CrownThreeClaimed;
	[SerializeField]
	private Image[] crownImage;
	[SerializeField]
	private Sprite[] crownSprite;

	// Use this for initialization
	void Start () {
		ClaimBtn.interactable = false;
		switch (monster.ToString()) {
		case "Tiyanak":
			LoadTiyanakQuestData ();
			TiyanakQuest ();
			UpdateCrown ();
			break;
		case "Manananggal":
			LoadManananggalQuestData ();
			ManananggalQuest ();
			UpdateCrown ();
			break;
		case "ManananggalHalfBody":
			LoadManananggalHalfBodyQuestData ();
			ManananggalHalfBodyQuest ();
			UpdateCrown ();
			break;
		case "Kapre":
			LoadKapreQuestData ();
			KapreQuest ();
			UpdateCrown ();
			break;
		case "Tiktik":
			LoadTiktikQuestData ();
			TikTikQuest ();
			UpdateCrown ();
			break;
		}
	}








	void LoadTiyanakQuestData(){


		string Tiyanakdata = PlayerPrefs.GetString ("TiyanakQuest", "");
		if (Tiyanakdata != "") {
			string[] data = Tiyanakdata.Split ('%');
			CurrentCrown = int.Parse (data [0]);
			CrownOneClaimed = int.Parse (data [1]);
			CrownTwoClaimed = int.Parse (data [2]);
			CrownThreeClaimed = int.Parse (data [3]);

		}

	}
	void SaveTiyanakQuestData(){
		string Tiyanakdata = "";

		Tiyanakdata += CurrentCrown + "%";
		Tiyanakdata += CrownOneClaimed + "%";
		Tiyanakdata += CrownTwoClaimed + "%";
		Tiyanakdata += CrownThreeClaimed + "%";
		PlayerPrefs.SetString ("TiyanakQuest", Tiyanakdata);
	}
	void LoadManananggalQuestData(){


		string Manananggaldata = PlayerPrefs.GetString ("ManananggalQuest", "");
		if (Manananggaldata != "") {
			string[] data = Manananggaldata.Split ('%');
			CurrentCrown = int.Parse (data [0]);
			CrownOneClaimed = int.Parse (data [1]);
			CrownTwoClaimed = int.Parse (data [2]);
			CrownThreeClaimed = int.Parse (data [3]);

		}

	}
	void SaveManananggalQuestData(){
		string Manananggaldata = "";

		Manananggaldata += CurrentCrown + "%";
		Manananggaldata += CrownOneClaimed + "%";
		Manananggaldata += CrownTwoClaimed + "%";
		Manananggaldata += CrownThreeClaimed + "%";
		PlayerPrefs.SetString ("ManananggalQuest", Manananggaldata);
	}
	void LoadManananggalHalfBodyQuestData(){


		string ManananggalHalfBodydata = PlayerPrefs.GetString ("ManananggalHalfBodyQuest", "");
		if (ManananggalHalfBodydata != "") {
			string[] data = ManananggalHalfBodydata.Split ('%');
			CurrentCrown = int.Parse (data [0]);
			CrownOneClaimed = int.Parse (data [1]);
			CrownTwoClaimed = int.Parse (data [2]);
			CrownThreeClaimed = int.Parse (data [3]);

		}

	}
	void SaveManananggalHalfBodyData(){
		string ManananggalHalfBodydata = "";

		ManananggalHalfBodydata += CurrentCrown + "%";
		ManananggalHalfBodydata += CrownOneClaimed + "%";
		ManananggalHalfBodydata += CrownTwoClaimed + "%";
		ManananggalHalfBodydata += CrownThreeClaimed + "%";
		PlayerPrefs.SetString ("ManananggalHalfBodyQuest", ManananggalHalfBodydata);
	}
	void LoadKapreQuestData(){


		string Kapredata = PlayerPrefs.GetString ("KapreQuest", "");
		if (Kapredata != "") {
			string[] data = Kapredata.Split ('%');
			CurrentCrown = int.Parse (data [0]);
			CrownOneClaimed = int.Parse (data [1]);
			CrownTwoClaimed = int.Parse (data [2]);
			CrownThreeClaimed = int.Parse (data [3]);

		}

	}
	void SaveKapreData(){
		string Kapredata = "";

		Kapredata += CurrentCrown + "%";
		Kapredata += CrownOneClaimed + "%";
		Kapredata += CrownTwoClaimed + "%";
		Kapredata += CrownThreeClaimed + "%";
		PlayerPrefs.SetString ("KapreQuest",Kapredata);
	}
	void LoadTiktikQuestData(){


		string Tiktikdata = PlayerPrefs.GetString ("TiktikQuest", "");
		if (Tiktikdata != "") {
			string[] data =Tiktikdata.Split ('%');
			CurrentCrown = int.Parse (data [0]);
			CrownOneClaimed = int.Parse (data [1]);
			CrownTwoClaimed = int.Parse (data [2]);
			CrownThreeClaimed = int.Parse (data [3]);

		}

	}
	void SaveTiktikData(){
		string Tiktikdata = "";

		Tiktikdata += CurrentCrown + "%";
		Tiktikdata += CrownOneClaimed + "%";
		Tiktikdata += CrownTwoClaimed + "%";
		Tiktikdata += CrownThreeClaimed + "%";
		PlayerPrefs.SetString ("TiktikQuest",Tiktikdata);
	}
	public int GetCrowns(){
		if (CrownThreeClaimed == 1) {
			return 3;
		} else if (CrownTwoClaimed == 1) {
			return 2;

		} else if (CrownOneClaimed == 1) {

			return 1;
		}
		return 0;
	}
	public int GetCurrentOneCrown(){
		if (CrownOneClaimed == 1) {
			return 1;
		}
		return 0;
	}
	public int GetCurrentTwoCrown(){
		if (CrownTwoClaimed == 1) {
			return 1;
		}
		return 0;
	}
	public int GetCurrentThreeCrown(){
		if (CrownThreeClaimed == 1) {
			return 1;
		}
		return 0;
	}
	void UpdateCrown(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			QuestText.text = "Kill " + needToKill + " " + monster.ToString ();
		} else {
			QuestText.text = "Pumatay ng " + needToKill + " " + monster.ToString ();
		}
	
	

		if (CrownThreeClaimed == 1) {

			crownImage [0].sprite = crownSprite [1];	
			crownImage [1].sprite = crownSprite [1];	
			crownImage [2].sprite = crownSprite [1];	
		} else if (CrownTwoClaimed == 1) {
			crownImage [0].sprite = crownSprite [1];	
			crownImage [1].sprite = crownSprite [1];	

		} else if (CrownOneClaimed == 1) {

			crownImage [0].sprite = crownSprite [1];
		}
	}

	void TiyanakQuest(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int tiyanakKilled = PlayerPrefs.GetInt ("TiyanakKilled", 0);
		if (tiyanakKilled >= needToKill) {
			if (CurrentCrown != 3) {
				CountText.text = needToKill + "/" + needToKill;
			}
			if (CurrentCrown == 0 && CrownOneClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
				ClaimBtn.interactable = true;
			}

		} else {
			if (CurrentCrown == 3) {
				CountText.text = needToKill + "/" + needToKill;
			} else {
				CountText.text = tiyanakKilled + "/" + needToKill;
			}
			ClaimBtn.interactable = false;
		}
	}
	void ManananggalQuest(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int ManananggalKilled = PlayerPrefs.GetInt ("ManananggalKilled", 0);
		if (ManananggalKilled >= needToKill) {
			if (CurrentCrown != 3) {
				CountText.text = needToKill + "/" + needToKill;
			}
			if (CurrentCrown == 0 && CrownOneClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
				ClaimBtn.interactable = true;
			}

		} else {
			if (CurrentCrown == 3) {
				CountText.text = needToKill + "/" + needToKill;
			} else {
				CountText.text = ManananggalKilled + "/" + needToKill;
			}
			ClaimBtn.interactable = false;
		}


	}
	void ManananggalHalfBodyQuest(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int ManananggalHalfBodyKilled = PlayerPrefs.GetInt ("ManananggalHalfBodyKilled", 0);
		if (ManananggalHalfBodyKilled >= needToKill) {
			if (CurrentCrown != 3) {
				CountText.text = needToKill + "/" + needToKill;
			}
			if (CurrentCrown == 0 && CrownOneClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
				ClaimBtn.interactable = true;
			}

		} else {
			if (CurrentCrown == 3) {
				CountText.text = needToKill + "/" + needToKill;
			} else {
				CountText.text = ManananggalHalfBodyKilled + "/" + needToKill;
			}
			ClaimBtn.interactable = false;
		}


	}
	void KapreQuest(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int KapreKilled = PlayerPrefs.GetInt ("KapreKilled", 0);
		if (KapreKilled >= needToKill) {
			if (CurrentCrown != 3) {
				CountText.text = needToKill + "/" + needToKill;
			}
			if (CurrentCrown == 0 && CrownOneClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
				ClaimBtn.interactable = true;
			}

		} else {
			if (CurrentCrown == 3) {
				CountText.text = needToKill + "/" + needToKill;
			} else {
				CountText.text = KapreKilled + "/" + needToKill;
			}
			ClaimBtn.interactable = false;
		}
	}
	void TikTikQuest(){
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int TiktikKilled = PlayerPrefs.GetInt ("TiktikKilled", 0);
		if (TiktikKilled >= needToKill) {
			if (CurrentCrown != 3) {
				CountText.text = needToKill + "/" + needToKill;
			}
			if (CurrentCrown == 0 && CrownOneClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
				ClaimBtn.interactable = true;
			} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
				ClaimBtn.interactable = true;
			}

		} else {
			if (CurrentCrown == 3) {
				CountText.text = needToKill + "/" + needToKill;
			} else {
				CountText.text = TiktikKilled + "/" + needToKill;
			}
			ClaimBtn.interactable = false;
		}
	}
	// Update is called once per frame
	void Update () {
		UpdateCrown ();
		int needToKill = 0;
		if (CurrentCrown == 2 || CurrentCrown == 3) {
			needToKill = volumeMonster * volumeMonster * volumeMonster;
		} else if (CurrentCrown == 1) {
			needToKill = volumeMonster * volumeMonster;
		} else if (CurrentCrown == 0) {
			needToKill = volumeMonster;
		}
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		switch (monster.ToString()) {
		case "Tiyanak":
			if (languageIndex == 0) {
				
				int tiyanakKilled = PlayerPrefs.GetInt ("TiyanakKilled", 0);
				if (tiyanakKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
					} else {
						CountText.text = tiyanakKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}
					ClaimBtn.interactable = false;
				}
			} else {
				int tiyanakKilled = PlayerPrefs.GetInt ("TiyanakKilled", 0);
				if (tiyanakKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
					} else {
						CountText.text = tiyanakKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}
					ClaimBtn.interactable = false;
				}

			}
			break;
		case "Manananggal":
			if (languageIndex == 0) {

				int ManananggalKilled = PlayerPrefs.GetInt ("ManananggalKilled", 0);
				if (ManananggalKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
					} else {
						CountText.text = ManananggalKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}
					ClaimBtn.interactable = false;
				}
			} else {
				int ManananggalKilled = PlayerPrefs.GetInt ("ManananggalKilled", 0);
				if (ManananggalKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
					} else {
						CountText.text = ManananggalKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}
					ClaimBtn.interactable = false;
				}

			}
			break;
		case "ManananggalHalfBody":
			if (languageIndex == 0) {

				int ManananggalHalfBodyKilled = PlayerPrefs.GetInt ("ManananggalHalfBodyKilled", 0);
				if (ManananggalHalfBodyKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
					} else {
						CountText.text = ManananggalHalfBodyKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}
					ClaimBtn.interactable = false;
				}
			} else {
				int ManananggalHalfBodyKilled = PlayerPrefs.GetInt ("ManananggalHalfBodyKilled", 0);
				if (ManananggalHalfBodyKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
					} else {
						CountText.text = ManananggalHalfBodyKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}
					ClaimBtn.interactable = false;
				}

			}
			break;
		case "Kapre":
			if (languageIndex == 0) {

				int KapreKilled = PlayerPrefs.GetInt ("KapreKilled", 0);
				if (KapreKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
					} else {
						CountText.text = KapreKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}
					ClaimBtn.interactable = false;
				}
			} else {
				int KapreKilled = PlayerPrefs.GetInt ("KapreKilled", 0);
				if (KapreKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
					} else {
						CountText.text = KapreKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}
					ClaimBtn.interactable = false;
				}

			}
			break;
		case "Tiktik":
			if (languageIndex == 0) {

				int TiktikKilled = PlayerPrefs.GetInt ("TiktikKilled", 0);
				if (TiktikKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIMED";
					} else {
						CountText.text = TiktikKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "CLAIM";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Golds";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Golds";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Golds \n" + rewardItems.EnglishName;
					}
					ClaimBtn.interactable = false;
				}
			} else {
				int TiktikKilled = PlayerPrefs.GetInt ("TiktikKilled", 0);
				if (TiktikKilled >= needToKill) {
					if (CurrentCrown == 0 && CrownOneClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
						ClaimBtn.interactable = true;
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;

					} else if (CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}

					CountText.text = needToKill + "/" + needToKill;
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
						ClaimBtn.interactable = false;
					} else {
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
				} else {
					if (CurrentCrown == 3) {
						CountText.text = needToKill + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "NAKUHA NA";
					} else {
						CountText.text = TiktikKilled + "/" + needToKill;
						ClaimBtn.GetComponentInChildren<Text> ().text = "KUNIN";
					}
					if (CurrentCrown == 0) {
						RewardText.text = rewardGold [0] + " Ginto";
					} else if (CurrentCrown == 1) {
						RewardText.text = rewardGold [1] + " Ginto";
					} else if (CurrentCrown == 2 || CurrentCrown == 3) {
						RewardText.text = rewardGold [2] + " Ginto \n" + rewardItems.tagalogName;
					}
					ClaimBtn.interactable = false;
				}

			}
			break;
		}
	}
	public void Claimed(){
		AchievementsManager.Instance.PlaySoundAchievement ();
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
	
		if (CurrentCrown == 0 && CrownOneClaimed == 0) {
			StatsManager.Instance.addGold (rewardGold [0]);
			if (languageIndex == 0) {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("You Got :" + rewardGold [0] + " Golds");
			} else {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("Nakakuha ka ng :" + rewardGold [0] + " Ginto");
			}
			CrownOneClaimed = 1;
			CurrentCrown = 1;
			UpdateCrown ();
		} else if (CurrentCrown == 1 && CrownTwoClaimed == 0) {
			StatsManager.Instance.addGold (rewardGold [1]);
			if (languageIndex == 0) {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("You Got :" + rewardGold [1] + " Golds");
			} else {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("Nakakuha ka ng :" + rewardGold [1] + " Ginto");
			}
			CrownTwoClaimed = 1;
			CurrentCrown = 2;
			UpdateCrown ();
		} else if (CurrentCrown == 2 && CrownThreeClaimed == 0) {
			StatsManager.Instance.addGold (rewardGold [2]);
			Inventory.Instance.Add (rewardItems);
			if (languageIndex == 0) {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("You Got :" + rewardGold [2] + " Golds" + " And " + rewardItems.EnglishName);
			} else {
				Custom_NetworkLobbyManager._LMSingleton.CreatePoppUpText ("Nakakuha ka ng :" + rewardGold [2] + " Ginto" + " At " + rewardItems.tagalogName);
			}
			Inventory.Instance.MyInventoryUi.UpdateInventoryUI ();
			EquipmentManager.Instance.SaveInventoryItem ();
			CrownThreeClaimed = 1;
			CurrentCrown = 3;
			UpdateCrown ();
		}
		
		switch (monster.ToString()) {
		case "Tiyanak":
			SaveTiyanakQuestData ();
			break;
		case "Manananggal":
			SaveManananggalQuestData ();
			break;
		case "ManananggalHalfBody":
			SaveManananggalHalfBodyData ();
			break;
		case "Kapre":
			SaveKapreData ();
			break;
		case "Tiktik":
			SaveTiktikData ();
			break;
		}
		AchievementsManager.Instance.SetTrophy ();
	}

}
public enum MonsterType{Tiyanak,Manananggal,ManananggalHalfBody,Kapre,Tiktik,None}
