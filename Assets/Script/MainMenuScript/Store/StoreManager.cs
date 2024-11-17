using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {
	private static StoreManager instance;
	public static StoreManager Instance{get{if (instance == null) {instance = GameObject.FindObjectOfType<StoreManager> ();} return instance;}}

	public ItemInfoPanel ItemInfoPanelScript;
}
