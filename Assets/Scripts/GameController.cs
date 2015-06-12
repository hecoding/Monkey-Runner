using UnityEngine;
using UnityEngine.UI;
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

public class GameController : MonoBehaviour {
	static public GameController S;

	// a public reference to the player would be better than take it directly
	public bool freezeCameraY;
	public Text countText;
	public Text winText;
	public Text loseText;
	private int _playerPoints;
	public int playerPoints { get { return _playerPoints; } }

	private Vector3 initialCameraPos;
	private float currentCameraYPos;
	private float offsetCameraXPos;

	public bool instantiatedPlayer;
	public List<Level> levels;
	public static int NUM_LEVELS = 10;

	private Dictionary<string, int> bonusPoints = new Dictionary<string, int>();

	void Awake() {
		S = this;

		instantiatedPlayer = true;
		initLockedLevels ();
	}

	void Start () {
		if (instantiatedPlayer) {
			initialCameraPos = transform.position;
			offsetCameraXPos = transform.position.x - PlayerController.S.transform.position.x;
		
			_playerPoints = 0;
			setCountText ();
		}
		
		bonusPoints.Add ("Banana", 2);
		bonusPoints.Add ("BananaBunch", 5);
		bonusPoints.Add ("Pineapple", 7);
	}
	
	void Update () {
		Debug.Log ("instantiated: " + instantiatedPlayer);
		if (instantiatedPlayer) {
			if (freezeCameraY) currentCameraYPos = initialCameraPos.y;
			else 		 currentCameraYPos = PlayerController.S.transform.position.y;

			transform.position = new Vector3 (PlayerController.S.transform.position.x + offsetCameraXPos, currentCameraYPos, initialCameraPos.z);
		}
	}

	public void winLevel() {
		winText.text = "You Win! Total points: " + _playerPoints.ToString ();
		countText.text = "";
	}

	public void onDie() {
		loseText.text = "You Lose! Total points: " + _playerPoints.ToString ();
		countText.text = "";
	}

	public int getPoints (string bonusName) {
		return bonusPoints[bonusName];
	}

	public void modifyPoints (int points) {
		_playerPoints += points;
		setCountText ();
	}

	void setCountText() {
		countText.text = "Score: " + _playerPoints.ToString ();
	}

	private void initLockedLevels(){
		levels = new List<Level> ();

		for (int i = 0; i < NUM_LEVELS; i++)
			levels.Add (new Level ());

		levels [0].locked = false;
	}
	
	public void unlockLevel(int level){
		levels [level].locked = false;
	}
	
	public void setStarsLevel (int level, int stars){
		levels [level].starsAchieved = stars;
	}
}
