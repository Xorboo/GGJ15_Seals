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

    private bool mRunning;

    public Thread mThread;
    public TcpListener tcp_Listener = null;

    public List<UserKeys> ukeys = new List<UserKeys>();
    void Awake()
    {
        mRunning = true;
        ThreadStart ts = new ThreadStart(Receive);
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
        byte[] tempbuffer = new byte[10000];
        string strResult = "";
        s.Receive(tempbuffer); // received byte array from client
        strResult = Encoding.ASCII.GetString(tempbuffer);
        Debug.Log("Recieved: " + strResult);
        clientNames.Add(num, "Player" + num); //strResult);
        SendColor(s, num);

        while(true) // нехорошо, ну и хрен с ними
        {
            s.Receive(tempbuffer); // received byte array from client
            strResult = Encoding.ASCII.GetString(tempbuffer);
            Debug.Log("Recieved: " + strResult);
            lock (ukeys)
            {
                ukeys[num].ReRead(strResult);
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
            if (tcp_Listener.Pending())
            {
                Socket s = tcp_Listener.AcceptSocket();
                clients.Add(s);
                ukeys.Add(el);
                thread = new Thread(() => WorkWithClient(s,num));
                Debug.Log("Create new thread "+num+"; with socket"+s);
                num++;
            }
            Thread.Sleep(100);
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
}
