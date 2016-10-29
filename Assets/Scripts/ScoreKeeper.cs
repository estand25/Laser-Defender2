using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text myText;

	void Start(){
		myText = GetComponent<Text> ();
		Reset ();
		myText.text = "Score: " + score.ToString ();
	}

	public void Score(int points){
		score += points;
		myText.text = "Score: " + score.ToString ();
	}

	public static void Reset(){
		score = 0;
	}

	public int GetScore(){
		return score;
	}
}
