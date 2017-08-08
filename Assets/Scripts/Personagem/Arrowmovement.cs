using UnityEngine;
using System.Collections;

public class Arrowmovement : MonoBehaviour {

	public float speedX;
	public float speedY;

	public bool canShoot;
	public float timeToShot;
	public float coolDownTime;

	public GameObject shoot;

	// Use this for initialization
	void Start () {
		canShoot = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = Vector2.zero;

		if(Input.GetKey(KeyCode.W))
			velocity.y += speedY * Time.deltaTime;
		if(Input.GetKey(KeyCode.D))
			velocity.x += speedX * Time.deltaTime;
		if(Input.GetKey(KeyCode.S))
			velocity.y -= speedY * Time.deltaTime;
		if(Input.GetKey(KeyCode.A))
			velocity.x -= speedX * Time.deltaTime;

		this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;

		if(canShoot) {
			if(Input.GetKey(KeyCode.UpArrow)) {
				GameObject upShoot = Generator.Spawn(shoot, this.gameObject.transform.position, Quaternion.identity);
				upShoot.gameObject.GetComponent<FireBallScript>().direction = 1;
				canShoot = false;
				timeToShot = coolDownTime;
			}
			if(Input.GetKey(KeyCode.RightArrow)) {
				GameObject upShoot = Generator.Spawn(shoot, this.gameObject.transform.position, Quaternion.identity);
				upShoot.gameObject.GetComponent<FireBallScript>().direction = 2;
				canShoot = false;
				timeToShot = coolDownTime;
			}
			if(Input.GetKey(KeyCode.DownArrow)) {
				GameObject upShoot = Generator.Spawn(shoot, this.gameObject.transform.position, Quaternion.identity);
				upShoot.gameObject.GetComponent<FireBallScript>().direction = 3;
				canShoot = false;
				timeToShot = coolDownTime;
			}
			if(Input.GetKey(KeyCode.LeftArrow)) {
				GameObject upShoot = Generator.Spawn(shoot, this.gameObject.transform.position, Quaternion.identity);
				upShoot.gameObject.GetComponent<FireBallScript>().direction = 4;
				canShoot = false;
				timeToShot = coolDownTime;
			}

		} else {
			timeToShot -= Time.deltaTime;
			if(timeToShot <= 0)
				canShoot = true;
		}
	}
}
