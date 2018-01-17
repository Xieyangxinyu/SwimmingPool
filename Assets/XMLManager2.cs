using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; //for lists
using System.Xml; //for the xml files that will be used to store game data
using System.Xml.Serialization;
using System.IO;//for file management

public class XMLManager2 : MonoBehaviour
{
    public GameObject FirstPersonCharacter;
    public static XMLManager2 ins;
    private string newtime;
    public List<string> datalist;

    void Start()
    {
        ins = this;
        newtime = DateTime.Now.ToString("hh:mm:ss tt");
        datalist = new List<string>();
    }

    void OnApplicationQuit() //save function
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream("C:\\Users\\Jake\\Desktop\\0_data\\gamedata.xml", FileMode.Create);
        serializer.Serialize(stream, datalist);
        stream.Close();
    }

    public void loadlist() //load function
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream(Application.dataPath + "/gamedata.xml", FileMode.Open);
        datalist = serializer.Deserialize(stream) as List<string>;
        stream.Close();
    }

    void LateUpdate()
    {
        if (newtime.Equals(DateTime.Now.ToString("hh:mm:ss tt")))
        {
            //do nothing
        }

        else
        {
            newtime = DateTime.Now.ToString("hh:mm:ss tt");
            Vector3 position = FirstPersonCharacter.transform.position;
            Quaternion rotation = FirstPersonCharacter.transform.rotation;
            string realtime = DateTime.Now.ToString("hh:mm:ss tt");
            string p = position.ToString();
            string r = rotation.eulerAngles.ToString();
            datalist.Add(realtime);
            datalist.Add(p);
            datalist.Add(r);
        }
    }

}

