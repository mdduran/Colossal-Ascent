using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ByteColor
{
    public byte red;
    public byte green;
    public byte blue;
};

public class OperationParser {
    public byte[] controllerData;
    public int port;
    

    // TODO: Send the event to the game manager
    public static Event Recieve(byte[] data, int playerNumber)
    {
        //parse the data
        OpCode operation = (OpCode)data[0];
        Event newEvent;
        switch (operation)
        {
            case OpCode.Connect:
                newEvent = new Event(OpCode.Connect, 0, playerNumber);
                break;
            case OpCode.Disconnect:
                newEvent = new Event(OpCode.Disconnect, 0, playerNumber);
                break;
            case OpCode.StripPot:
                newEvent = new Event(OpCode.StripPot, data[1], playerNumber);
                break;
            case OpCode.RotPot:
                newEvent = new Event(OpCode.RotPot, data[1], playerNumber);
                break;
            case OpCode.ExitButton:
                newEvent = new Event(OpCode.ExitButton, data[1], playerNumber);
                break;
            case OpCode.StartButton:
                newEvent = new Event(OpCode.StartButton, data[1], playerNumber);
                break;
            case OpCode.FireButton:
                newEvent = new Event(OpCode.FireButton, data[1], playerNumber);
                break;
            case OpCode.JumpButton:
                newEvent = new Event(OpCode.JumpButton, data[1], playerNumber);
                break;
            case OpCode.KeepAlive:
                newEvent = new Event(OpCode.KeepAlive, 0, playerNumber);
                break;
            default:
                newEvent = new Event(OpCode.Error, 0, playerNumber);
                break;
        }
        return newEvent;
    }

    public static byte[] Broadcast(int port)
    {
        //Add the opcode with the port
        byte[] data = new byte[5];
        data[0] = (byte)OpCode.Broadcast;
        byte[] portBytes = BitConverter.GetBytes(port);
        Array.Copy(portBytes, 0, data, 1, 4);
        return data;
    }

    /*SimpleSend*/
    public static byte[] SimpleSend(OpCode currentOp)
    {
        byte[] data = new byte[1];
        data[0] = (byte)currentOp;
        return data;
    }


    /*Set LED Strip*/
    public static byte[] SetLEDStrip(byte startPixel, byte endPixel, ByteColor[] colors)
    {
        int numLights = colors.Length;
        int arraySize = 3 + (numLights * 3);
        byte[] data = new byte[arraySize];
        byte[] colorArray = ColorsToByteArray(colors);
        data[0] = (byte)OpCode.SetLEDStrip;
        Array.Copy(colorArray, 0, data, 1, data.Length);

        return data;

    }

    public static byte[] ColorsToByteArray(ByteColor[] colors)
    {
        byte[] data = new byte[colors.Length * 3];
        for (int i = 0; i < colors.Length; i++)
        {
            for (int j = 0; j < data.Length; j += 3)
            {
                data[j] = colors[i].red;
                data[j++] = colors[i].blue;
                data[j + 2] = colors[i].green;
            }

        }

        return data;
    }

    /*Set LED Pixel*/
    public static byte[] SetLEDPixel(byte pixelIndex, ByteColor newColor)
    {
        byte[] data = new byte[4];
        data[0] = pixelIndex;
        data[1] = newColor.red;
        data[2] = newColor.green;
        data[3] = newColor.blue;
        return data;
    }

    
	
}
