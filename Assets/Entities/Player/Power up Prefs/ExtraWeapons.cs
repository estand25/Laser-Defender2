using UnityEngine;
using System.Collections;

public class ExtraWeapons : MonoBehaviour {
	private int extraWeapons = 1;
	private int maxWeapons = 5;

	public int GetExtraWeapon(){
		return extraWeapons;
	}

	public void extraWeaponUsed(){
		Destroy (gameObject);
	}
	
	public int getMaxWeapons(){
		return maxWeapons;
	}
}
