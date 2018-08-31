using UnityEngine;
using System.Collections;

public class floater : MonoBehaviour {
	public Vector3 destination;
	public float speed = 0.2f;
	bool moving = false;
	TextMesh tm;
	bool resetGame = false;
	int x = 1;

	void Start () {
		destination = transform.position;
		GameObject playerText = GameObject.Find("mnTxt");
		tm = playerText.GetComponent<TextMesh> ();
		speed = 0.2f;
		x = Random.Range (0, 99);

	}

	void Update () {
		if (x > 100) {
			x = 0;
			if (destination.y == -30) {
				destination.y = -50;
			} else {
				destination.y = -30;
			}
		}
		x++;
		transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
	}

	public void setVector(Vector3 vec){
		destination = vec;
	}
	public void setSpeed(float inSpeed){
		speed = inSpeed;
	}

}