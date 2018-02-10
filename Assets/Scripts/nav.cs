using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour {

	public GameObject target;
	public float rotationSpeed;
	private float moveSpeed;
	public Animation anim;
	private Vector3 targetR;
	private int WTime;
	private Vector3 pos;
	public GameObject count;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		moveSpeed = 1f;
		pos = new Vector3();
		pos.x = Random.Range(-50, 50);
		pos.y = Random.Range(1f, 40);
		pos.z = Random.Range(-50, 50);
	}

	// Update is called once per frame
	void Update () {
		targetR = target.transform.position - transform.position;
		float temp = targetR.x * targetR.x + targetR.y * targetR.y + targetR.z * targetR.z;
		if (temp < 15) {
			anim.CrossFade ("eat");
		}
		else if(temp < 500){
			moveSpeed = 2f;
			anim.CrossFade ("fastswim");
		}
		else {
			targetR = pos - transform.position;
			moveSpeed = 1f;
			anim.CrossFade ("swim");
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetR), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * Time.deltaTime * moveSpeed;
	}
	void OnTriggerEnter(Collider hit){
		//if the bubble hits the FirstPerson
		if (hit.tag == "Player") {
			hit.transform.position = new Vector3(10f, 10f, 10f);
			count.transform.position = new Vector3 (0f, 0f, 0f);
		}
		if (hit.tag == "shark") {
			moveSpeed = 10f;
			transform.position -= transform.forward * Time.deltaTime * moveSpeed;
		}
	}
}
