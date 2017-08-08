using UnityEngine;
using System.Collections;

public class snake : MonoBehaviour {

	public float speed;
	public int vida;
	public bool following;

	// Use this for initialization
	void Start () {
		following = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!following) {
			if(Vector2.Distance(this.transform.position, Generator.Data.rootChar.transform.position) < 4)
				following = true;
		} else {

			Vector3 moveTo = Generator.Data.rootChar.transform.position - this.transform.position;

			this.transform.position += moveTo.normalized * (speed * Time.deltaTime);

		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "FireBall") {

			FireBallScript fire = coll.gameObject.GetComponent<FireBallScript>();
			Generator.Spawn(fire.particle, fire.gameObject.transform.position, Quaternion.identity);
			Destroy(coll.gameObject);

			this.vida -= 1;
			if(vida <= 0) {
				Generator.Data.enemiesKilled++;
				Destroy(this.gameObject);
			}
		}
	}
}
