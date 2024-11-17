[System.Serializable]
public class Question  {
	public string Fact;
	public int AnswerIndex;
	public string[] Choices;  
	public QuestionWordLength questonLegth;
}
public enum QuestionWordLength{qshort,qlong,qverylong}