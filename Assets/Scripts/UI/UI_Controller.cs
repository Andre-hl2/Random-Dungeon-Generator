using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

	public Text number_initial;
	public Slider zoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		number_initial.text = Generator.Data.number_initial_rooms.ToString();

		if(Generator.Data.generating) {
			Camera.main.orthographicSize = zoom.value;
			Camera.main.transform.position = new Vector3(0,0,-10);
		} else {
			Camera.main.orthographicSize = 5;
			Vector3 pos = Generator.Data.rootChar.transform.position;
			pos.z = -10;
			Camera.main.transform.position = pos;
		}
	}

	public void addInitialRoom(int number) {
		Generator.Data.infoText = "Selecione o número de salas e clique em Gerar Salas Iniciais.";
		if ((Generator.Data.number_initial_rooms += number) > 100) {
			Generator.Data.infoText = "Número máximo de salas é 100.";
			Generator.Data.number_initial_rooms = 100;
		}
	}

	public void removeInitialRoom(int number) {
		Generator.Data.infoText = "Selecione o número de salas e clique em Gerar Salas Iniciais.";
		Generator.Data.number_initial_rooms -= number;
		if (Generator.Data.number_initial_rooms < 10) {
			Generator.Data.infoText = "Número mínimo de salas é 10.";
			Generator.Data.number_initial_rooms = 10;
		}
	}
}
