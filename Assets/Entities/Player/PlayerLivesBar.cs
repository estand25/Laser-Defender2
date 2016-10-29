using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLivesBar : MonoBehaviour {
	private static Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		displayPlayersLives ();
	}
	
	public static void displayPlayersLives(){
		myText.text = "Lives: " + PlayerController.lives;
	}

	public static void displayPlayersLives(string text){
		myText.text = text + PlayerController.lives;
	}
}