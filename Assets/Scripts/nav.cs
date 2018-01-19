using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour {

	public GameObject target;
	NavMeshAgent navi;
	// Use this for initialization
	void Start () {
		navi = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		navi.SetDestination (target.transform.position);
	}
}
