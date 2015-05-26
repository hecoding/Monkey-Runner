using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {
	
	public float tileSizeX;
	// a public reference to player would be better, and then rename bool to moveRelativeToGameObject
	public bool moveRelativeToPlayer;
	private float oldPos;

	private Vector3 startPosition;

	void Start () {
		startPosition = new Vector3 (transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x, transform.position.y, transform.position.z);
		oldPos = PlayerController.S.transform.position.x;
	}

	void Update () {
		if (moveRelativeToPlayer)
			moveRelativeTo (PlayerController.S.transform);
		else // patch since there are no time to work on scrolling
			transform.position = new Vector3 (Camera.main.transform.position.x, transform.position.y, transform.position.z);
	}

	void moveRelativeTo (Transform relativeTransform) {
		float newPos = relativeTransform.position.x;
		float moved = newPos - oldPos;
		
		float newPosition = Mathf.Repeat (-moved, tileSizeX);
		transform.position = startPosition + Vector3.right * newPosition;
	}
}
