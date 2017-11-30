using System.Collections;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	public float Bx;
	public float By;
	public float Bz;
	public GameObject count;
	public ParticleSystem hitEffect;
	
	void OnTriggerEnter(Collider hit){
		if (hit.tag == "Player") {
			hitEffect.transform.position = transform.position;
			Vector3 pos = new Vector3 ();
			pos.x = Random.Range (1f, Bx);
			pos.y = Random.Range (1f, By);
			pos.z = Random.Range (1f, Bz);
			count.transform.position = new Vector3 (count.transform.position.x + 1, 0f, 0f);
			transform.position = pos;
		}
	}
}