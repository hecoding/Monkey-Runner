using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// a public reference to the player would be better than take it directly
	public bool freezeY;

	private Vector3 initialPos;
	private float currentYPos;
	private float offsetXPos;

	void Start () {
		initialPos = transform.position;
		offsetXPos = transform.position.x - PlayerController.S.transform.position.x;
	}

	void Update () {
		if (freezeY) currentYPos = initialPos.y;
		else 		 currentYPos = PlayerController.S.transform.position.y;

		transform.position = new Vector3 (PlayerController.S.transform.position.x + offsetXPos, currentYPos, initialPos.z);
	}
}
