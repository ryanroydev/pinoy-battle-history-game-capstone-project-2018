using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerCameraMovement : MonoBehaviour {
	[SerializeField]
	private Transform cameraNormalPos, cameraBattlePos;
	[SerializeField]
	private bool OnBattle = false;
	private bool isIntroCamera = true;
	private float cameraFieldOfView;

	void Start () {
		cameraFieldOfView = GetComponent<Camera> ().fieldOfView;

	}

	public IEnumerator showDamage(){
		cameraFieldOfView = 65;
		SinglePlayer_GameManager.Instance.BloodPanel.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.3f);
		SinglePlayer_GameManager.Instance.BloodPanel.gameObject.SetActive (false);
		cameraFieldOfView = 60;
		yield return new WaitForSeconds (0.3f);
	
	}
	public void SetCameraBattleMode(bool OnBattle){
		this.OnBattle = OnBattle;
	
	}

	public void SetIntroCamera(bool inIntro){
		isIntroCamera = inIntro;//set false kapag tapos na intro
	}



	public bool IsCameraInBattlePosition(){

		//Debug.Log (Vector3.Distance (this.transform.position, cameraBattlePos.position));
		if (Vector3.Distance (this.transform.position, cameraBattlePos.position) >= 1.3) {
			return true;
		} else {
			return false;
		}
	}
	void FixedUpdate () {
		if (GetComponent<Camera> ().fieldOfView != cameraFieldOfView) {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, cameraFieldOfView, Time.deltaTime * 5);
		}
		if (!isIntroCamera) {
			CheckAndMoveToBattlePos ();

			CheckAndMoveToNormalPos ();
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
