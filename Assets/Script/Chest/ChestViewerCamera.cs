using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestViewerCamera : MonoBehaviour {
	private Transform targetPos;
	[SerializeField]private Transform RealCameraPos; 
	// Use this for initialization
	void OnEnable () {
		
		targetPos = RealCameraPos;
	}
	
	// Update is called once per frame
	void Update () {
		CheckCameraDestination ();
	}
	void CheckCameraDestination(){
		if (this.transform.position != targetPos.position) {
			this.transform.position = Vector3.Lerp (this.transform.position, targetPos.position, 3f * Time.deltaTime);
		}
		if (this.transform.rotation != targetPos.rotation) {
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetPos.rotation, 3f * Time.deltaTime);
		}
	}
	public void SetCameraNewTargetPos(Transform newPos){
		targetPos = newPos;
	}

	public void SetRealCameraPos(){
		targetPos = RealCameraPos;
	}
}
