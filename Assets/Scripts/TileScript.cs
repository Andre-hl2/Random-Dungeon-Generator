using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public Sprite normal_sprite;
	public Sprite border_sprite;
	public LayerMask mask;
	public bool border;
	public int borderSide;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MakeBorder() {
		this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = border_sprite;
	}

	public void unMakeBorder() {
		if(border) {
			Vector3 pos = this.transform.position;


			switch(borderSide) {
			case 1:
				RaycastHit2D[] hits1 = Physics2D.RaycastAll(pos, new Vector2(0,1), 1.2f, mask);
				foreach(RaycastHit2D hit in hits1) {
					if(hit.collider != null) {
						if(hit.collider.gameObject != this.gameObject) {
							TileScript ti = hit.collider.gameObject.GetComponent<TileScript>();
							if(ti.border && ti.borderSide == 3) {
								ti.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								ti.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								ti.border = false;
								this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								this.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								this.border = false;
							}
						}
					}
				}
				break;
			case 2:
				RaycastHit2D[] hits2 = Physics2D.RaycastAll(pos, new Vector2(1,0), 1.2f, mask);
				foreach(RaycastHit2D hit in hits2) {
					if(hit.collider != null) {
						if(hit.collider.gameObject != this.gameObject) {
							TileScript ti = hit.collider.gameObject.GetComponent<TileScript>();
							if(ti.border && ti.borderSide == 4) {
								ti.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								ti.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								ti.border = false;
								this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								this.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								this.border = false;
							}
						}
					}
				}
				break;
			case 3:
				RaycastHit2D[] hits3 = Physics2D.RaycastAll(pos, new Vector2(0,-1), 1.2f, mask);
				foreach(RaycastHit2D hit in hits3) {
					if(hit.collider != null) {
						if(hit.collider.gameObject != this.gameObject) {
							TileScript ti = hit.collider.gameObject.GetComponent<TileScript>();
							if(ti.border && ti.borderSide == 1) {
								ti.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								ti.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								ti.border = false;
								this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								this.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								this.border = false;
							}
						}
					}
				}
				break;
			case 4:
				RaycastHit2D[] hits4 = Physics2D.RaycastAll(pos, new Vector2(-1,0), 1.2f, mask);
				foreach(RaycastHit2D hit in hits4) {
					if(hit.collider != null) {
						if(hit.collider.gameObject != this.gameObject) {
							TileScript ti = hit.collider.gameObject.GetComponent<TileScript>();
							if(ti.border && ti.borderSide == 2) {
								ti.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								ti.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								ti.border = false;
								this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								this.gameObject.GetComponent<SpriteRenderer>().sprite = normal_sprite;
								this.border = false;
							}
						}
					}
				}
				break;
				break;
			}

		}
	}
}