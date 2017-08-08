using UnityEngine;
using System.Collections;

public class RootCreator : MonoBehaviour {

	public GameObject tile;
	public GameObject room;
	public int sizeX;
	public int sizeY;
	public bool selected;
	public bool spawnPoint;

	// Use this for initialization
	void Start () {
		this.sizeX = Random.Range(1,8);
		this.sizeY = Random.Range(1,8);
		this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(sizeX*2, sizeY*2);
		this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, -0.5f);
		selected = false;
		spawnPoint = false;
		CreateRoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateRoom() {
		Vector3 pos = this.transform.position;
		for(int i=-this.sizeX;i<this.sizeX;i++) {
			for(int j=-this.sizeY;j<this.sizeY;j++) {
				GameObject clone = Generator.Spawn(tile, new Vector3(pos.x+i, pos.y+j, 0), Quaternion.identity);
				TileScript tile_s = clone.gameObject.GetComponent<TileScript>();
				tile_s.border = false;
				if(i == -this.sizeX) {
					tile_s.border = true;
					tile_s.borderSide = 4;
				}
				if(i == (this.sizeX-1)) {
					tile_s.border = true;
					tile_s.borderSide = 2;
				}
				if(j == -this.sizeY) {
					tile_s.border = true;
					tile_s.borderSide = 3;
				}
				if(j == (this.sizeY-1)) {
					tile_s.border = true;
					tile_s.borderSide = 1;
				}

				// Remove do switch de acao, caso seja um tile de borda
				if(i == -this.sizeX && j == -this.sizeY)
					tile_s.borderSide = 5;
				if(i == -this.sizeX && j == (this.sizeY-1))
					tile_s.borderSide = 6;
				if(i == (this.sizeX-1) && j == -this.sizeY)
					tile_s.borderSide = 7;
				if(i == (this.sizeX-1) && j == (this.sizeY-1))
					tile_s.borderSide = 8;

				clone.transform.parent = this.gameObject.transform;
			}
		}
		GameObject clone_room = Generator.Spawn(room, new Vector3(pos.x - 0.5f, pos.y -0.5f, 0), Quaternion.identity);
		clone_room.transform.localScale = new Vector3(this.sizeX * 2, this.sizeY * 2, 1);
		clone_room.transform.parent = this.transform;
	}
}
