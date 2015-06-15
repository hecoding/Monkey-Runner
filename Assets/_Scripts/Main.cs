using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	private float oldTimeScale;

	void Start () {
		oldTimeScale = Time.timeScale;
	}

	void Update () {

	}

	public void changeToScene (string scene) {
		Application.LoadLevel (scene);
	}

	public void rechargeScene() {
		Application.LoadLevel ("Level " + GameController.S.getCurrentLevel());
	}

	public void Pause() {
		if (Time.timeScale != 0) {
			oldTimeScale = Time.timeScale;
			Time.timeScale = 0;
		} else
			Time.timeScale = oldTimeScale;
	}

	public void resume() {
		Pause ();
	}

	public bool isPaused() {
		return Time.timeScale == 0;
	}

	public void Quit() {
		Application.Quit();
	}
}
