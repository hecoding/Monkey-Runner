using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private bool hanged;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		rb.AddForce (new Vector2(2, 1) * 250.0f);
		Debug.Log ("hola");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)  {
		Debug.Log ("colision");

		if (!hanged) {
			other.attachedRigidbody.velocity = rb.velocity;
			//rb.velocity = Vector2.zero;
			transform.parent = other.transform;
			//Destroy (this.gameObject);
			hanged = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {

		hanged = false;
	}
}
