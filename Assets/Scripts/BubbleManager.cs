using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BubbleManager : MonoBehaviour
{
	public BubbleController bubble;                // The bubble prefab to be spawned.
	public nav shark;
	public FirstPersonController player;
	public int init;
    public int destroyTime; //Set how often the bubbles are going to be destroyed
	public GameObject[] Fishes;
    
	void Start(){
		for (int i = 0; i < init; i++) {
			Spawn (i);
		}
		Instantiate (shark, bubble.randomV(50,50), Quaternion.identity);
	}

    void Spawn (int fishNumber)
	{
        // Create an instance of the bubble prefab at the randomly selected spawn point's position and rotation.
		fishNumber %= 10;
		Vector3 pos = bubble.randomV(player.Bx,player.Bz);
		BubbleController thisbubble = Instantiate (bubble, pos, Quaternion.identity);
		Instantiate (Fishes [fishNumber], pos, Quaternion.identity).transform.SetParent (thisbubble.transform);
		thisbubble.fishPoint = fishNumber + 1;
		//setup lifetime
		thisbubble.LifeTime = Time.time;
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
}