using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BubbleController : MonoBehaviour {

	//Bx,By and Bz are boundary-control-variables
	public FirstPersonController player;
	//hitEffect is the visual effect after a bubble gets hit
	public ParticleSystem hitEffect;
	public int fishPoint;
	public float LifeTime;
	public float interval;

	void Start(){
		interval = Random.Range (10f, 15f);
	}
    //You can import scripts here to record datas
    //The following function only works when bubbles collide with other things
    //For more information, OnTriggerEnter(Collider hit) can be find in the Unity API
	void Update(){
		if (Time.time > interval + LifeTime)
			positionSwap ();
	}

	void positionSwap(){
		transform.position = randomV(player.Bx,player.Bz);
		LifeTime = Time.time;
	}

    void OnTriggerEnter(Collider hit){
		//if the bubble hits the FirstPerson
		if (hit.tag == "Player") {

			//Initiate visual effect after a bubble gets hit
			hitEffect.transform.position = transform.position;
			player.count += fishPoint;
			positionSwap ();
			//Change the position of this bubble that got hit
			//Please edit this part of scripts
		}
	}
	float min(float a,float b){
		if (a < b)
			return a;
		else
			return b;
	}
	float max(float a,float b){
		if (a > b)
			return a;
		else
			return b;
	}
	public Vector3 randomV(float x,float z){
		Vector3 pos = new Vector3();
		pos.x = Random.Range (0, x);
		pos.y = Random.Range (0f, 40f);
		pos.z = Random.Range (0, z);
		return pos;
	}
}