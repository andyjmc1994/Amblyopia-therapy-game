using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public static Transform player;
	public static Vector3 offset;
	public static int playerNum = 1;

	// Use this for initialization
	void Start () {
		
	}

	void Update () 
	{
		transform.position = new Vector3 (player.position.x + 11, player.position.y + 5, offset.z - 10); // Camera follows the player with specified offset position
	}
}
