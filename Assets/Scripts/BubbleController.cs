using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; //for lists
using System.Xml; //for the xml files that will be used to store game data
using System.Xml.Serialization;
using System.IO;//for file management
using UnityStandardAssets.Characters.FirstPerson;

public class BubbleController : MonoBehaviour {

	//Bx,By and Bz are boundary-control-variables
	public FirstPersonController player;
	//hitEffect is the visual effect after a bubble gets hit
	public int fishPoint;
	public float LifeTime;
	private float interval;
    private string nametime; //used to name the save file
    private List<string> datalist; //list of strings which will be saved

    void Start(){
		interval = UnityEngine.Random.Range (10f, 15f);
        nametime = DateTime.Now.ToString("hh-mm-ss-tt-MM-dd-yyyy");
        datalist = new List<string>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //creates a serializer for the xml file
        FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Create); //creates the initial xml file
        serializer.Serialize(stream, datalist); //puts all the data from "datalist" into the xml file
        stream.Close(); //closes the serializer


        FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Create); //creates the initial xml file
        serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
        stream2.Close(); //closes the serializer

    }
    //You can import scripts here to record datas
    //The following function only works when bubbles collide with other things
    //For more information, OnTriggerEnter(Collider hit) can be find in the Unity API
	void Update(){
        if (Time.time > interval + LifeTime)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //retrieves existing despawn data from existing despawn data file
            FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Open);
            datalist = serializer.Deserialize(stream) as List<string>;
            stream.Close();

            datalist.Add(DateTime.Now.ToString("hh:mm:ss tt"));
            datalist.Add(transform.position.x.ToString());
            datalist.Add(transform.position.y.ToString());
            datalist.Add(transform.position.z.ToString());

            FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
            serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
            stream2.Close(); //closes the serializer

            positionSwap();
        }
	}

	void positionSwap(){
		transform.position = randomV(player.Bx,player.Bz);
		LifeTime = Time.time;
	}

    void OnTriggerEnter(Collider hit){
		//if the bubble hits the FirstPerson
		if (hit.tag == "Player") {

            XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //retrieves existing despawn data from existing despawn data file
            FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Open);
            datalist = serializer.Deserialize(stream) as List<string>;
            stream.Close();

            datalist.Add(DateTime.Now.ToString("hh:mm:ss tt"));
            datalist.Add(this.gameObject.transform.position.x.ToString());
            datalist.Add(this.gameObject.transform.position.y.ToString());
            datalist.Add(this.gameObject.transform.position.z.ToString());

            FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
            serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
            stream2.Close(); //closes the serializer

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
		pos.x = UnityEngine.Random.Range (0, x);
		pos.y = UnityEngine.Random.Range (0f, 40f);
		pos.z = UnityEngine.Random.Range (0, z);

        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream3 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Open);
        datalist = serializer.Deserialize(stream3) as List<string>;
        stream3.Close();

        datalist.Add(DateTime.Now.ToString("hh:mm:ss tt"));
        datalist.Add(pos.x.ToString());
        datalist.Add(pos.y.ToString());
        datalist.Add(pos.z.ToString());


        FileStream stream4 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
        serializer.Serialize(stream4, datalist); //puts all the data from "datalist" into the xml file
        stream4.Close(); //closes the serializer

        return pos;


	}
}