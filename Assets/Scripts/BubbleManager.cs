using UnityEngine;

public class BubbleManager : MonoBehaviour
{
	public GameObject bubble;                // The bubble prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform spawnPoints;         // An array of the spawn points this bubble can spawn from.
    public float initBubbles;           //Initial amount of bubbles
	public float Bx;                   //Bx,By and Bz are boundary-control-variables
    public float By;
    public float Bz;

	// You may edit this entire thing to make it a read-file function to import maps
	// More information about spawn can be found on Unity Tutorial Series
    void Start ()
	{
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        for (int i = 0; i < initBubbles; i++)
        {
            Invoke("Spawn", spawnTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Spawn();
    }

    void Spawn ()
	{
        Vector3 pos = new Vector3();
        pos.x = Random.Range(1f, Bx - 1);
        pos.y = Random.Range(1f, By - 1);
        pos.z = Random.Range(1f, Bz - 1);
        // Create an instance of the bubble prefab at the randomly selected spawn point's position and rotation.
        Instantiate (bubble, pos, Quaternion.identity);
	}
}