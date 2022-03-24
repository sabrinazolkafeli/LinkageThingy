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
    // public GameObject Slider;
    public LineRenderer Line;
    public LineRenderer BoomOneCurve;

    
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
    Vector3 receivedPos5 = Vector3.zero;
    Vector3 size = Vector3.zero;
    Vector3 zeros = Vector3.zero;

    public bool running; //nothing

    private void Update()
    {
        squareone.transform.position = receivedPos1; //assigning receivedPos in SendAndReceiveData()
        squaretwo.transform.position = receivedPos2;
        squarethree.transform.position = receivedPos3;
        squarefour.transform.position = receivedPos4;

        squareone.transform.localScale = size; //assigning receivedPos in SendAndReceiveData()
        squaretwo.transform.localScale = size;
        squarethree.transform.localScale = size;
        squarefour.transform.localScale = size;

        if(!receivedPos4.Equals(zeros)&&!receivedPos3.Equals(zeros))
        {
            Vector3[] LinePositions = {receivedPos3,receivedPos4};
            Line.SetPositions(LinePositions);
        }
        else
        {
            Vector3[] LinePositions = {zeros, zeros};
            Line.SetPositions(LinePositions);
        }

        for (int i=0; i<BoomOnePos.Length; i++)
        {
            BoomOneCurve.SetPositions(BoomOnePos[i], BoomOnePos[i+1]);
        }
       
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
            receivedPos5 = StringToVector3_5(dataReceived);
            size = StringToVector3_size(dataReceived);
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

    public static Vector3 StringToVector3_5(string sVector)
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
            float.Parse(sArray[12]),
            float.Parse(sArray[13]),
            float.Parse(sArray[14]));

        return result;
    }

    public static Vector3 StringToVector3_size(string sVector)
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
            float.Parse(sArray[15]),
            float.Parse(sArray[16]),
            float.Parse(sArray[17]));

        return result;
    }

// // Circle code here
//     public int vertexCount = 40;
//     public float lineWidth = 0.2f;
//     private float radius;
//     private LineRenderer lineRenderer;

// #if UNITY_EDITOR
//     public void OnDrawGizmos()
//     {

//         radius = (float)Math.Sqrt((Math.Pow(receivedPos3[0]-receivedPos4[0],2)+Math.Pow(receivedPos3[1]-receivedPos4[1],2)));

//         float deltaTheta = (2f * Mathf.PI) / vertexCount;
//         float theta = 0f;

//         Vector3 oldPos = Vector3.zero;
//         for (int i = 0; i<vertexCount +1; i++)
//         {
//             Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
//             Gizmos.DrawLine(oldPos, transform.position + pos);
//             transform.position = receivedPos4;
//             oldPos = transform.position + pos;

//             theta += deltaTheta;
//         }
//     }
// #endif

// // Animation here

//     public void Main(string[] args)
//     {
//         CreateArray();
//     }

//     public void CreateArray()
//     {

//         Vector3[] pointsoncircle = new [] { 
//             new Vector3(receivedPos3[0],(receivedPos3[1]+radius) , 0f), 
//             new Vector3((receivedPos3[0]+radius),(receivedPos3[1]) , 0f), 
//             new Vector3(receivedPos3[0],(receivedPos3[1]-radius) , 0f), 
//             new Vector3((receivedPos3[0]-radius),(receivedPos3[1]) , 0f)};      

//     }

// George's Calculation here

    public float DistanceBetweenPoints(Vector3 v1, Vector3 v2)
        {
            float xDiff = (float)Math.Pow((v2[0] - v1[0]),2.0);
            float yDiff = (float)Math.Pow((v2[1] - v1[1]),2.0);

            return (float)Math.Sqrt(xDiff + yDiff);
        }

    
    public void Main(string[] args)
    {
        // defining variable/coordinate
        Vector3 BoomOneStart = receivedPos3;
        Vector3 BoomOneEnd = receivedPos4;
        Vector3 PistonOneStart = receivedPos5;

        float BoomOneStartX = BoomOneStart[0];
        float BoomOneStartY = BoomOneStart[1]; 
        float BoomOneEndX = BoomOneEnd[0];
        float BoomOneEndY = BoomOneEnd[1];
        float PistonOneStartX = PistonOneStart[0];
        float PistonOneStartY = PistonOneStart[1];
        float PistonOneFraction = 0.8f;

        float PistonOneEndX = PistonOneFraction*(BoomOneEndX - BoomOneStartX) + BoomOneStartX;
        float PistonOneEndY = PistonOneFraction*(BoomOneEndY - BoomOneStartY) + BoomOneStartY;

        Vector3 PistonOneEnd = new Vector3(PistonOneEndX, PistonOneEndY, 0f);

        // data from inputs
        float BoomOneLength = DistanceBetweenPoints(BoomOneStart, BoomOneEnd);
        float BoomLengthToPiston = BoomOneLength*PistonOneFraction;
        float BoomOneStartToPistonOneStart = DistanceBetweenPoints(BoomOneStart, PistonOneStart);

        //Piston Constants - finding minimum and maximum length of piston
        const float PistonRatio = 1.8f;
        const float PistonRate = 0.5f;
        bool Max = false; //Assume piston placed in minimum position unless defined as maximum
        float PistonOneMin = DistanceBetweenPoints(PistonOneStart, PistonOneEnd);
        float PistonOneMax = 0f;
            if (Max = false)
            {
                if ((PistonOneMin*PistonRatio)<(BoomOneStartToPistonOneStart+BoomLengthToPiston))
                {
                    PistonOneMax = PistonOneMin*PistonRatio;
                }   
                 else
                {
                    PistonOneMax = BoomOneStartToPistonOneStart+BoomLengthToPiston;
                }
                
            }
            else if (Max = true)
            {
                PistonOneMax = DistanceBetweenPoints(PistonOneStart, PistonOneEnd);
                PistonOneMin = PistonOneMax/PistonRatio;
            }

        //Time Step and Piston Step
        float TimeStep = 1f;
        float PistonStep = PistonRate*TimeStep;
        int ArrayLengthOne = (int)Math.Round((PistonOneMax-PistonOneMin)/PistonStep);
        float[] PistonOneLength = new float [ArrayLengthOne];
            
            for (int i = 0; i<PistonOneLength.Length; i++)
            {
                
                PistonOneLength[i] = PistonOneMin + i*PistonStep;
            }

            float[] Time = new float[ArrayLengthOne];
            for (int i=0; i<Time.Length; i++)
            {
                Time[i] = i*TimeStep;
            }

        //Solve
        //z array (simplifies calcucations)
        float[] z = new float [ArrayLengthOne];
            for (int i=0; i<z.Length; i++)
            {
                z[i] = (((float)Math.Pow(BoomLengthToPiston,2.0) + 
                (float)Math.Pow(BoomOneStartToPistonOneStart,2.0) - 
                (float)Math.Pow(PistonOneLength[i],2.0))/(2*BoomLengthToPiston*BoomOneStartToPistonOneStart));
            } 

        //Theta - angle between Boom 1 and vector BoomOneStart to PistonOneStart
        float[] Theta = new float [ArrayLengthOne];
            for (int i=0; i<Theta.Length; i++)
            {
                if (((PistonOneStartY-BoomOneStartY)/(PistonOneStartX-BoomOneStartX))> ((BoomOneEndY-BoomOneStartY)/(BoomOneEndX-BoomOneStartX)))
                {
                    if (PistonOneStartX < BoomOneStartX)
                    {
                        Theta[i] = (float)Math.Acos(z[i]) + (float)Math.PI; 
                    }
                    else
                    {
                        Theta[i] = -(float)Math.Acos(z[i]);
                    }
                }
                else
                {
                    if (PistonOneStartX < BoomOneStartX)
                    {
                        Theta[i] = -(float)Math.Acos(z[i]) + (float)Math.PI;
                    }
                    else
                    {
                        Theta[i] = (float)Math.Acos(z[i]);
                    }
                }

            }

        //Theta is in radians
        float Alpha = (float)Math.Atan((PistonOneStartY-BoomOneStartY)/(PistonOneStartY-BoomOneStartX));
        float[] PistonOneX = new float [ArrayLengthOne];
        float[] PistonOneY = new float [ArrayLengthOne];
        float[] BoomOneX = new float [ArrayLengthOne];
        float[] BoomOneY = new float [ArrayLengthOne];
        float[] ZerosArray = new float [ArrayLengthOne];
            for (int k=0; k<PistonOneX.Length; k++)
            {
                ZerosArray[k] = 0f;
            }
            for (int j=0; j<PistonOneX.Length; j++)
            {
                PistonOneX[j] = BoomLengthToPiston*((float)Math.Cos(Alpha+Theta[j])) + BoomOneStartX;
                PistonOneY[j] = BoomLengthToPiston*((float)Math.Sin(Alpha+Theta[j])) + BoomOneStartY;
                BoomOneX[j] = (PistonOneX[j] - BoomOneStartX)/PistonOneFraction + BoomOneStartX;
                BoomOneY[j] = (PistonOneY[j] - BoomOneStartY)/PistonOneFraction + BoomOneStartY;
            }

        Vector3[] ConvertToVector3Array (float[] floatX, float[] floatY)
	    {
		    List<Vector3> vector3List = new List<Vector3>();
		    for ( int i = 0; i < floatX.Length; i++)
		    {
		    	vector3List.Add( new Vector3(floatX[i], floatY[i], ZerosArray[i]) );
		    }
		    return vector3List.ToArray();
	    }

        Vector3[] BoomOnePos = ConvertToVector3Array(BoomOneX, BoomOneY);
        Vector3[] PistonOnePos = ConvertToVector3Array(PistonOneX, PistonOneY);


    }
            

}