using UnityEngine;
using System.Collections;

public class ExtraHealths : MonoBehaviour {
	private int extraHealth = 50;
	
	public int GetExtraHealth(){
		return extraHealth;
	}
	
	public void extraHealthUsed(){
		Destroy (gameObject);
	}
}
