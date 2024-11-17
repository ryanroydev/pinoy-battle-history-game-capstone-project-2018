using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerBattleCameraMovement : MonoBehaviour {
	private Transform cameraNormalPos;
	private Transform cameraBattlePos;
	[SerializeField]private bool OnBattle = true;
	private bool gameStart = false;
	private float cameraFieldOfView;
	public void SetCameraPositions(Transform newCameraNormalPos,Transform newCameraBattlePos){
		cameraNormalPos = newCameraNormalPos;
		cameraBattlePos = newCameraBattlePos;
		gameStart = true;
	}
	void Start(){
		cameraFieldOfView = GetComponent<Camera> ().fieldOfView;
	}
	void FixedUpdate () {
		if (!gameStart)
			return;
		if (GetComponent<Camera> ().fieldOfView != cameraFieldOfView) {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, cameraFieldOfView, Time.deltaTime * 5);
		}
		CheckAndMoveToBattlePos ();

		CheckAndMoveToNormalPos ();
	}
	public IEnumerator showDamage(){
		cameraFieldOfView = 65;
		TwoPlayer_GameManager.Instance.BloodPanel.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.3f);
		TwoPlayer_GameManager.Instance.BloodPanel.gameObject.SetActive (false);
		cameraFieldOfView = 60;
		yield return new WaitForSeconds (0.3f);

	}
	public void SetCameraBattleMode(bool OnBattle){
		this.OnBattle = OnBattle;

	}
	public bool IsCameraInBattlePosition(){

		//Debug.Log (Vector3.Distance (this.transform.position, cameraBattlePos.position));
		if (Vector3.Distance (this.transform.position, cameraBattlePos.position) >= 1.3f) {
			return true;
		} else {
			return false;
		}
	}
	private void CheckAndMoveToBattlePos(){
		if (OnBattle && this.transform.position != cameraBattlePos.position) {
			Vector3 newPos = cameraBattlePos.position;
			this.transform.position = Vector3.Lerp (this.transform.position, newPos, 5f * Time.deltaTime);
		}
		if (OnBattle && this.transform.rotation != cameraBattlePos.rotation) {
			Quaternion newRot = cameraBattlePos.rotation;
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, newRot, 5f * Time.deltaTime);
		}
	}

	private void CheckAndMoveToNormalPos(){
		if (!OnBattle && this.transform.position != cameraNormalPos.position) {
			Vector3 newPos = cameraNormalPos.position;
			this.transform.position = Vector3.Lerp (this.transform.position, newPos, 5f * Time.deltaTime);
		}
		if (!OnBattle && this.transform.rotation != cameraNormalPos.rotation) {
			Quaternion newRot = cameraNormalPos.rotation;
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, newRot, 5f * Time.deltaTime);
		}
	}
}
