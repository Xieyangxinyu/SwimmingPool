using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionControl : MonoBehaviour {

	public float Bx;
	public float By;
	public float Bz;

	// Update Spawnpoints
	void Update () {
		Vector3 pos = new Vector3 ();
		pos.x = Random.Range (1f, Bx - 1);
		pos.y = Random.Range (1f, By - 1);
		pos.z = Random.Range (1f, Bz - 1);
		transform.position = pos;
	}
}
