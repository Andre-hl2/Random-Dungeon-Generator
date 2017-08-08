using UnityEngine;
using UnityEngine.UI;

public class Colisao_script : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Generator.Data.generating) {
			int collisionCount = 0;
			GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
			foreach(GameObject obj in objs) {
				Vector3 pos = obj.transform.position;
				RootCreator root = obj.gameObject.GetComponent<RootCreator>();
				Vector2 pointA = new Vector2(pos.x - 0.5f - (root.sizeX * 0.8f), pos.y - 0.5f - (root.sizeY * 0.8f));
				Vector2 pointB = new Vector2(pos.x - 0.5f + (root.sizeX * 0.8f), pos.y - 0.5f + (root.sizeY * 0.8f));
				
				Collider2D[] res = Physics2D.OverlapAreaAll(pointA, pointB);
				if(res.Length != 0) {
					foreach(Collider2D coll in res) {
						if(coll.gameObject != obj.gameObject) {
							Debug.DrawLine(obj.transform.position, coll.gameObject.transform.position, Color.red);
							collisionCount++;
						}
					}
				}
			}
			
			if(collisionCount != 0){
				text.color = Color.red;
			} else {
				text.color = Color.green;
			}
		}
	}
}
