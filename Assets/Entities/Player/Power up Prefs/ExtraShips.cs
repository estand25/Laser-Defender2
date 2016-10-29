using UnityEngine;
using System.Collections;

public class ExtraShips : MonoBehaviour {
	private int extraShip = 1;
	private int maxLives = 5;
	
	public int GetExtraShip(){
		return extraShip;
	}
	
	public void extraShipUsed(){
		Destroy(gameObject);
	}
	
	public int getMaxLive(){
		return maxLives;
	}
}