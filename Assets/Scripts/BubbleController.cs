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
    private StreamWriter writespawn;
    private StreamWriter writedespawn;
    private string[] spawnlist;
    private string[] despawnlist;
    private string template;

    void Start(){

		interval = UnityEngine.Random.Range (10f, 15f);
        template = "                    ";
        nametime = DateTime.Now.ToString("hh-mm-ss-tt-MM-dd-yyyy");
        datalist = new List<string>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //creates a serializer for the xml file
        FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Create); //creates the initial xml file
        serializer.Serialize(stream, datalist); //puts all the data from "datalist" into the xml file
        stream.Close(); //closes the serializer


        FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Create); //creates the initial xml file
        serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
        stream2.Close(); //closes the serializer

        spawnlist = new string[] { "Time           X-Position     Y-Position     Z-Position     " };
        despawnlist = new string[] { "Time           X-Position     Y-Position     Z-Position     " };

    }
    //You can import scripts here to record datas
    //The following function only works when bubbles collide with other things
    //For more information, OnTriggerEnter(Collider hit) can be find in the Unity API

    void OnApplicationQuit() //save function that activates when the game is over
    {
        System.IO.File.WriteAllLines(@"C:\Data Collection\" + nametime + "-bubble-spawn-data.txt", spawnlist);
        System.IO.File.WriteAllLines(@"C:\Data Collection\" + nametime + "-bubble-despawn-data.txt", despawnlist);
    }

    void Update(){
        if (Time.time > interval + LifeTime)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //retrieves existing despawn data from existing despawn data file
            FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Open);
            datalist = serializer.Deserialize(stream) as List<string>;
            stream.Close();
            string realtime = DateTime.Now.ToString("hh:mm:ss tt");
            string px = transform.position.x.ToString();
            string py = transform.position.y.ToString();
            string pz = transform.position.z.ToString();
            datalist.Add(realtime);
            datalist.Add(px);
            datalist.Add(py);
            datalist.Add(pz);

            string addtodespawnlist = "" + realtime + template.Substring(0,(15-realtime.Length)) + px + template.Substring(0, (15 - px.Length)) + py + template.Substring(0, (15 - py.Length)) + pz + template.Substring(0, (15 - pz.Length));
            Array.Resize(ref despawnlist, despawnlist.Length + 1);
            despawnlist[despawnlist.Length - 1] = addtodespawnlist;

            FileStream stream2 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-despawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
            serializer.Serialize(stream2, datalist); //puts all the data from "datalist" into the xml file
            stream2.Close(); //closes the serializer

            positionSwap();
        }

        if (Input.GetKeyUp(KeyCode.S)) //saves data on command, emergency save function
        {
            System.IO.File.WriteAllLines(@"C:\Data Collection\" + nametime + "-bubble-spawn-data.txt", spawnlist);
            System.IO.File.WriteAllLines(@"C:\Data Collection\" + nametime + "-bubble-despawn-data.txt", despawnlist);
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

            string realtime = DateTime.Now.ToString("hh:mm:ss tt");
            string px = this.gameObject.transform.position.x.ToString();
            string py = this.gameObject.transform.position.y.ToString();
            string pz = this.gameObject.transform.position.z.ToString();
            datalist.Add(realtime);
            datalist.Add(px);
            datalist.Add(py);
            datalist.Add(pz);

            string addtodespawnlist = "" + realtime + template.Substring(0, (15 - realtime.Length)) + px + template.Substring(0, (15 - px.Length)) + py + template.Substring(0, (15 - py.Length)) + pz + template.Substring(0, (15 - pz.Length));
            Array.Resize(ref despawnlist, despawnlist.Length + 1);
            despawnlist[despawnlist.Length - 1] = addtodespawnlist;

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

        string realtime = DateTime.Now.ToString("hh:mm:ss tt");
        string px = pos.x.ToString();
        string py = pos.y.ToString();
        string pz = pos.z.ToString();
        datalist.Add(realtime);
        datalist.Add(px);
        datalist.Add(py);
        datalist.Add(pz);


        string addtospawnlist = "" + realtime + template.Substring(0, (15 - realtime.Length)) + px + template.Substring(0, (15 - px.Length)) + py + template.Substring(0, (15 - py.Length)) + pz + template.Substring(0, (15 - pz.Length));
        Array.Resize(ref spawnlist, spawnlist.Length + 1);
        spawnlist[spawnlist.Length - 1] = addtospawnlist;

        FileStream stream4 = new FileStream("C:\\Data Collection\\" + nametime + "-bubble-spawn-data" + ".xml", FileMode.Create); //creates a new file, overwriting existing data
        serializer.Serialize(stream4, datalist); //puts all the data from "datalist" into the xml file
        stream4.Close(); //closes the serializer

        return pos;


	}
}