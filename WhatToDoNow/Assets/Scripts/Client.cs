using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Net;

public class Client : MonoBehaviour {

    private bool mRunning;
    public static string msg = "";

    public Thread mThread;

    public void ThreadSend(string message)
    {
        Debug.Log(message);
       // mRunning = true;
       // mThread = new Thread(() => Send(message));
       // mThread.Start();
       // print("Thread done...");
    }

	// Use this for initialization
    void Awake()
    {
        ThreadSend("This is a test");
	}

    public void ABtnSend()
    {
        ThreadSend("A");
    }

    public void BBtnSend()
    {
        ThreadSend("B");
    }


    public void TopBtnSend()
    {
        ThreadSend("1");
    }


    public void RightBtnSend()
    {
        ThreadSend("2");
    }


    public void DownBtnSend()
    {
        ThreadSend("3");
    }


    public void LeftBtnSend()
    {
        ThreadSend("4");
    }

    void Send(string message)
    {
        IPAddress ipAddress = IPAddress.Parse("10.10.8.39");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress,1234);

        Socket sender = new Socket(AddressFamily.InterNetwork, 
            SocketType.Stream, ProtocolType.Tcp );

        try {
            sender.Connect(remoteEP);

            Debug.Log(String.Format("Socket connected to {0}",
                sender.RemoteEndPoint.ToString()));

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);

            //byte[] bytes = new byte[10000];
            // Receive the response from the remote device.
            //int bytesRec = sender.Receive(bytes);
            //Debug.Log(String.Format("Echoed test = {0}",
            //    Encoding.ASCII.GetString(bytes,0,bytesRec)));

            // Release the socket.
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
                
        } catch (ArgumentNullException ane) {
            Debug.Log(String.Format("ArgumentNullException : {0}",ane.Message));
        } catch (SocketException se) {
            Debug.Log(String.Format("SocketException : {0}",se.Message));
        } catch (Exception e) {
            Debug.Log(String.Format("Unexpected exception : {0}", e.Message));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        mThread.Join(500);
    }
}
