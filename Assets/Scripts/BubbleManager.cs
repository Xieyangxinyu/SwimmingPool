using UnityEngine;
using System.Collections;

public class BubbleManager : MonoBehaviour
{
	public GameObject bubble;                // The bubble prefab to be spawned.
    public int initBubbles;           //Initial amount of bubbles
	public float Bx;                   //Bx,By and Bz are boundary-control-variables
    public float By;
    public float Bz;
    public int destroyTime; //Set how often the bubbles are going to be destroyed
	public GameObject[] Fishes;
    private float second = 1; //Seconds
    public float spawnRate = 1; //Used to spawn a bubble based on the update of the seconds

	// You may edit this entire thing to make it a read-file function to import maps
	// More information about spawn can be found on Unity Tutorial Series
    private void Update()
    {
        //Update the seconds
        second = second + Time.deltaTime * 10;
        //Spawn 1 fish per second
        if(second > spawnRate*10)
        {
            Spawn(1);
            spawnRate++;
        }
    }

    //Method that destroyes the bubble after certain time
    IEnumerator Death(GameObject obj)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(obj);
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
        //Set timer for the bubble to die
        StartCoroutine(Death(thisbubble));
    }
}