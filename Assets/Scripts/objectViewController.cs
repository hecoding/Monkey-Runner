using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.IO;  

public class objectViewController : MonoBehaviour {

	public Text objectText;

	// Use this for initialization
	void Start () {

		SpriteRenderer img = GameObject.Find("Object").GetComponent<SpriteRenderer>();

		if(img == null)
			Debug.Log("Load Object Fail");

		Sprite newSprite = Resources.Load<Sprite> ("Pendientes");

		Debug.Log(newSprite);

		img.sprite = newSprite;

		Debug.Log(img);

		TextAsset texto = Resources.Load<TextAsset> ("texto");

		objectText.text = texto.text;



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
