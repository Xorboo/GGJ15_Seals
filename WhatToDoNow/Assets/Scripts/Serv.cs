using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;

public class Serv : MonoBehaviour
{
    private bool mRunning;
    public static string msg = "";

    public Thread mThread;
    public TcpListener tcp_Listener = null;

    void Awake()
    {
        mRunning = true;
        ThreadStart ts = new ThreadStart(Receive);
        mThread = new Thread(ts);
        mThread.Start();
        print("Thread done...");
    }

    public void stopListening()
    {
        mRunning = false;
    }

    void Receive()
    {
        tcp_Listener = new TcpListener(IPAddress.Any, 1234);
        tcp_Listener.Start();
        print("Server Start");
        while (mRunning)
        {
            // check if new connections are pending, if not, be nice and sleep 100ms
            if (!tcp_Listener.Pending())
            {
                Thread.Sleep(100);
            }
            else
            {
                Socket ss = tcp_Listener.AcceptSocket();
                byte[] tempbuffer = new byte[10000];
                ss.Receive(tempbuffer); // received byte array from client
                string strResult = Encoding.ASCII.GetString(tempbuffer);
                print(strResult);
            }
        }
    }

    void Update()
    {

    }

    void OnApplicationQuit()
    { // stop listening thread
        stopListening(); // wait fpr listening thread to terminate (max. 500ms)
        mThread.Join(500);
    }
}
