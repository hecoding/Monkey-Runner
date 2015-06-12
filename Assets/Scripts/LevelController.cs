using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	void Awake () {

	}

	void Start () {
		for (int i = 0; i < DataSaver.S.levels.Count; i++) {
			Button btn = GameObject.Find("BLevel" + (i + 1)).GetComponent<Button> ();

			if(DataSaver.S.levels[i].locked)
				btn.interactable = false;

			Image[] imgs = btn.GetComponentsInChildren<Image>();
			// show number of stars achieved
			for(int j = 0; j < 3-DataSaver.S.levels[i].starsAchieved; j++)
					imgs[3-j].enabled = false;
		}
	}

	void Update () {
		
	}
}
