using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControllerTester : MonoBehaviour {


	List<Transform> squareList = LevelSetup.squareList;
	public GameObject dice;
	public int currentSquare;
	Transform newSquare;
	Transform oldSquare;
	int someValue = 0;
	public float speed;
	private bool isActive;
	GameObject playerText;
	public int playerNum;
	TextMesh playerMesh;
	public int gameEnd = 0;
	public bool reset = false;
	bool targetpicking = false;
	GameObject fDust;


	private bool dieSwitch = false;


	// Use this for initialization
	void Start () {

		currentSquare = 1;
		playerText = GameObject.Find("mnTxt");
		fDust = GameObject.Find("FairyDust");

		playerMesh = playerText.GetComponent<TextMesh> ();
		dice = GameObject.Find("Die");
		
	}

	public void setSquare(int oldSquareId, int newSquareID) {	
		//float step = speed * Time.deltaTime;


		oldSquare = squareList [(oldSquareId) -1];
		newSquare = squareList [(newSquareID) -1];

		SquareSetup oldSquareSettings = newSquare.GetComponent<SquareSetup>();
		SquareSetup newSquareSettings = newSquare.GetComponent<SquareSetup>();


		SmoothMove sm = GetComponentInParent<SmoothMove>();

		sm.setSpeed (3.0F);
		foreach (Transform square in squareList) {
			SquareSetup sq = square.GetComponent<SquareSetup>();
			if (sq.isRocket()) {
				sq.takeRocket ();
				int c = sq.getRocket ().GetComponent<rocketController>().move();
				squareList[c -1].GetComponent<SquareSetup>().giveRocket(sq.getRocket());
			}

		}

		if (newSquareSettings.isRocket ()) {
			Transform rocket = newSquareSettings.getRocket ();
			rocketController rc = rocket.GetComponent<rocketController>();
			Transform tc = squareList [(rc.getTargetSquare ()) - 1];
			SquareSetup tcSquareSettings = tc.GetComponent<SquareSetup> ();
			sm.setVector (tcSquareSettings.getCoords ());

			rc.fireIt (tcSquareSettings.getCoords ());
			//transform.position = Vector3.position (oldSquareSettings.getCoords (), tcSquareSettings.getCoords (), (float)0.0001);
			//transform.position = tcSquareSettings.getCoords ();
			currentSquare = tcSquareSettings.getsquareID ();
			Renderer r = rocket.GetComponent<Renderer> ();
			//r.enabled = false;
		} else {
			sm.setVector (newSquareSettings.getCoords ());
			//transform.position = newSquareSettings.getCoords ();
			//transform.position = Vector3.MoveTowards (oldSquareSettings.getCoords (), newSquareSettings.getCoords (), (float)0.0001);
			currentSquare = newSquareID;
		}
	
	}

	public void setSwitch() {
		dieSwitch = true;
	}

	public void setIsActive(bool value) {
		isActive = value;

	}

	public void setNum(int z) {
		playerNum = z;
	}


		

	// Update is called once per frame
	void Update () {
		
		if (!LevelSetup.uiEnabled) {
			if (targetpicking == false) {
				DisplayCurrentDieValue dieValue = dice.GetComponent<DisplayCurrentDieValue> ();
				if (Input.GetKeyDown (KeyCode.K)) {
					setSquare (currentSquare, currentSquare + 1);
				} else if (Input.GetKeyDown (KeyCode.L)) {
					setSquare (currentSquare, currentSquare + 2);
				} else if (dieValue.getRollComplete ()) {




					if (dieSwitch) {




						if (someValue > 0) {

							isActive = !isActive;

							if (isActive) {







								int setCurrentSquare = currentSquare;



								//for (int i = 1; i <= dieValue.getDieValue (); i++) {
								if (setCurrentSquare + dieValue.getDieValue () >= 64) {
									setSquare (64, 64);

								} else {
									setSquare (currentSquare, setCurrentSquare + dieValue.getDieValue ());
								}
								//System.Threading.Thread.Sleep(5000);

								//}




							}
						} else {
							playerMesh.text = "Player 1 Roll";
							Destroy (fDust);
						}

						dieSwitch = false;

						someValue = 1;
					}
						
				} else if (!dieValue.getRollComplete ()) { 

					dieSwitch = true;

				}
			}
		}
	}
}
