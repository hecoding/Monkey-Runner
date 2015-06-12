using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {

	public string framesFolder;
	public bool loop = false;
	public float FPS = 24;

	private float frameRateInSeconds;
	private Texture[] frames;
	private int counter = 0;
	private float nextPic = 0;

	void Start () {
		frameRateInSeconds = 1 / FPS;
		frames = Resources.LoadAll<Texture> (framesFolder);
	}

	void Update () {
		if (Time.time > nextPic && counter < frames.Length) {
			GetComponent<Renderer>().material.mainTexture = frames[counter];

			nextPic = Time.time + frameRateInSeconds;
			counter += 1;
		}

		if (counter >= frames.LongLength) {
			if (loop) counter = 0;
			else {
				// we're done, make callback or something
			}
		}
	}
	// no need to Resources.UnloadAsset since (I assume) we're destroying the current scene right after this
}