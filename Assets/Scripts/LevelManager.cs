using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Application.Quit ();
	}
	
	//Load the next scence based on the build numbers
	public void LoadNextLevel(){
		// Load the next level based on the current load level plus 1
		Application.LoadLevel(Application.loadedLevel+1);

		Debug.Log ("Current Levels: " + Application.loadedLevel);
		Debug.Log ("Current Levels Name: " + Application.loadedLevelName);
	}
}
