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
    public int Port;
    public int PlayerNum;

    public Queue Events;

    public Thread Self;
    public bool Running;

    public Client(int PlayerNum, IPEndPoint EndPoint, int Port, Queue EventsQueue)
    {
        this.EndPoint = EndPoint;
        this.Port = Port;
        this.PlayerNum = PlayerNum;
        this.Events = EventsQueue;

        Running = true;
        Self = new Thread(Run);
    }

    public void Run()
    {
        UdpClient listener = new UdpClient(Port);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, Port);

        try
        {
            while (Running)
            {
                groupEP = new IPEndPoint(IPAddress.Any, Port);
                byte[] bytes;
                if (listener.Available > 0)
                {
                    bytes = listener.Receive(ref groupEP);
                    // Parse Bytes into event

                }
                else
                    Thread.Sleep(50);

            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        finally
        {
            listener.Close();
        }
    }

    public void Send(byte[] bytes)
    {
    }

}