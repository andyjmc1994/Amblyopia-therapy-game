using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelSetup : MonoBehaviour {

	int levelNum = 1;
	public Transform cell;
	public Transform dangerCell;
	public Transform mainChar;
	public Transform mainChar2;
	public Transform rocket;
	public Transform ufo;
	Transform mainGuy1;
	Transform mainGuy2;
	Transform rocket1;
	Transform rocket2;
	Transform rocket3;
	Transform ufo1;
	Transform ufo2;
	Transform ufo3;
	Transform ufo4;

	public Transform diceObj;
	public GameObject uiObj;
	public Button yourButton;
	public static bool uiEnabled = true;
	public GameObject txtMesh;
	int p1s = 0;
	int p2s = 0;


	public static List<Transform> squareList = new List<Transform>();


	// Use this for initialization
	void Start () {
		squareList.Clear ();
		if (levelNum == 1) {
			starterLevel ();
		}
		uiObj.SetActive (true);
		diceObj.GetComponent<Renderer> ().enabled = false;
		Button btn = yourButton.GetComponent<Button> ();
		foreach (Renderer r in diceObj.GetComponentsInChildren<Renderer>()){
			r.enabled = false;
		}
		btn.onClick.AddListener(TaskOnClick);

	}
	
	// Update is called once per frame
	void Update () {
		TempControllerTester controlScript = mainGuy1.GetComponent<TempControllerTester>();
		TempControllerTester controlScript2 = mainGuy2.GetComponent<TempControllerTester>();

		if (Input.GetKeyDown(KeyCode.C)) {
			if (CameraFollow.player == mainGuy1) {
				CameraFollow.player = mainGuy2;
				controlScript = mainGuy1.GetComponent<TempControllerTester>();
				controlScript.enabled = false;
				controlScript2 = mainGuy2.GetComponent<TempControllerTester>();
				controlScript2.enabled = true;

			} else {
				CameraFollow.player = mainGuy1;
				controlScript2 = mainGuy2.GetComponent<TempControllerTester>();
				controlScript.enabled = false;
				controlScript = mainGuy1.GetComponent<TempControllerTester>();
				controlScript.enabled = true;
			}
		}
		if (controlScript.gameEnd == 1) {
			controlScript.gameEnd = 0;
			controlScript.reset = true;
			controlScript2.reset = true;
			p2s++;
			txtMesh.GetComponent<TextMesh> ().text = "Player 1  [" + p1s + " - " + p2s + " ]  Player 2";
			controlScript.setSquare (1, 1);
			controlScript2.setSquare (1, 1);


		} else if (controlScript2.gameEnd == 1) {
			controlScript2.gameEnd = 0;
			controlScript.reset = true;
			controlScript2.reset = true;
			p1s++;

			txtMesh.GetComponent<TextMesh> ().text = "Player 1  [" + p1s + " - " + p2s + " ]  Player 2";

			controlScript.setSquare (1, 1);
			controlScript2.setSquare (1, 1);


		}
		
	}

	void starterLevel () {
		int squareCount = 1;
		bool reverse = false;
		for (int y = 0; y < 8; y++) {
			

			if (reverse) {
				for (int x = 7; x >= 0; x--) {
					TextMesh[] text;
					Transform squareCell;
					List<int> dangerValues = new List<int>(new int[] { 14,15,16,17,18,19,25,26,27,38,39,40,41,46,47,45,61,62, 63} );

					if (dangerValues.Contains(squareCount)) {
						squareCell = Instantiate (dangerCell, new Vector3 (x * 20, -40, y * 20), Quaternion.Euler (90, 0, 0));

					} else {
						squareCell = Instantiate (cell, new Vector3 (x * 20, -40, y * 20), Quaternion.Euler (90, 0, 0));
					}
					SquareSetup squareSettings = squareCell.GetComponent<SquareSetup>();
					squareSettings.setCoords (new Vector3(x*20, -40, y*20));
					squareSettings.setsquareID (squareCount);
					//squareCell.transform.rotation = Vector3(90,0,0);
					text = squareCell.GetComponentsInChildren<TextMesh>();

					if (squareCount == 64) {
						text[0].text = "END";
						squareCell.GetComponent<SpriteRenderer> ().color = Color.green;

					} 
					else {

						text[0].text = (squareCount -1).ToString();
					}
						
					squareCount++;

					squareList.Add (squareCell);

				}
				reverse = false;
				
			} else {
				for (int x = 0; x < 8; x++) {
					TextMesh[] text;
					Transform squareCell;

					List<int> dangerValues = new List<int>(new int[] { 14,15,16,17,18,19,25,26,27,38,39,36,40,41,46,47,45,61,62, 63} );

					if (dangerValues.Contains(squareCount)) {
						squareCell = Instantiate (dangerCell, new Vector3 (x * 20, -40, y * 20), Quaternion.Euler (90, 0, 0));

					} else {
						squareCell = Instantiate (cell, new Vector3 (x * 20, -40, y * 20), Quaternion.Euler (90, 0, 0));
					}
					SquareSetup squareSettings = squareCell.GetComponent<SquareSetup>();
					squareSettings.setCoords (new Vector3(x*20, -40, y*20));
					squareSettings.setsquareID (squareCount);
					//squareCell.transform.rotation = Vector3(90,0,0);
					text = squareCell.GetComponentsInChildren<TextMesh>();

					if (squareCount == 1) {
						squareCell.GetComponent<SpriteRenderer> ().color = Color.green;

						text[0].text = "START";

					} 
					else {

						text[0].text = (squareCount -1).ToString();
					}

					squareCount++;

					squareList.Add (squareCell);

				}
				reverse = true;

			}


		}
		mainGuy1 = Instantiate(mainChar, new Vector3(0, -40, 0), Quaternion.Euler(90, 0, 0));
		mainGuy2 = Instantiate(mainChar2, new Vector3(0, -40, 0), Quaternion.Euler(90, 0, 0));

		CameraFollow.player = mainGuy1;
		TempControllerTester controlScript = mainGuy1.GetComponent<TempControllerTester>();
		controlScript.setNum (2);
		TempControllerTester controlScript2 = mainGuy2.GetComponent<TempControllerTester>();
		controlScript2.setNum (1);
		controlScript2.setSwitch ();

		//controlScript.enabled = true;
		controlScript.setSquare (1,1);
		//controlScript.enabled = false;
		//controlScript2.enabled = true;
		controlScript2.setSquare (1,1);
		//controlScript2.enabled = false;

		controlScript.setIsActive (true);
		controlScript2.setIsActive (false);




		rocket1 = Instantiate(rocket, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController rocketScript = rocket1.GetComponent<rocketController>();
		rocketScript.setSquare (7, 7);
		SquareSetup rocketSquare = squareList[7-1].GetComponent<SquareSetup>();
		squareList[7-1].GetComponent<SpriteRenderer>().color = Color.blue;
		rocketSquare.giveRocket (rocket1);
		List<int> targValues = new List<int>(new int[] { 12} );
		rocketScript.setTargetSquare (targValues);
		List<int> areaValues = new List<int>(new int[] { 7} );
		rocketScript.setArea (areaValues);
		rocketScript.setDist (3.8F);

		rocket2 = Instantiate(rocket, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController rocketScript2 = rocket2.GetComponent<rocketController>();
		rocketScript2.setSquare (31, 31);
		SquareSetup rocketSquare2 = squareList[31-1].GetComponent<SquareSetup>();
		squareList[31-1].GetComponent<SpriteRenderer>().color = Color.blue;
		rocketSquare2.giveRocket (rocket2);
		targValues = new List<int>(new int[] { 41} );
		rocketScript2.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] { 31} );
		rocketScript2.setArea (areaValues);
		rocketScript2.setDist (10F);


		rocket3 = Instantiate(rocket, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController rocketScript3 = rocket3.GetComponent<rocketController>();
		rocketScript3.setSquare (53, 53);
		SquareSetup rocketSquare3 = squareList[53-1].GetComponent<SquareSetup>();
		squareList[53-1].GetComponent<SpriteRenderer>().color = Color.blue;

		rocketSquare3.giveRocket (rocket3);
		targValues = new List<int>(new int[] { 60} );
		rocketScript3.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] { 53} );
		rocketScript3.setArea (areaValues);
		rocketScript3.setDist (3.5F);


		ufo1 = Instantiate(ufo, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController ufoScript = ufo1.GetComponent<rocketController>();
		ufoScript.setSquare (18, 18);
		SquareSetup ufoSquare = squareList[18-1].GetComponent<SquareSetup>();
		ufoSquare.giveRocket (ufo1);
		targValues = new List<int>(new int[] { 2} );
		ufoScript.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] { 14,15,16,17,18,19} );
		ufoScript.setArea (areaValues);
		ufoScript.setDist (3.5F);



		ufo2 = Instantiate(ufo, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController ufoScript2 = ufo2.GetComponent<rocketController>();
		ufoScript2.setSquare (39, 39);
		SquareSetup ufoSquare2 = squareList[39-1].GetComponent<SquareSetup>();
		ufoSquare2.giveRocket (ufo2);
		targValues = new List<int>(new int[] { 11} );
		ufoScript2.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] {25,26,27,38,39,40,41} );
		ufoScript2.setArea (areaValues);
		ufoScript2.setDist (3.5F);


		ufo3 = Instantiate(ufo, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController ufoScript3 = ufo3.GetComponent<rocketController>();
		ufoScript3.setSquare (63, 63);
		SquareSetup ufoSquare3 = squareList[63-1].GetComponent<SquareSetup>();
		ufoSquare3.giveRocket (ufo3);
		targValues = new List<int>(new int[] { 11} );
		ufoScript3.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] {61,62, 63} );
		ufoScript3.setArea (areaValues);
		ufoScript3.setDist (11F);

		ufo4 = Instantiate(ufo, new Vector3(0, -40, 0), Quaternion.Euler(90, -20, 0));
		rocketController ufoScript4 = ufo4.GetComponent<rocketController>();
		ufoScript4.setSquare (47, 47);
		SquareSetup ufoSquare4 = squareList[47-1].GetComponent<SquareSetup>();
		ufoSquare4.giveRocket (ufo4);
		targValues = new List<int>(new int[] { 29} );
		ufoScript4.setTargetSquare (targValues);
		areaValues = new List<int>(new int[] {47, 46,45,36} );
		ufoScript4.setArea (areaValues);
		ufoScript4.setDist (11F);






	}
	void TaskOnClick(){
		diceObj.GetComponent<Renderer>().enabled = true;
		foreach (Renderer r in diceObj.GetComponentsInChildren<Renderer>()){
			r.enabled = true;
		}
		uiObj.SetActive (false);
		uiEnabled = false;

	}
}
