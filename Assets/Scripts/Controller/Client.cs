using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Client
{
    public IPEndPoint EndPoint;
    public UdpClient Listener;
    public int Port;
    public int PlayerNum;

    public Queue Events;

    public Thread Self;
    public bool Running;

    public Client(int PlayerNum, IPEndPoint EndPoint, int Port, Queue EventsQueue)
    {
        this.EndPoint = EndPoint;
        Debug.Log(EndPoint + "");
        this.Port = Port;
        this.PlayerNum = PlayerNum;
        this.Events = EventsQueue;

        Running = true;
        Self = new Thread(Run);
        Self.Start();
    }

    public void Stop()
    {
        Running = false;
    }

    public void Run()
    {
        Listener = new UdpClient(Port);

        try
        {
            while (Running)
            {
                byte[] bytes;
                if (Listener.Available > 0)
                {
                    bytes = Listener.Receive(ref EndPoint);
                    // Parse Bytes into event
                    Event e = OperationParser.Recieve(bytes, 0);
                    Debug.Log(e.Action + " " + e.Value);
                }
                else
                    Thread.Sleep(10);

            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        finally
        {
            Listener.Close();
            Listener = null;
        }
    }

    public void Send(byte[] bytes)
    {
        if(Listener != null)
            Listener.Send(bytes, bytes.Length, EndPoint);
    }

}