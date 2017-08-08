using UnityEngine;
using UnityEngine.UI;

public class Info_script : MonoBehaviour {

	public Text txt;

	void Start () {
		txt = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = Generator.Data.infoText;
	}
}
