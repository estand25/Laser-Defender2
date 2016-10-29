using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	private static Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		displayPlayersHealth ();
	}

	public static void displayPlayersHealth(){
		myText.text = "Health: " + PlayerController.health;
	}
}
