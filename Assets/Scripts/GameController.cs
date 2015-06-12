using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
	private int currentLevel;
	private int totalPointsOfCurrentLevel;

	public bool instantiatedPlayer;
	//public List<Level> DataSaver.S.levels;
	public static int NUM_LEVELS = 10;
	public static int MIN_STARS_TO_UNLOCK = 1;

	private Dictionary<string, int> bonusPoints = new Dictionary<string, int>();

	void Awake() {
//		if (S == null) {
			S = this;
//			DontDestroyOnLoad (gameObject);
//		} else
//			Destroy (this.gameObject);

		instantiatedPlayer = true;
		//initLockedLevels ();

		bonusPoints.Add ("Banana", 2);
		bonusPoints.Add ("BananaBunch", 5);
		bonusPoints.Add ("Pineapple", 7);
	}

	void Start () {
		//if (instantiatedPlayer) {
			initialCameraPos = transform.position;
			offsetCameraXPos = transform.position.x - PlayerController.S.transform.position.x;
		
			_playerPoints = 0;
			setCountText ();
		//}
	}
	
	void Update () {
		Debug.Log ("instantiated: " + instantiatedPlayer);
		//if (instantiatedPlayer) {
			if (freezeCameraY) currentCameraYPos = initialCameraPos.y;
			else 		 currentCameraYPos = PlayerController.S.transform.position.y;

			transform.position = new Vector3 (PlayerController.S.transform.position.x + offsetCameraXPos, currentCameraYPos, initialCameraPos.z);
		//}
	}

	public void setLevel (int currLevel, int totalPoints) {
		currentLevel = currLevel;
		totalPointsOfCurrentLevel = totalPoints;
	}

	public void winLevel() {
		winText.text = "You Win! Total points: " + _playerPoints.ToString ();
		countText.text = "";

		int stars = Mathf.FloorToInt (((float)_playerPoints / (float)totalPointsOfCurrentLevel) * 3);

		if (stars > DataSaver.S.levels [currentLevel - 1].starsAchieved) {
			DataSaver.S.levels [currentLevel - 1].starsAchieved = stars;

			if (currentLevel != NUM_LEVELS && DataSaver.S.levels [currentLevel - 1].starsAchieved >= MIN_STARS_TO_UNLOCK)
				DataSaver.S.levels [currentLevel].locked = false; // +1 for the next, -1 for the index
		}

		instantiatedPlayer = false;
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
}
