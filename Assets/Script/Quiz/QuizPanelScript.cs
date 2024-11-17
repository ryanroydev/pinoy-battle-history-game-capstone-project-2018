using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class QuizPanelScript : MonoBehaviour {
	//quiz
	[SerializeField]
	private Text factText,aButtonText,bButtonText,cButtonText,dButtonText;
	[SerializeField]
	private Question[] englishQuestions, tagalogQuestions;
	private List<Question> unAnsweredQuestion;
	public Question currentQuestion;

	//quizend
	// Use this for initialization
	void Start () {
		

	}
	void OnEnable(){
		//store questions
		if (unAnsweredQuestion == null || unAnsweredQuestion.Count == 0) {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			switch (languageIndex) {
			case 0:
				unAnsweredQuestion = englishQuestions.ToList<Question> ();
				break;
			case 1:
				unAnsweredQuestion = tagalogQuestions.ToList<Question> ();

				break;
			}


		}

		GetRandomQuestion ();
	}


	//quiz function
	private void GetRandomQuestion(){
		if (unAnsweredQuestion.Count != 0) {
			int randomIndex = Random.Range (0, unAnsweredQuestion.Count);
			currentQuestion = unAnsweredQuestion [randomIndex];
			storeQuizText ();
			unAnsweredQuestion.RemoveAt (randomIndex);
		} else {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			switch (languageIndex) {
			case 0:
				currentQuestion =  englishQuestions [0];
				break;
			case 1:
				currentQuestion =  tagalogQuestions [0];

				break;
			}

			storeQuizText ();
		}
	}
	private void storeQuizText(){
		factText.text = currentQuestion.Fact;
		//Debug.Log (currentQuestion.Choices.Count);
		aButtonText.text = currentQuestion.Choices [0];
		bButtonText.text = currentQuestion.Choices [1];
		cButtonText.text = currentQuestion.Choices [2];
		dButtonText.text = currentQuestion.Choices [3];

	}
	public void  UserSelectChoicesIndex(int index){
		SinglePlayer_GameManager.Instance.QuestionAnswered = true;
		StartCoroutine (DelayUserSelectChoicesIndex (index));

	}
	IEnumerator DelayUserSelectChoicesIndex(int index){
		changeCorrectWrongText (index);
		float delayTime = 1f;
		bool isCorrect = (currentQuestion.AnswerIndex == index);
		ShowInfoText (isCorrect, currentQuestion.AnswerIndex);
		yield return new WaitForSeconds (delayTime);
	
		SinglePlayer_GameManager.Instance.ShowQuizPanel (false);
		if (isCorrect) {
			SinglePlayer_GameManager.Instance.Player.GetComponent<SinglePlayer_PlayerAttack> ().Attack ();
		} else {
			SinglePlayer_GameManager.Instance.EnemiesAttack ();
		}
	}
	private void ShowInfoText(bool isCorrect,int index){
		//bool isTagalog = SinglePlayer_GameManager.Instance.isTagalog;
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (isCorrect) {
			if (languageIndex == 0) {
				SinglePlayer_GameManager.Instance.CreatePoppUpInfoText ("You're Correct!! \n Player Turn To Attack ");
			} else {
				SinglePlayer_GameManager.Instance.CreatePoppUpInfoText ("Tama ang Sagot mo!! \n Ang Manlalaro ang Susunod na Aatake");
			}
		}else {
			if (languageIndex == 0) {
				SinglePlayer_GameManager.Instance.CreatePoppUpWarningText("You're Wrong!! \n Enemy Turn To Attack  \n The Correct Answer is '" + currentQuestion.Choices [index] + "'");
			} else {
				SinglePlayer_GameManager.Instance.CreatePoppUpWarningText ("Mali ang Sagot mo!! \n Ang Kalaban ang Susunod na Aatake \n Ang Tamang Sagot ay '" + currentQuestion.Choices [index] + "'");
					
			}

		}
	}
	private void changeCorrectWrongText(int index){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		bool isTagalog = false;
		switch (languageIndex) {
		case 0:
			isTagalog = false;
			break;
		case 1:
			isTagalog = true;

			break;
		}


			switch (index) {
		case 0:
			aButtonText.text = (currentQuestion.AnswerIndex == index) ? ((!isTagalog) ? "Correct!" : "Tama!") : ((!isTagalog) ? "Wrong!" : "Mali!");
				break;
			case 1:
			bButtonText.text = (currentQuestion.AnswerIndex == index) ? ((!isTagalog) ? "Correct!" : "Tama!") : ((!isTagalog) ? "Wrong!" : "Mali!");
				break;
		case 2:
			cButtonText.text = (currentQuestion.AnswerIndex == index) ? ((!isTagalog) ? "Correct!" : "Tama!") : ((!isTagalog) ? "Wrong!" : "Mali!");
				break;
			case 3:
			dButtonText.text = (currentQuestion.AnswerIndex == index) ? ((!isTagalog) ? "Correct!" : "Tama!") : ((!isTagalog) ? "Wrong!" : "Mali!");
				break;
			}
	}

}
