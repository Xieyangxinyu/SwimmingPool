﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; //for lists
using System.Xml; //for the xml files that will be used to store game data
using System.Xml.Serialization;
using System.IO;//for file management

public class XMLManager2 : MonoBehaviour
{
    public GameObject FPSController; //the gameobject that this code is saving xyz position and yz rotation data for
    public GameObject FirstPersonCharacter; //the gameobject that this code is saving x rotation data for
    public static XMLManager2 ins; //this xml manager
    private List<string> datalist; //list of strings which will be saved
    private string nametime;
    private string objname;
    private string[] list;
    private string template;


    void Start()
    {
        ins = this;
        nametime = DateTime.Now.ToString("hh-mm-ss-tt-MM-dd-yyyy");
        datalist = new List<string>();
        objname = FirstPersonCharacter.name;
        list = new string[] { "Time           X-Position     Y-Position     Z-Position     X-Rotation     Y-Rotation     Z-Rotation     " };
        template = "                    ";
    }

    void OnApplicationQuit() //save function that activates when the game is over
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //creates a serializer for the xml file
        FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-" + objname + "-data" + ".xml", FileMode.Create); //creates the xml file as [insert current date, time, and gameobject name].xml in the current file path
        serializer.Serialize(stream, datalist); //puts all the data from "datalist" into the xml file
        stream.Close(); //closes the serializer

        System.IO.File.WriteAllLines(@"C:\Data Collection\"+nametime+"-"+objname+"-data.txt", list);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.S)) //saves data on command, emergency save function
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>)); //creates a serializer for the xml file
            FileStream stream = new FileStream("C:\\Data Collection\\" + nametime + "-" + objname + "-data" + ".xml", FileMode.Create);
            serializer.Serialize(stream, datalist); //puts all the data from "datalist" into "playerdata.xml"
            stream.Close(); //closes the serializer

            System.IO.File.WriteAllLines(@"C:\Data Collection\" + nametime + "-" + objname + "-data.txt", list);
        }
    }


    void LateUpdate()
    {
       Vector3 position = FPSController.transform.position;
       Quaternion rotation = FPSController.transform.rotation;
       Quaternion charrotation = FirstPersonCharacter.transform.rotation;
    
       string realtime = DateTime.Now.ToString("hh:mm:ss tt");
       string px = position.x.ToString();
       string py = position.y.ToString();
       string pz = position.z.ToString();
       string rx = charrotation.eulerAngles.x.ToString();
       string ry = rotation.eulerAngles.y.ToString();
       string rz = rotation.eulerAngles.z.ToString();

       datalist.Add(realtime);
       datalist.Add(px);
       datalist.Add(py);
       datalist.Add(pz);
       datalist.Add(rx);
       datalist.Add(ry);
       datalist.Add(rz);

        string addtolist = "" + realtime + template.Substring(0, (15 - realtime.Length)) + px + template.Substring(0, (15 - px.Length)) + py + template.Substring(0, (15 - py.Length)) + pz + template.Substring(0, (15 - pz.Length)) + rx + template.Substring(0, (15 - rx.Length)) + ry + template.Substring(0, (15 - ry.Length)) + rz + template.Substring(0, (15 - rz.Length));
        Array.Resize(ref list, list.Length + 1);
        list[list.Length - 1] = addtolist;
    }

}


