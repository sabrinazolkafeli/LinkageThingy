using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

[RequireComponent(typeof(LineRenderer))]

public class Portlistener : MonoBehaviour
{
    public GameObject squareone;
    public GameObject squaretwo;
    public GameObject squarethree;    
    public GameObject squarefour;
    public GameObject Circle;
    public LineRenderer Line;
    
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 receivedPos1 = Vector3.zero;
    Vector3 receivedPos2 = Vector3.zero;
    Vector3 receivedPos3 = Vector3.zero;
    Vector3 receivedPos4 = Vector3.zero;

    public bool running; //nothing

    private void Update()
    {
        squareone.transform.position = receivedPos1; //assigning receivedPos in SendAndReceiveData()
        squaretwo.transform.position = receivedPos2;
        squarethree.transform.position = receivedPos3;
        squarefour.transform.position = receivedPos4;

        Vector3[] positions = {receivedPos3, receivedPos4};
        Line.SetPositions(positions);
       
    }

    public void Start() //private
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    public void GetInfo() //nothing
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }

    public void SendAndReceiveData() //nothing
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {
            //---Using received data---
            receivedPos1 = StringToVector3_1(dataReceived); //<-- assigning receivedPos value from Python
            receivedPos2 = StringToVector3_2(dataReceived);
            receivedPos3 = StringToVector3_3(dataReceived);
            receivedPos4 = StringToVector3_4(dataReceived);
            print("received position one data, and moved the Cube!");
            
            

            //---Sending Data to Host----
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Hey I got your message Python! Do You see this massage?"); //Converting string to byte data
            nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
    }

    public static Vector3 StringToVector3_1(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public static Vector3 StringToVector3_2(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[3]),
            float.Parse(sArray[4]),
            float.Parse(sArray[5]));

        return result;
    }

    public static Vector3 StringToVector3_3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[6]),
            float.Parse(sArray[7]),
            float.Parse(sArray[8]));

        return result;
    }
    public static Vector3 StringToVector3_4(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[9]),
            float.Parse(sArray[10]),
            float.Parse(sArray[11]));

        return result;
    }

// Circle code here
    public int vertexCount = 40;
    public float lineWidth = 0.2f;
    public bool circleFillscreen;
    // public float radius;
    private float radius;
    private LineRenderer lineRenderer;


    // private void Awake()
    // {
    //     lineRenderer= GetComponent<LineRenderer>();
    //     SetupCircle();
    // }

    // public void SetupCircle()
    // {
    //     lineRenderer.widthMultiplier = lineWidth;

    //     if (circleFillscreen)
    //     {
    //         radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
    //             Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - lineWidth;

    //     }

    //     float deltaTheta = (2f * Mathf.PI) / vertexCount;
    //     float theta = 0f;

    //     lineRenderer.positionCount = vertexCount;
    //     for (int i = 0; i < lineRenderer.positionCount; i++)
    //     {
    //         Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
    //         lineRenderer.SetPosition(i, pos);
    //         theta += deltaTheta;
    //     }

    // }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        radius = (float)Math.Sqrt((Math.Pow(receivedPos3[0]-receivedPos4[0],2)+Math.Pow(receivedPos3[1]-receivedPos4[1],2)));

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;
        for (int i = 0; i<vertexCount +1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            transform.position = receivedPos4;
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }
    }
#endif

}