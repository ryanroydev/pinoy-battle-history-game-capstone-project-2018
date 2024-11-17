using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_RewardSystem : MonoBehaviour {
	private int expirience = 0;

	private int coin = 0;

	private int score = 0;
	public void AddExpi(int expiAdded){
		expirience += expiAdded;
	}
	public void AddCoin(int coinAdded){
		coin += coinAdded;
	}
	public void AddScore(int scoreAdded){
		score += scoreAdded;
	}
	public int GetExpirience(){
		return expirience;
	}
	public int GetCoin(){
		return coin;
	}
	public int GetScore(){
		return score;
	}

}
