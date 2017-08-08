using UnityEngine;
using UnityEngine.UI;

public class show_generating : MonoBehaviour {

	public Image img;
	public Text txt;
	public Button btn;
	public Slider sld;
	
	// Use this for initialization
	void Start () {
		img = this.gameObject.GetComponent<Image>();
		txt = this.gameObject.GetComponent<Text>();
		btn = this.gameObject.GetComponent<Button>();
		sld = this.gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		bool show = Generator.Data.generating;
		
		if(img != null)
			img.enabled = show;
		if(btn != null)
			btn.enabled = show;
		if(txt != null)
			txt.enabled = show;
		if(sld != null)
			sld.enabled = show;
		
	}
}
