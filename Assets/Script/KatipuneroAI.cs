using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KatipuneroAI : MonoBehaviour {
	
	private int currentPosIndex;
	[SerializeField]private Transform[] PathPosistions;
	// Use this for initialization
	void Start () {
		

			
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (this.transform.position, PathPosistions [currentPosIndex].position, 3f*Time.deltaTime);

		Vector3 dir = PathPosistions [currentPosIndex].position - this.transform.position;
		Quaternion lookRot = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Euler (0, lookRot.eulerAngles.y, 0);
		if (Vector3.Distance (transform.position, PathPosistions [currentPosIndex].position) <= 0) {
			if (currentPosIndex == PathPosistions.Length - 1) {
				currentPosIndex = 0;
			} else if (currentPosIndex == 0) {
				
				currentPosIndex++;
			}
		}
	}

}
