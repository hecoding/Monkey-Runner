using UnityEngine;
using System.Collections;

public class BGController : MonoBehaviour {

	private float distanceToInitPos;
	private float sizeOfBackground;
	private float initialPos;
	private float initialPlayerPos;

	void Start () {
		distanceToInitPos = sizeOfBackground = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
		initialPos = transform.position.x;
		initialPlayerPos = PlayerController.S.transform.position.x;
	}

	void Update () {
	
	}

	void LateUpdate () {
		if (PlayerController.S.transform.position.x >= initialPlayerPos + distanceToInitPos) {
			transform.position = new Vector3 (initialPos + distanceToInitPos, transform.position.y, transform.position.z);
			distanceToInitPos += sizeOfBackground;
		}
	}
}
