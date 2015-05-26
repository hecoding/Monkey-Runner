using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	void Start () {

	}

	void Update () {

	}

	public void changeToScene (string scene) {
		Application.LoadLevel (scene);
	}

	public void Quit() {
		Application.Quit();
	}
}
