﻿using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class Serv : MonoBehaviour
{
    public List<Color> playerColors;

    private List<Socket> clients = new List<Socket>();
    public Dictionary<int, String> clientNames = new Dictionary<int, string>();
    public List<UserKeys> ukeys = new List<UserKeys>();
    //private static object _lock = new object();

    private bool mRunning;

    public Thread mThread;
    public TcpListener tcp_Listener = null;

    public char currentWeapon;
    void Awake()
    {
        mRunning = true;
        ThreadStart ts = new ThreadStart(ListenClients);// (Receive);
        mThread = new Thread(ts);
        mThread.Start();
        Debug.Log("Thread done...");
    }

    public void stopListening()
    {
        mRunning = false;
    }
    void Receive()
    {
        tcp_Listener = new TcpListener(IPAddress.Any, 1234);
        tcp_Listener.Start();
        Debug.Log("Server Start");
        while (mRunning)
        {
            // check if new connections are pending, if not, be nice and sleep 100ms
            if (tcp_Listener.Pending())
            {
                Socket s = tcp_Listener.AcceptSocket();
                clients.Add(s);
                byte[] tempbuffer = new byte[10000];
                s.Receive(tempbuffer); // received byte array from client
                string strResult = Encoding.ASCII.GetString(tempbuffer);
                Debug.Log("Recieved: " + strResult);
                SendColor(s, clients.Count - 1);
            }

            foreach (var s in clients)
            {
                
            }
            Thread.Sleep(100);
        }
    }
    
    void WorkWithClient(Socket s, int num)
    {
        Debug.Log("Create new thread " + num + "; with socket" + s);
        byte[] tempbuffer = new byte[10000];
        string strResult = "";
        s.Receive(tempbuffer); // received byte array from client
        strResult = Encoding.ASCII.GetString(tempbuffer);
        Debug.Log("Recieved0: " + strResult);
        clientNames.Add(num, "Player" + num); //strResult);
        //SendColor(s, num);
        
        while(true) // нехорошо, ну и хрен с ними
        {
            s.Receive(tempbuffer); // received byte array from client
            strResult = Encoding.ASCII.GetString(tempbuffer);
            if (strResult != "")
            {
                Debug.Log("Recieved: " + strResult);

                lock (ukeys)
                //lock(((ICollection)ukeys).SyncRoot)
                {
                    ukeys[num].ReRead(strResult);
                }
                strResult = "";
            }
        }
    }
    void ListenClients()
    {
        UserKeys el = new UserKeys("0,0,0");
        tcp_Listener = new TcpListener(IPAddress.Any, 1234);
        tcp_Listener.Start();
        Debug.Log("Server Start");
        int num=0;
        Thread thread;
        while (mRunning)
        {
            /*if (tcp_Listener.Pending())
            {*/
                Socket s = tcp_Listener.AcceptSocket();
                clients.Add(s);
                ukeys.Add(el);
                thread = new Thread(() => WorkWithClient(s,num));
                thread.Start();
                //new Thread(delegate() { WorkWithClient(s, num); }).Start();
                num++;
            //}
            //Thread.Sleep(100);
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

    void SendColor(Socket s, int number)
    {
        Debug.Log("Sending...");
        Color color = number < playerColors.Count ? playerColors[number] : Utils.RandomColor();
        SendText(s, NetUtils.ColorMessage(color));
        Debug.Log("Sent!");
    }

    void SendText(Socket s, string text)
    {
        s.Send(Encoding.ASCII.GetBytes(text));
    }

    public Vector3 MainMoving()
    {
        Vector3 res = new Vector3(0.0f, 0.0f, 0.0f);
        float kx=0.0f, ky=0.0f;
        for(int i=0; i> ukeys.Count; i++)
        {
            res.x += ukeys[i].moveX;
            if(ukeys[i].moveX>0)
                kx += 1.0f;
            res.z += ukeys[i].moveY;
            if(ukeys[i].moveY>0)
                ky += 1.0f;
        }
        res.x /= kx;
        res.y /= ky;
        return res;
    }

    public float ButtonNumChance(int button, float maxTimeout)
    {
        float res = 0.0f;
        float currTime = Time.time;
        float btnTime = 0.0f;
        for (int i = 0; i < ukeys.Count; i++)
        {
            btnTime = (button==1) ? ukeys[i].timeA : ukeys[i].timeB;
            res += (currTime - btnTime < maxTimeout) ? 1 : 0;
        }
        float ucount = ukeys.Count > 0 ? ukeys.Count : 1.0f;
        return Mathf.Pow(res/ucount,4);
    }

}
