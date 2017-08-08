using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {

	public int direction;
	public float speed;
	public float life;
	public GameObject particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		switch(direction) {
		case 1:
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,speed);
			break;
		case 2:
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
			break;
		case 3:
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-speed);
			break;
		case 4:
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
			break;
		}

		life -= Time.deltaTime;
		if(life <= 0)
			Destroy(this.gameObject);
	}
}
