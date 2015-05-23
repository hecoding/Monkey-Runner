using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	static public PlayerController S;

	public float initialForce = 250.0f;
	public float maxAbsVelX = 20.0f;
	public float maxAbsVelY = 20.0f;
	public float jumpForce = 250.0f;
	public float transmittedForceToLiana = 1000.0f;

	private Rigidbody2D rb;
	private Transform oldParent;
	private bool hanged;
	private Bounds playerBounds;
	private float lowerBound;
	private bool velChanged;
	private float currentVelX;
	private float currentVelY;

	void Awake() {
		S = this;
	}
	
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		oldParent = transform.parent;
		lowerBound = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
		velChanged = false;
		currentVelX = currentVelY = 0f;

		rb.AddForce (new Vector2(2, 1) * initialForce);
	}

	void Update () {

	}

	void FixedUpdate () {
		if (hanged && Input.GetKeyUp ("space")) {
			rb.isKinematic = false;
			rb.AddForce (new Vector2(2, 1) * jumpForce);
			transform.rotation = Quaternion.Euler(0,0,0);
			// some inverse force to the liana would be nice

			transform.parent = oldParent;
			hanged = false;
		}

		if (transform.position.y < lowerBound) {
			// do whatever it has to be done when player die
			transform.position = new Vector3(-5.92f,-2.35f,0f);
		}

		currentVelX = rb.velocity.x;
		currentVelY = rb.velocity.y;

		if (Mathf.Abs (rb.velocity.x) > maxAbsVelX) {
			currentVelX = maxAbsVelX * Mathf.Sign(rb.velocity.x);
			velChanged = true;
		}
		if (Mathf.Abs (rb.velocity.y) > maxAbsVelY) {
			currentVelY = maxAbsVelY * Mathf.Sign(rb.velocity.y);
			velChanged = true;
		}

		if (velChanged) {
			rb.velocity = new Vector2 (currentVelX, currentVelY);
			velChanged = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other)  {
		switch (other.tag) {
		case "Liana": 
			if (!hanged && Input.GetKey("space")) {
				other.attachedRigidbody.AddForce(new Vector2(1,0) * transmittedForceToLiana);

				rb.isKinematic = true;
				transform.parent = other.transform;

				hanged = true;
			}

			break;
		}
	}
}
