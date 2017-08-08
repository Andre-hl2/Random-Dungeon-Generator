using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Generator : MonoBehaviour {

	public static Generator Data;

	public GameObject RootCreator;
	public int number_initial_rooms;
	public Sprite tile_selected;
	public Sprite tile_game;
	public bool generating;
	public GameObject rootChar;

	public GameObject inimigo_cobra;
	public GameObject inimigo_golem;

	public int enemiesToKill = 0;
	public int enemiesKilled = 0;

	public string infoText;
	public string[] Text = new string[9];
	public Color color;

	public void Awake() {
		if (Data == null) {
			Data = this;
			number_initial_rooms = 30;
			generating = true;
		}
		infoText = "- Selecione o número de salas que deseja criar.\n" +
				   "- Clique nos botões para gerar um terreno e começar a exploração.\n" +
				   "- Siga a ordem de cima para baixo.";
	}

	void Start () {
		Text [0] = "As salas são criadas de acordo com o número escolhido. Para cada sala é definido um ponto randômico dentro de um raio que será a sua posição inicial, além de ter um tamanho em largura e altura em tiles randômicos.";
		Text [1] = "Quando a área em tiles de uma sala for maior que 10, ela se torna elegível para fazer parte do layout final da dungeon. Essa regra pode ser alterada para: 'largura maior que X' e 'altura maior que Y' por exemplo.";
		Text [2] = "Deleta as salas que não possuem a área em tiles maior que 10, ou seja, as que não irão fazer parte do layout final da dungeon.";
		Text [3] = "O box collider das salas é ligado e a física da Unity se encarrega de afastar as salas até que não exista mais interseção entre elas.";
		Text [4] = "O box collider é desligado e a posição da sala é arredondada para fixar as posições corretamente, porque cada tile tem exatamente uma unidade de tamanho, então ao arredondarmos as posições, teremos certeza que todos os tiles estarão alinhados.";
		Text [5] = "As salas são renderizadas criando um terreno só.";
		Text [6] = "São criadas bordas para cada sala dentro do terreno.";
		Text [7] = "Verifica cada parede de cada uma das salas, caso exista uma sala ao lado (acima, direita, abaixo ou esquerda), a parede é deletada abrindo caminho para a sala vizinha.";
		Text [8] = "Cria um 'x' de inimigos (de 1 a 20 dependendo do tipo do inimigo) de maneira randômica, sorteando o tipo de inimigo para cada sala.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate() {
		color = new Color(0.31764f, 0.59607f, 0.74117F, 1.0F);
		Camera.main.backgroundColor = color;
		Generator.Data.infoText = "Gerar salas iniciais: " + Text[0];
		Generator.Data.generating = true;
		Generator.Data.rootChar.transform.position = new Vector3(100,100,0);
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject obj in objs) {
			Destroy(obj.gameObject);
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Inimigos");
		foreach(GameObject ini in enemies) {
			Destroy(ini.gameObject);
		}
		StartCoroutine("SpawnRoot");
	}
			
	public void SelectMain() {
			Generator.Data.infoText = "Escolher salas elegíveis: " + Text[1];
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject obj in objs) {
			RootCreator root = obj.gameObject.GetComponent<RootCreator>();
			if(root.sizeX * root.sizeY > 10) {
				root.selected = true;
				TileScript[] tiles = obj.GetComponentsInChildren<TileScript>();
				foreach(TileScript tile in tiles) {
					tile.gameObject.GetComponent<SpriteRenderer>().sprite = Generator.Data.tile_selected;
				}
			}
		}
	}

	public void removeUnselected() {
		Generator.Data.infoText = "Remover salas não elegíveis: " + Text[2];
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject obj in objs) {
			RootCreator root = obj.gameObject.GetComponent<RootCreator>();
			if(!root.selected)
				Destroy(obj.gameObject);
		}
	}

	public void Afasta() {
		Generator.Data.infoText = "Afastar salas: " + Text[3];
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject obj in objs) {
			obj.gameObject.GetComponent<BoxCollider2D>().enabled=true;
		}
	}

	public void ReadHallWay() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		List<GameObject> needed = new List<GameObject>();
		foreach(GameObject obj in objs) {
			// Procura pro outras salas ao meu redor, indicando se eu preciso ou nao de um corredor
			Vector3 pos = obj.transform.position;
			RootCreator root = obj.gameObject.GetComponent<RootCreator>();
			Vector2 pointA = new Vector2(pos.x - 0.5f - (root.sizeX * 1.1f), pos.y - 0.5f - (root.sizeY * 1.1f));
			Vector2 pointB = new Vector2(pos.x - 0.5f + (root.sizeX * 1.1f), pos.y - 0.5f + (root.sizeY * 1.1f));
			Collider2D[] res = Physics2D.OverlapAreaAll(pointA, pointB);

			int numberOfIntersections = 0;
			// Se eu precisar de um corredor
			foreach(Collider2D coll in res) {
				if(coll.gameObject != obj.gameObject) {
					numberOfIntersections++;
				}
			}

			if(numberOfIntersections == 0) {
				needed.Add(obj);
			}
		}

		foreach(GameObject need in needed) {
			GameObject nearest = getNearestRoom(need, objs);
			Debug.DrawLine(need.transform.position, nearest.transform.position, Color.black, 10);
		}
	}

	public void RoundPositions() {
		Generator.Data.infoText = "Fixar salas: " + Text[4];
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject obj in objs) {
			obj.GetComponent<BoxCollider2D>().enabled = false;
		}
		foreach(GameObject obj in objs) {
			Vector3 pos = obj.transform.position;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
			obj.transform.position = pos;
		}
	}

	public void TransformTerrain() {
			Generator.Data.infoText = "Transformar salas em terreno: " + Text[5];
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach(GameObject tile in tiles) {
			tile.gameObject.GetComponent<SpriteRenderer>().sprite = tile_game;
		}
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		foreach(GameObject sala in rooms) {
			sala.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	public void makeBorder() {
			Generator.Data.infoText = "Criar paredes: " + Text[6];
		StartCoroutine("CreateWall");
	}

	public void UnmakeBorder() {
			Generator.Data.infoText = "Organizar caminhos: " + Text[7];
		StartCoroutine("DestroyWall");
	}

	public void PopularTerreno() {
			Generator.Data.infoText = "Popular terreno: " + Text[8];
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		GameObject room = getNearestRoom(Camera.main.gameObject, objs);
		Generator.Data.rootChar.transform.position = room.transform.position;
		room.gameObject.GetComponent<RootCreator>().spawnPoint = true;
		StartCoroutine("Populate");
	}

	public void beginGame() {
		Generator.Data.generating = false;
		Camera.main.backgroundColor = Color.black;
		Generator.Data.infoText = "Use as teclas WASD para mover o player e as setas para atirar";
	}

	// Funcoes de Apoio
	public IEnumerator SpawnRoot() {
		for(int i=0;i<Generator.Data.number_initial_rooms;i++) {
			Generator.Spawn(RootCreator, Random.insideUnitCircle * 5, Quaternion.identity);
			yield return null;
		}
	}

	public IEnumerator CreateWall() {
		GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject root in roots) {
			foreach(Transform child in root.transform) {
				if(child.gameObject.tag == "Tile") {
					TileScript ti = child.gameObject.GetComponent<TileScript>();
					if(ti.border) {
						ti.MakeBorder();
					}
				}
			}
			yield return null;
		}
	}

	public IEnumerator DestroyWall() {
		GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject root in roots) {
			foreach(Transform child in root.transform) {
				if(child.gameObject.tag == "Tile") {
					TileScript ti = child.gameObject.GetComponent<TileScript>();
					if(ti.border) {
						ti.unMakeBorder();
					}
				}
			}
			yield return null;
		}
	}

	public IEnumerator Populate() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Root");
		foreach(GameObject sala in objs) {
			RootCreator root = sala.gameObject.GetComponent<RootCreator>();
			if(!root.spawnPoint) {
				
				// TIPO DE SALA
				int ran = Random.Range(1,3);
				
				switch(ran) {
				case 1: //inimigo cobra
					
					int dom = Random.Range(2,30); //numero de cobras
					for (int i=0;i<dom;i++) {
						int posX = Random.Range((-root.sizeX)+1, root.sizeX -1);
						int posY = Random.Range( (-root.sizeY)+1, root.sizeY-1);
						Generator.Spawn(Generator.Data.inimigo_cobra, new Vector3(sala.transform.position.x + posX, sala.transform.position.y + posY), Quaternion.identity);
						Generator.Data.enemiesToKill++;
					}
					
					break;
				case 2: //inimigo golem
					
					int ram = Random.Range(1,10); //numero de cobras
					for (int i=0;i<ram;i++) {
						int posX = Random.Range((-root.sizeX)+1, root.sizeX -1);
						int posY = Random.Range( (-root.sizeY)+1, root.sizeY-1);
						Generator.Spawn(Generator.Data.inimigo_golem, new Vector3(sala.transform.position.x + posX, sala.transform.position.y + posY), Quaternion.identity);
						Generator.Data.enemiesToKill++;
					}
					
					break;
				case 3: //chefe
					break;
				}
				
			}
			yield return null;
		}
	}

	public GameObject getNearestRoom(GameObject origin, GameObject[] rooms) {
		GameObject tMin = null;
		float minDist = Mathf.Infinity;
	
		foreach(GameObject room in rooms)
		{
			if(room.gameObject != origin.gameObject) {
				float dist = Vector3.Distance(room.transform.position, origin.transform.position);
				if (dist < minDist)
				{
					tMin = room;
					minDist = dist;
				}
			}
		}
		return tMin;
	}

	static public GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation) {
#if UNITY_EDITOR
		GameObject clone = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
		clone.transform.position = position;
		clone.transform.rotation = rotation;
		return clone;
#else
		return Object.Instantiate(gameObject, position, rotation) as GameObject;
#endif
	}
}
