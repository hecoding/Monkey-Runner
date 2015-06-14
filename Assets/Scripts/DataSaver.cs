using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level {
	public bool locked { get; set; }
	public int starsAchieved { get; set; }
	
	public Level() {
		locked = true;
		starsAchieved = 0;
	}
}

public class DataSaver : MonoBehaviour {
	public static DataSaver S;

	public List<Level> levels;

	void Awake () {
		if (S == null) {
			DontDestroyOnLoad (gameObject);
			S = this;
		} else if (S != this)
			Destroy (gameObject);

		initLockedLevels ();
	}

	private void initLockedLevels(){
		levels = new List<Level> ();
		
		reset ();
	}

	public void reset() {
		levels.Clear ();

		for (int i = 0; i < 10; i++)
			levels.Add (new Level ());
		
		levels [0].locked = false;
	}
}
