using UnityEngine;
using System.Collections;

public class rSmoothMove : MonoBehaviour {
	public Vector3 destination;
	public float speed = 3.0f;
	bool moving = false;
	TextMesh tm;
	bool resetGame = false;
	float persDist = 2;

	void Start () {
		destination = transform.position;
		GameObject playerText = GameObject.Find("mnTxt");
		tm = playerText.GetComponent<TextMesh> ();

	}

	void Update () {
		transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);

	}

	public void setVector(Vector3 vec){
		destination = vec;
	}
	public void setSpeed(float inSpeed){
		speed = inSpeed;
	}

	public void setDist(float y){
		persDist = y;
	}
}