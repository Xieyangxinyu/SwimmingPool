using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Xml; //for the xml files that will be used to store game data
using System.Xml.Serialization;
using System.IO;//for file management
using System.Collections.Generic; //for lists

public class BubbleManager : MonoBehaviour
{
	public BubbleController bubble;                // The bubble prefab to be spawned.
	public nav shark;
	public FirstPersonController player;
	public int init;
    public int destroyTime; //Set how often the bubbles are going to be destroyed
	public GameObject[] Fishes;
    private string nametime; //used to name the save file
    private List<string> datalist; //list of strings which will be saved

    void Start(){
        nametime = DateTime.Now.ToString("hh-mm-ss-tt-MM-dd-yyyy");
        datalist = new List<string>();
        for (int i = 0; i < init; i++) {
			Spawn (i);
		}
		Instantiate (shark, bubble.randomV(50,50), Quaternion.identity);
        
        
    }


    void Spawn (int fishNumber)
	{
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //retrieves existing spawn data from existing spawn data file
        FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Open);
        datalist = serializer.Deserialize(stream) as List<string>;
        stream.Close();

        // Create an instance of the bubble prefab at the randomly selected spawn point's position and rotation.
        fishNumber %= 10;
		Vector3 pos = bubble.randomV(player.Bx,player.Bz);

        datalist.Add(DateTime.Now.ToString("hh:mm:ss tt"));
        datalist.Add(pos.x.ToString());
        datalist.Add(pos.y.ToString());
        datalist.Add(pos.z.ToString());

        FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
        serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
        stream2.Close(); //closes the serializer

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