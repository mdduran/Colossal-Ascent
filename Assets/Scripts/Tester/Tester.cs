using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputDevice
{
    button,
    limitSwitch,
    slider,
};

public class Tester : MonoBehaviour {
    public byte opcode;
    public byte input;
    public string inputVal;
    public string opcodeVal;
    public Text textBox;
    public InputDevice currDeviceType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Getters

    /**
     *   Getting the opcode from the device
     * 
     * 
     */
     //public byte GetOpCode

    //Setters

    /**
     * 
     *  Setting the opcode for the device
     * 
     */
}
