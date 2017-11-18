using UnityEngine;

public class BubbleManager : MonoBehaviour
{
	public GameObject bubble;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform spawnPoints;         // An array of the spawn points this enemy can spawn from.


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		// Create an instance of the bubble prefab at the randomly selected spawn point's position and rotation.
		Instantiate (bubble, spawnPoints.position, spawnPoints.rotation);
	}
}