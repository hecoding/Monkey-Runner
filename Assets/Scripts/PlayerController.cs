using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	static public PlayerController S;

	public float initialForce;
	public float jumpForce;
	public Vector2 jumpDirection;
	public Vector2 maxAbsVelocity;
	public float transmittedForceToLiana;
	public GameObject attachedObject;

	private Rigidbody2D rb;
	private Transform oldParent;
	private bool hanged;
	private Bounds playerBounds;
	private float lowerBound;
	private bool velChanged;
	private float currentVelX;
	private float currentVelY;

	private Transform oldAttObjParent;
	private bool attObjWasKinematic;

	void Awake() {
		S = this;

		rb = GetComponent<Rigidbody2D> ();
		oldParent = transform.parent;
		lowerBound = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
		velChanged = false;
		currentVelX = currentVelY = 0f;
		
		if (attachedObject)
			attachObject (attachedObject);
		attObjWasKinematic = false;
	}
	
	void Start () {
		rb.AddForce (jumpDirection * initialForce);
	}

	void Update () {

	}

	void FixedUpdate () {
		if (hanged && Input.GetKeyUp ("space"))
			unhangFromLiana ();

		if (transform.position.y < lowerBound)
			die ();

		checkVelocityConstraints ();
	}

	void OnTriggerEnter2D (Collider2D other)  {
		switch (other.tag) {
		case "Liana": 
			if (!hanged && Input.GetKey("space"))
				hangFromLiana (other.gameObject);
			break;
		case "Banana":
		case "BananaBunch":
			GameController.S.modifyPoints (GameController.S.getPoints (other.tag));
			Destroy (other.gameObject);
			break;
		case "Finish":
			rb.isKinematic = true; // do some good looking dance
			GameController.S.winLevel();
			break;
		}
	}

	void checkVelocityConstraints () {
		Rigidbody2D rigidb;

		if (hanged) rigidb = transform.parent.GetComponent<Rigidbody2D> ();
		else rigidb = rb;

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

	void hangFromLiana (GameObject other) {
		rb.isKinematic = true;
		transform.parent = other.transform;
		transform.position = other.transform.position - new Vector3 (0.6f, 0, 0);
		other.GetComponent<Rigidbody2D>().mass += rb.mass;

		other.GetComponent<Rigidbody2D>().AddForce (new Vector2 (1, 0) * transmittedForceToLiana * rb.mass * 0.4f);
		hanged = true;
	}

	void unhangFromLiana() {
		rb.isKinematic = false;
		rb.AddForce (jumpDirection * jumpForce);
		transform.rotation = Quaternion.Euler (0, 0, 0);
		// some inverse force to the liana would be nice
		transform.parent.GetComponent<Rigidbody2D> ().mass -= rb.mass;
		transform.parent = oldParent;
		hanged = false;
	}

	void die() {
		// transform.position = new Vector3(-5.92f,-2.35f,0f);
		GameController.S.onDie ();
		gameObject.SetActive (false);
	}

	public void attachObject (GameObject obj) {
		if (obj) attachedObject = obj;
		else 	 return;

		if (!obj.GetComponent<Rigidbody2D> () || obj.GetComponent<Rigidbody2D> ().isKinematic)
			attObjWasKinematic = true;
		else
			obj.GetComponent<Rigidbody2D> ().isKinematic = true;

		oldAttObjParent = obj.transform.parent;
		obj.transform.parent = transform;
		// setting to pretend the monkey is grabbing the object
		obj.transform.position = transform.position - new Vector3(0.5f,0.8f,0);
	}

	public void deattachObject (GameObject obj) {
		if (!attObjWasKinematic)
			obj.GetComponent<Rigidbody2D> ().isKinematic = false;

		obj.transform.parent = oldAttObjParent;
	}
}
