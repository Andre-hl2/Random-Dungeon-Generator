using UnityEngine;
using UnityEngine.UI;

public class inimigosMortosScript : MonoBehaviour {

	public Text txt;

	// Use this for initialization
	void Start () {
		txt = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Inimigos mortos: "+Generator.Data.enemiesKilled.ToString()+" / "+Generator.Data.enemiesToKill.ToString();
	}
}