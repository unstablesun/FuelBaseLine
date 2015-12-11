using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour {

	[SerializeField]
	protected Text consoleLine1;

	[SerializeField]
	protected Text consoleLine2;

	[SerializeField]
	protected Text consoleLine3;

	[SerializeField]
	protected Text consoleLine4;

	[SerializeField]
	protected Text consoleLine5;
	
	[SerializeField]
	protected Text consoleLine6;
	
	[SerializeField]
	protected Text consoleLine7;

	// Use this for initialization
	void Start () {
	
		consoleLine1.text = "Init 1";
		consoleLine2.text = "Init 2";
		consoleLine3.text = "Init 3";
		consoleLine4.text = "Init 4";
		consoleLine5.text = "Init 5";
		consoleLine6.text = "Init 6";
		consoleLine7.text = "Init 7";
	}
	

	public void AddConsoleLine(string line)
	{
		shiftConsoleLinesDown ();

		consoleLine1.text = line;
	}



	private void shiftConsoleLinesDown()
	{
		consoleLine7.text = consoleLine6.text;
		consoleLine6.text = consoleLine5.text;
		consoleLine5.text = consoleLine4.text;
		consoleLine4.text = consoleLine3.text;
		consoleLine3.text = consoleLine2.text;
		consoleLine2.text = consoleLine1.text;
	}

}
