using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	void Start () {

	}

	void Update () {

	}

	public static void changeToScene (string scene) {
		Application.LoadLevel (scene);
	}

	public static void Quit() {
		Application.Quit();
	}
}
