using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPanelScript : MonoBehaviour {
	bool isChaging=false;
	private int currentPage;
	[SerializeField]
	private RectTransform ChangeBook1,ChangeBook2;
	[SerializeField]
	private RectTransform[] BookPages;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ChangePageRight(){
		if (!isChaging) {
			int pageIndex = currentPage == BookPages.Length - 1 ? 0 : currentPage + 1;

			Debug.Log (pageIndex);
			Debug.Log (BookPages.Length - 1);
			StartCoroutine (changePage (pageIndex));
		}
	}
	public void ChangePageLeft(){
		if (!isChaging) {
			int pageIndex = currentPage == 0 ? BookPages.Length - 1 : currentPage - 1;

			Debug.Log (pageIndex);
			Debug.Log (BookPages.Length - 1);
			StartCoroutine (changePage (pageIndex));
		}
	}
	private	IEnumerator changePage(int pageIndex){
		isChaging = true;
		BookPages [currentPage].gameObject.SetActive (false);
		currentPage = pageIndex;
		ChangeBook1.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.01f);
		ChangeBook1.gameObject.SetActive (false);
		ChangeBook2.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.01f);
		ChangeBook2.gameObject.SetActive (false);
		BookPages [ pageIndex].gameObject.SetActive (true);
		isChaging = false;

	}
}
