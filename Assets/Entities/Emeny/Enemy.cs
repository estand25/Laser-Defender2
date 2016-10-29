using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health = 150f;

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float shotsPerSecond = 0.5f;
	public float spoilersPerSecond = 1f;

	public AudioClip enemyFiring;
	public AudioClip enemyTakingFiring;

	public int scoreValue = 150;
	private ScoreKeeper scoreKeeper;

	public static int playerLives;

	public GameObject smokeDamage;
	public GameObject[] spoilers;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			PuffSmoke();
			if(health <= 0){
				Die();
			}
		}
	}

	void Die(){
		// After enemy is hit play clip
		AudioSource.PlayClipAtPoint (enemyTakingFiring, transform.position, 0.35f);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
		
		// float probabiltyOfSpoilers = Time.deltaTime * spoilersPerSecond;
		// Debug.Log ("Deltatime - " + Time.deltaTime);
		if (Random.Range(0, 100) < spoilersPerSecond) {
			Spoiler();
		}
	}

	void Spoiler(){
		GameObject powerUpPref = spoilers[Random.Range (0, spoilers.Length)];
		GameObject powerUp = Instantiate (powerUpPref, transform.position, Quaternion.identity) as GameObject;
		powerUp.rigidbody2D.velocity = new Vector3(0,-1,0);
	}

	void Fire(){
		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0,-projectileSpeed);
		
		// After enemy fires play clip
		AudioSource.PlayClipAtPoint (enemyFiring, missile.transform.position);
	}

	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire ();
		}
	}

	// Generates the puff for the brick
	void PuffSmoke(){
		// Create new instance of the snoke at the brick's fromer location
		GameObject smokePuff = Instantiate(smokeDamage,gameObject.transform.position,Quaternion.identity) as GameObject;
		
		//Destroy The Extinguished Smoke Instance
		//---------------------------------------
		//Create a variable 'smokeLife' to hold the length of time the smoke will be visible. 
		float smokeLife = smokePuff.GetComponent<ParticleSystem>().duration + smokePuff.GetComponent<ParticleSystem>().startLifetime;
		//Destroy the 'smokePuff' instance after it has finished its animation. 
		Destroy(smokePuff, smokeLife);  
	}
}
