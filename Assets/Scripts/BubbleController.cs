using System.Collections;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	//Bx,By and Bz are boundary-control-variables
	public float Bx;
	public float By;
	public float Bz;
	//count GameObject records the number of bubbles caught in its Position x
	//The reason why I record bubble numbers by a GameObject is that this info might need to be imported by various scripts
	public GameObject count;
	//hitEffect is the visual effect after a bubble gets hit
	public ParticleSystem hitEffect;

	//You can import scripts here to record datas
	//The following function only works when bubbles collide with other things
	//For more information, OnTriggerEnter(Collider hit) can be find in the Unity API
	void OnTriggerEnter(Collider hit){
		//if the bubble hits the FirstPerson
		if (hit.tag == "Player") {
			//Initiate visual effect after a bubble gets hit
			hitEffect.transform.position = transform.position;

			//Change the position of this bubble that got hit
			//Please edit this part of scripts
			Vector3 pos = new Vector3 ();
			pos.x = Random.Range (1f, Bx);
			pos.y = Random.Range (1f, By);
			pos.z = Random.Range (1f, Bz);
			count.transform.position = new Vector3 (count.transform.position.x + 1, 0f, 0f);
			transform.position = pos;
		}
	}
}