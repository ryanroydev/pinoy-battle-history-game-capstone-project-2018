using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour {
	private static CharacterSelectionManager instance;
	public static CharacterSelectionManager Instance{get{ if (instance == null) {
				instance = GameObject.FindObjectOfType<CharacterSelectionManager> ();}return instance;}}
	public GameObject[] characterSelectionPrefabs;

	[SerializeField]
	private GameObject mainCamera;
	public GameObject MainCamera{get{ return mainCamera;}set{ this.mainCamera = value;}}
	[SerializeField]
	private GameObject characterSelectionCamera;


	public GameObject CharacterSelectionCamera{get{ return characterSelectionCamera;}set{ this.characterSelectionCamera = value;}}
	[SerializeField]
	private GameObject chestViewerCamera;


	public GameObject ChestViewerCamera{get{ return chestViewerCamera;}set{ this.chestViewerCamera = value;}}



}
