using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.IO; 
using System.Collections.Generic; 


public class objectViewController : MonoBehaviour {

	public Image image;
	public Text textHolder;
	
	private Dictionary<int, string> objects = new Dictionary<int, string>();//level-object

	// Use this for initialization
	void Start () {


		int randomNumber = Random.Range (1, 11);

		objects.Add (1, "Collar");
		objects.Add (2, "Tocado");
		objects.Add (3, "Tocado2");
		objects.Add (4, "Pendientes");
		objects.Add (5, "Vasija Shipibo");
		objects.Add (6, "Escudo Shuar");
		objects.Add (7, "Carcaj y dardos");
		objects.Add (8, "Brazalete");
		objects.Add (9, "Tambor");
		objects.Add (10, "Tobillera");

		Sprite newSprite = Resources.Load<Sprite> (objects[randomNumber]);
		image.sprite = newSprite;

		TextAsset texto = Resources.Load<TextAsset> (objects[randomNumber]);
		textHolder.text = texto.text;
	}
}
