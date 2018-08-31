using UnityEngine;
using System.Collections;

public class SmoothMove : MonoBehaviour {
	public Vector3 destination;
	public float speed = 0.1f;
	bool moving = false;
	TextMesh tm;
	bool resetGame = false;

	void Start () {
		destination = transform.position;
		GameObject playerText = GameObject.Find("mnTxt");
		tm = playerText.GetComponent<TextMesh> ();

	}

	void Update () {
		transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
		hasStopped();
	}

	public void setVector(Vector3 vec){
		destination = vec;
	}
	public void setSpeed(float inSpeed){
		speed = inSpeed;
	}

	public bool hasStopped() {

		int y = GetComponentInParent<TempControllerTester> ().playerNum;
		float dist = Vector3.Distance (destination, transform.position);

		if (dist < 1) {
			
			if (moving) {

				if (y == 1) {
					y = 2;
				} 
				else {
					y = 1;
				}

				tm.text = "player " + y.ToString() + " roll";
			}
			moving = false;
			resetGame = false;
			GetComponentInParent<TempControllerTester> ().reset = false;
			return true;
		}

		moving = true;

		if (GetComponentInParent<TempControllerTester> ().currentSquare == 64) {

			GetComponentInParent<TempControllerTester> ().gameEnd = 1;
			resetGame = true;
		}

		if (!GetComponentInParent<TempControllerTester> ().reset) {

			tm.text = "moving...";
		} 

		if(resetGame) {

			tm.text = "player " + y.ToString() + " wins!";
		}

		return false;
	}
}