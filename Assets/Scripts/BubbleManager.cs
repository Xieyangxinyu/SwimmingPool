using UnityEngine;

public class BubbleManager : MonoBehaviour
{
	public GameObject bubble;                // The bubble prefab to be spawned.
    public int initBubbles;           //Initial amount of bubbles
	public float Bx;                   //Bx,By and Bz are boundary-control-variables
    public float By;
    public float Bz;
	public GameObject[] Fishes;

	// You may edit this entire thing to make it a read-file function to import maps
	// More information about spawn can be found on Unity Tutorial Series
    void Start ()
	{
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        for (int i = 0; i < initBubbles; i++)
        {
			Spawn (i);
        }
    }

	void Spawn (int fishNumber)
	{
        Vector3 pos = new Vector3();
        pos.x = Random.Range(1f, Bx - 1);
        pos.y = Random.Range(1f, By - 1);
        pos.z = Random.Range(1f, Bz - 1);
        // Create an instance of the bubble prefab at the randomly selected spawn point's position and rotation.
        GameObject thisbubble = Instantiate (bubble, pos, Quaternion.identity);
		Instantiate (Fishes [fishNumber], pos, Quaternion.identity).transform.SetParent (thisbubble.transform);
	}
}