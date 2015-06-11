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

	public bool[] lockedLevels;
	public int[] starsLevels;
	public static int NUM_LEVELS = 10;

	public void initLockedLevels(){

		lockedLevels = new bool[NUM_LEVELS];
		lockedLevels [0] = false;
		starsLevels = new int[NUM_LEVELS];
		starsLevels [0] = 0;
		for (int i = 1; i < NUM_LEVELS; i++) {
			lockedLevels [i] = true;
			starsLevels [i] = 0;
		}

	}

	public void unlockLevel(int level){
		lockedLevels [level] = false;
	}

	public void setStarsLevel (int level, int stars){
		starsLevels [level] = stars;
	}

	private Dictionary<string, int> bonusPoints = new Dictionary<string, int>();

	void Awake() {
		S = this;
	}

	void Start () {
		initialCameraPos = transform.position;
		offsetCameraXPos = transform.position.x - PlayerController.S.transform.position.x;

		_playerPoints = 0;
		setCountText ();
		initLockedLevels ();
		bonusPoints.Add ("Banana", 2);
		bonusPoints.Add ("BananaBunch", 5);
	}
	
	void Update () {
		if (freezeCameraY) currentCameraYPos = initialCameraPos.y;
		else 		 currentCameraYPos = PlayerController.S.transform.position.y;

		transform.position = new Vector3 (PlayerController.S.transform.position.x + offsetCameraXPos, currentCameraYPos, initialCameraPos.z);
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
}
