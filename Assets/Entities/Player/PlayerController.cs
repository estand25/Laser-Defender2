using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public float padding =  1f;
	public float projectileSpeed;
	public float firingRate = 0.2f;

	public AudioClip playerFiring;

	public static float health = 300f;
	public static int lives = 3;

	float xmin;
	float xmax;

	public GameObject projectile;

	private int numFire = 0;

	void Start(){
		ReSetHealth ();
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + padding;
		xmax = camera.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x - padding;
	}

	void Fire(){
		for (int i = 0; i <= numFire; i++) {
			GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			float fireAxle;

			if(i == 0){
				fireAxle = i;
			} else {
				fireAxle = transform.position.x+ i;
			}

			beam.rigidbody2D.velocity = new Vector3(fireAxle,projectileSpeed,0);
			
			// After I fire play the firing clip
			AudioSource.PlayClipAtPoint (playerFiring, beam.transform.position);
		}
	}

	// Update is called once per frame
	void Update () {		
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// Restrict the play to the game space
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		ExtraShips exShip = gameObject.GetComponent<ExtraShips> ();
		// Player's Lives
		//UpdatePlayersLives (exShip);
		
		PlayerLivesBar.displayPlayersLives ();
	}

	void OnTriggerEnter2D(Collider2D col){
		ExtraShips eShip = col.gameObject.GetComponent<ExtraShips> ();

		if (col.gameObject.name.Contains ("Ship")) {			
			if (eShip) {
				if(eShip.getMaxLive() > lives){
					lives += eShip.GetExtraShip ();
					eShip.extraShipUsed ();
				}
			}
		} else if (col.gameObject.name.Contains ("Health")) {
			ExtraHealths extraHealth = col.gameObject.GetComponent<ExtraHealths>();

			if(extraHealth){
				health += extraHealth.GetExtraHealth();
				extraHealth.extraHealthUsed();
			}
		} else if (col.gameObject.name.Contains ("Speed")) {
			ExtraSpeeds extraSpeed = col.gameObject.GetComponent<ExtraSpeeds>();

			if(extraSpeed){
				projectileSpeed += extraSpeed.GetExtraSpeed();
				extraSpeed.extraSpeedUsed();
			}
		} else if (col.gameObject.name.Contains ("ExtraWeapon")) {
			ExtraWeapons extraWeapon = col.gameObject.GetComponent<ExtraWeapons>();

			if(extraWeapon){
				if(extraWeapon.getMaxWeapons() >= numFire){
					numFire += extraWeapon.GetExtraWeapon();
					extraWeapon.extraWeaponUsed();
				}
			}
		} else if (col.gameObject.name.Contains ("Laser")) {
			Projectile missile = col.gameObject.GetComponent<Projectile> ();
			if (missile) {
				// Remove the damage from the player health
				health -= missile.GetDamage ();

				// Then destory the missile
				missile.Hit ();

				// If Health goes below 0
				if (health <= 0) {
					Debug.Log("Current Lives Count: "+lives);
					ReSetHealth ();
					lives -= 1;

					if (lives == 0) {
						Debug.Log("Live Count: "+lives);
						ReSetLives ();
						Die ();
					}
				}
			}			
		}
		
		Update();

		// Player's Health
		PlayerHealth.displayPlayersHealth ();
		// Player's Lives
		UpdatePlayersLives (eShip);

		Update();
	}

	void UpdatePlayersLives(ExtraShips eShip){
		// Player's Lives
		if (eShip.getMaxLive() <= lives) {
			PlayerLivesBar.displayPlayersLives("Lives: Max - ");
		} else {
			PlayerLivesBar.displayPlayersLives ();
		}
	}

	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
		Destroy(gameObject);
	}

	public static void ReSetHealth(){
		health = 300f;
	}

	public static void ReSetLives(){
		lives = 3;
	}
}