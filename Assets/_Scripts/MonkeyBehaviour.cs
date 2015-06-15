using UnityEngine;
using System.Collections;

public class MonkeyBehaviour : MonoBehaviour {

	public float transmittedForceToLiana;
	public float initialForce;
	public Vector2 initialDirection;
	public float jumpForce;
	public Vector2 jumpDirection;
	public GameObject attachedObject;
	public bool recolocateObject;
	public GameObject createAfterBonusCatch;

	private bool hanged;
	private Transform oldParent;
	private Rigidbody2D rb;

	private Transform oldAttObjParent;
	private bool attObjWasKinematic;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();

		oldParent = transform.parent;

		if (attachedObject)
			attachObject (attachedObject);
		attObjWasKinematic = false;
	}

	void Start () {
		GetComponent<Rigidbody2D>().AddForce (initialDirection * initialForce);
	}

	void Update () {
	
	}

	void FixedUpdate() {
		if (hanged && Input.GetKeyUp ("space"))
			unhangFromLiana ();
	}

	void OnTriggerEnter2D (Collider2D other)  {
		switch (other.tag) {
		case "Liana": 
			if (!hanged && Input.GetKey("space"))
				hangFromLiana (other.gameObject);
			break;
		case "Banana":
		case "Pineapple":
		case "BananaBunch":
			GameController.S.modifyPoints (GameController.S.getPoints (other.tag));
			if (createAfterBonusCatch) {
				GameObject a = (GameObject)Instantiate (createAfterBonusCatch, other.transform.position, Quaternion.identity);
				ParticleSystem b = a.GetComponent<ParticleSystem> ();
				if (b)
					b.Play();
			}
			Destroy (other.gameObject);
			break;
		}
	}

	void hangFromLiana (GameObject other) {
		rb.isKinematic = true;
		transform.parent = other.transform;
		// setting to pretend the monkey is grabbing the liana (the sub is to preserve his real y)
		transform.position = other.transform.position
			- new Vector3 (0.6f, (other.transform.position.y - transform.position.y), 0);

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
		if (Random.Range (0, 5) == 0)
			GetComponent<AudioSource> ().Play ();
	}

	public void attachObject (GameObject obj) {
		if (obj) attachedObject = obj;
		else 	 return;
		
		if (!obj.GetComponent<Rigidbody2D> () || obj.GetComponent<Rigidbody2D> ().isKinematic)
			attObjWasKinematic = true;
		else {
			rb.mass += obj.GetComponent<Rigidbody2D>().mass;
			obj.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		
		oldAttObjParent = obj.transform.parent;
		obj.transform.parent = transform;

		// setting to pretend the monkey is grabbing the object
		if (recolocateObject)
			obj.transform.position = transform.position - new Vector3(0.5f,0.8f,0);
	}
	
	public void deattachObject (GameObject obj) {
		if (!attObjWasKinematic) {
			obj.GetComponent<Rigidbody2D> ().isKinematic = false;
			rb.mass -= obj.GetComponent<Rigidbody2D>().mass;
		}
		
		obj.transform.parent = oldAttObjParent;
	}
}
