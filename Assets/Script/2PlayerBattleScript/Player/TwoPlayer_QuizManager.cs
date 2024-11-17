using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class TwoPlayer_QuizManager : MonoBehaviour {
	
public bool IsQuestionAnswered=false;
	//quiz
	[SerializeField]
	private Text factText,aButtonText,bButtonText,cButtonText,dButtonText;
	[SerializeField]
	private Question[] englishSpanishQuestions, tagalogSpanishQuestions,englishJapaneseQuestions,tagalogJapaneseQuestions,englishAmericanQuestions,tagalogAmericanQuestions;
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
			int CategegoryIndex = Custom_NetworkLobbyManager._LMSingleton.CurrentCategoryIndex;
			switch (CategegoryIndex) {
			case 0:
				unAnsweredQuestion = (languageIndex == 0 ? englishSpanishQuestions.ToList<Question>() : tagalogSpanishQuestions.ToList<Question>());
				break;
			case 1:
				unAnsweredQuestion = (languageIndex == 0 ? englishJapaneseQuestions.ToList<Question>() : tagalogJapaneseQuestions.ToList<Question>());

				break;
			case 2:
				unAnsweredQuestion = (languageIndex == 0 ? englishAmericanQuestions.ToList<Question>() : tagalogAmericanQuestions.ToList<Question>());
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
			int CategegoryIndex = Custom_NetworkLobbyManager._LMSingleton.CurrentCategoryIndex;
			switch (CategegoryIndex) {
			case 0:
				currentQuestion = (languageIndex == 0 ? englishSpanishQuestions[0] : tagalogSpanishQuestions[0]);
				break;
			case 1:
				currentQuestion = (languageIndex == 0 ? englishJapaneseQuestions[0] : tagalogJapaneseQuestions[0]);

				break;
			case 2:
				currentQuestion = (languageIndex == 0 ? englishAmericanQuestions[0] : tagalogAmericanQuestions[0]);
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
		TwoPlayer_GameManager.Instance.QuestionAnswered = true;
		StartCoroutine (DelayUserSelectChoicesIndex (index));

	}
	IEnumerator DelayUserSelectChoicesIndex(int index){
		changeCorrectWrongText (index);
		float delayTime = 0.5f;
		bool isCorrect = (currentQuestion.AnswerIndex == index);
		ShowInfoText (isCorrect, true);
		yield return new WaitForSeconds (delayTime);
		ShowInfoText (isCorrect, false);
		TwoPlayer_GameManager.Instance.ShowQuizPanel (false);
		if (isCorrect) {
			//TwoPlayer_GameManager.Instance.CreatePoppUpInfoText("Your Correct!! \n You are Turn To Attack");
			Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerAttack> ().Attack (false,false);
		} else {
			//TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("Your Wrong!! \n The Correct Answer is '" + currentQuestion.Choices [currentQuestion.AnswerIndex] + "'");
			Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerAttack> ().Attack (true,false);
		}
	}
	private void ShowInfoText(bool isCorrect,bool isShow){

		if (isCorrect) {
			//SinglePlayer_GameManager.Instance.DisplayTextInfo ("Player Turn To Attack", isShow);
		} else {
			//SinglePlayer_GameManager.Instance.DisplayTextInfo ("Enemy Turn To Attack", isShow);
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
