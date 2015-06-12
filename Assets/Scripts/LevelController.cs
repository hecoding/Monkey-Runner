using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	void Awake () {

	}

	void Start () {
		for (int i = 0; i < GameController.S.levels.Count; i++) {
			Button btn = GameObject.Find("BLevel" + (i + 1)).GetComponent<Button> ();

			if(GameController.S.levels[i].locked)
				btn.interactable = false;

			Image[] imgs = btn.GetComponentsInChildren<Image>();
			// show number of stars achieved
			for(int j = 0; j < 3-GameController.S.levels[i].starsAchieved; j++)
					imgs[3-j].enabled = false;
		}
	}

	void Update () {
		
	}
}
