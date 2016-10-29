using UnityEngine;
using System.Collections;

public class ExtraSpeeds : MonoBehaviour {
	private int extraSpeed = 10;
	
	public int GetExtraSpeed(){
		return extraSpeed;
	}
	
	public void extraSpeedUsed(){
		Destroy(gameObject);
	}
}
