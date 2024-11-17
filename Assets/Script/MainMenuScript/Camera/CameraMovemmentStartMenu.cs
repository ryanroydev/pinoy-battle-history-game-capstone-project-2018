using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CameraMovemmentStartMenu : MonoBehaviour {
	private Transform myTransform;
	private Vector3 targetTransform;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
		Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.StartPanel);
	}
	public void SetTarget(Vector3 Target){
		targetTransform = Target;
				
			
	}
	// Update is called once per frame
	void Update () {
		if (targetTransform != Vector3.zero)
			myTransform.position = Vector3.MoveTowards (myTransform.position, targetTransform, 2 * Time.deltaTime);
		
	}
}
