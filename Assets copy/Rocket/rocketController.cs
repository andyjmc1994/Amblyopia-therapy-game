using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

	List<Transform> squareList = LevelSetup.squareList;
	private int currentSquare;
	Transform newSquare;
	Transform oldSquare;
	int targetSquare;
	public Vector3 rBase;
	List<int> area = new List<int>();
	List<int> targets = new List<int>();
	LineRenderer lineRenderer;

	rSmoothMove rsm;
	bool inplay = false;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void setSquare(int oldSquareId, int newSquareID) {	


		//float step = speed * Time.deltaTime;
		oldSquare = squareList [(oldSquareId) -1];
		newSquare = squareList [(newSquareID) -1];
		rsm = GetComponentInParent<rSmoothMove> ();
		//rsm.setSpeed (3.0F);

		SquareSetup oldSquareSettings = newSquare.GetComponent<SquareSetup>();
		SquareSetup newSquareSettings = newSquare.GetComponent<SquareSetup>();

		//transform.position = squareSettings.getCoords ();
		rBase = oldSquareSettings.getCoords();
		if (inplay == true) {
			rsm.setSpeed (3.0F);
			rsm.destination = Vector3.MoveTowards (oldSquareSettings.getCoords (), newSquareSettings.getCoords (), (float)0.0001);
	/*		SquareSetup rocketSquare = squareList[oldSquareId].GetComponent<SquareSetup>();
			SquareSetup preRocketSquare = squareList[currentSquare].GetComponent<SquareSetup>();
			preRocketSquare.takeRocket ();
			Transform x = preRocketSquare.getRocket ();
			rocketSquare.giveRocket (x);*/

		} else {
			transform.position = Vector3.MoveTowards (oldSquareSettings.getCoords (), newSquareSettings.getCoords (), (float)0.0001);
			inplay = true;
		}
		currentSquare = newSquareID;

	}

	public void setTargetSquare(List<int> targs){
		targets = targs;
		//targetSquare = ts;
	}

	public int getTargetSquare(){
			return targets [0];
	}


	public void fireIt(Vector3 pos){
		rsm.destination = pos;
		rsm.setSpeed (3.0F);

	}

	public void setArea(List<int> x){
		area = x;

	}

	public int move(){
		int x = Random.Range (0, area.Count);
		setSquare (area [x], area[x]);
		return area [x];
	}

	public void setDist(float u){
		rsm.setDist (u);
	}
}
