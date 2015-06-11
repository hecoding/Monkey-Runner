using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {
	// Use this for initialization

	void Start () {

		for (int i = 0; i < 10; i++) {
			int numLevel = i+1;
			string level = "BLevel" + numLevel.ToString();
			Button btn = GameObject.Find(level).GetComponent<Button>();

			if(GameController.S.lockedLevels[i])
				btn.interactable = false;

			Image[] imgs = btn.GetComponentsInChildren<Image>();
			for(int j = 0; j < 3-GameController.S.starsLevels[i]; j++){//Pinta solo el numero de estrellas conseguidas.
					imgs[3-j].enabled = false;
			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
