using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	static public PlayerController S;
	
	public Vector2 maxAbsVelocity;
	
	private Bounds playerBounds;
	private float lowerBound;
	private bool velChanged;
	private float currentVelX;
	private float currentVelY;

	void Awake() {
		S = this;

		lowerBound = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
		velChanged = false;
		currentVelX = currentVelY = 0f;
	}

	void Start() {
		int totalPoints = 0;
		GameObject bonuses = GameObject.Find ("Bonus");

		foreach (Transform bonus in bonuses.transform)
			totalPoints += GameController.S.getPoints (bonus.name.Split (' ') [0]);

		GameController.S.setLevel (int.Parse (Application.loadedLevelName.Split(' ')[1]), totalPoints);
	}

	void Update () {

	}

	void FixedUpdate () {
		if (transform.position.y < lowerBound)
			die ();

		checkVelocityConstraints ();
	}

	void OnTriggerEnter2D (Collider2D other)  {
		if (other.tag == "Finish") {
			GetComponent<Rigidbody2D>().isKinematic = true; // do some good looking dance
			GameController.S.winLevel();
		}
	}

	void checkVelocityConstraints () {
		Rigidbody2D rigidb;

		if (GetComponent<Rigidbody2D>().isKinematic && transform.parent)
			rigidb = transform.parent.GetComponent<Rigidbody2D> ();
		else rigidb = GetComponent<Rigidbody2D>();

		currentVelX = rigidb.velocity.x;
		currentVelY = rigidb.velocity.y;

		if (Mathf.Abs (rigidb.velocity.x) > maxAbsVelocity.x) {
			currentVelX = maxAbsVelocity.x * Mathf.Sign (rigidb.velocity.x);
			velChanged = true;
		}

		if (Mathf.Abs (rigidb.velocity.y) > maxAbsVelocity.y) {
			currentVelY = maxAbsVelocity.y * Mathf.Sign (rigidb.velocity.y);
			velChanged = true;
		}

		if (velChanged) {
			rigidb.velocity = new Vector2 (currentVelX, currentVelY);
			velChanged = false;
		}
	}

	void die() {
		// transform.position = new Vector3(-5.92f,-2.35f,0f);
		GameController.S.onDie ();
		gameObject.SetActive (false);
	}

	void OnDestroy() {
		// TODO this should be Resume() of other object
		if (Time.timeScale == 0)
			Time.timeScale = 1;
	}
}
