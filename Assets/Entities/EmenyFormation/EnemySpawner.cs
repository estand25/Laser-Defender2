using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPreb;
	public float width = 15f;
	public float height = 5f;
	public bool movingRight = true;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	float xmin;
	float xmax;

	public int spawnLives = 5;

	// Use this for initialization
	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
		xmax = camera.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;

		SpawnUntilFull ();
	}

	void SpawnEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPreb, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull(){		
		Transform freePosition = NextFreePosition ();

		if (freePosition) {
			GameObject enemy = Instantiate (enemyPreb, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	// Update is called once per frame
	void Update () {		
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}

		if (AllMembersDead ()) {		
			spawnLives -= 1;

			if (spawnLives == 0) {
				NextLevel();
			}

			SpawnUntilFull ();
		}
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition(){
		foreach (Transform childNextFreePosition in transform) {
			if(childNextFreePosition.childCount == 0){
				return childNextFreePosition;
			}
		}
		return null;
	}

	void NextLevel(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadNextLevel ();
	}

	public Transform NextSpoilerFreePosition(){
		int selectSpoiler = Random.Range (0, this.transform.childCount);
		int i = 0;
		
		foreach (Transform child in transform) {
			if(i == selectSpoiler){
				return child;
			}
			i++;
		}
		return null;
	}
}
