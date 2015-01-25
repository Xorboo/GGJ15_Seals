using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Net;

public class Client : MonoBehaviour {
    private int A = 0;
    private int B = 0;
    private int direction = 0;

    Vector3 rotateTop = new Vector3(0,0,0);
    Vector3 rotateLeft = new Vector3(0,0,90);
    Vector3 rotateBot = new Vector3(0,0,180);
    Vector3 rotateRight = new Vector3(0,0,270);

    public static string msg = "";

    private Socket sender;
    public Thread mThread;
    GameObject rotatedJoystick;

    public void ThreadSend(string message)
    {
        Debug.Log(message);
        mThread = new Thread(() => Send(message));
        mThread.Start();
        print("Thread done...");
    }

	// Use this for initialization
    void Start()
    {
        GameObject pColor = GameObject.Find("Player_Color");
        GameObject pName = GameObject.Find("Player_Name");
        pName.GetComponent<Text>().text = "Ururu";
        rotatedJoystick = GameObject.Find("Joy_Rot_Button");
        rotatedJoystick.SetActive(false);
        IPAddress ipAddress = IPAddress.Parse("10.10.8.155");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress,1234);
        sender = new Socket(AddressFamily.InterNetwork, 
            SocketType.Stream, ProtocolType.Tcp );
        sender.Connect(remoteEP);
        Debug.Log(String.Format("Socket connected to {0}",
                sender.RemoteEndPoint.ToString()));
        Debug.Log("Start");
        Send("This is a test");

        byte[] msg = new byte[1000];
        sender.Receive(msg);
        string result = Encoding.ASCII.GetString(msg);
        var strs = result.Split(' ');
        Debug.Log(result);
        pColor.GetComponent<Image>().color = new Color(float.Parse(strs[1]), float.Parse(strs[2]), float.Parse(strs[3]));
	}

    public void ABtnSend()
    {
        A = 1;
        Send();
        A = 0;
    }

    public void BBtnSend()
    {
        B = 1;
        Send();
        B = 0;
    }


    public void TopBtnSend()
    {
        Debug.Log("joy top");
        direction = 1;
        rotatedJoystick.transform.rotation = Quaternion.Euler(rotateTop);
        rotatedJoystick.SetActive(true);
        Send();
    }

    public void JoystickOff()
    {
        direction = 0;
        rotatedJoystick.SetActive(false);
        Send();
        Debug.Log("joy off");
    }

    public void RightBtnSend()
    {
        direction = 2;
        rotatedJoystick.transform.rotation = Quaternion.Euler(rotateRight);
        rotatedJoystick.SetActive(true);
        Send();
    }


    public void DownBtnSend()
    {
        direction = 3;
        rotatedJoystick.SetActive(true);
        rotatedJoystick.transform.rotation = Quaternion.Euler(rotateBot);
        Send();
    }


    public void LeftBtnSend()
    {
        direction = 4;
        rotatedJoystick.transform.rotation = Quaternion.Euler(rotateLeft);
        rotatedJoystick.SetActive(true);
        Send();
    }

    void Send(string message)
    {
            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(message + ",<EOF>");
            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
    }

    void Send()
    {
        string message = direction.ToString() + "," + A.ToString() + "," + B.ToString();
        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes(message + ",<EOF>");
        Debug.Log(message);
        // Send the data through the socket.
        int bytesSent = sender.Send(msg);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }
}
