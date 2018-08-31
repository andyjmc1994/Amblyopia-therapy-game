using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSetup : MonoBehaviour {

	public Vector3 coords;
	public int iD;
	bool gotRocket = false;
	Transform rocketHold;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getCoords () {
		return coords;
	}

	public void setCoords (Vector3 squareCoords) {
		coords = squareCoords;
	//	Debug.Log (coords);
	}
	public int getsquareID () {
		return iD;
	}

	public void setsquareID (int cellId) {
		iD = cellId;
	//	Debug.Log (iD);
	}

	public bool isRocket(){
		return gotRocket;
	}
	public void giveRocket(Transform theRocket){
		rocketHold = theRocket;
		gotRocket = true;
	}
	public Transform getRocket(){
		return rocketHold;
	}
	public void takeRocket(){
		gotRocket = false;
	}
}
