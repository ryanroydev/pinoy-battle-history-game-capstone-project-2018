using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		AchievementsManager.Instance.SetTrophy ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
