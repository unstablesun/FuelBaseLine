using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKSimpleJSON;

public class FuelOBJ : MonoBehaviour 
{

	private bool m_initialized;
	private FuelListener m_listener;

	private void Awake ()
	{
		if (!m_initialized) {
			GameObject.DontDestroyOnLoad (gameObject);
			
			if (!Application.isEditor) {
				// Initialize the Fuel SDK listener
				// reference for later use by the launch
				// methods.
				m_listener = new FuelListener (this);
				FuelSDK.setListener (m_listener);
			}
			m_initialized = true;
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	

	public void DebugConsole (string s) {
		
		getConsoleClass().AddConsoleLine (s);
	}

	public void SyncCC () {

		DebugConsole ("Sync CC");
	
		FuelSDK.SyncChallengeCounts ();
	}

	public void SyncTI () {
		
		DebugConsole ("Sync TI");
		
		FuelSDK.SyncTournamentInfo ();
	}

	public void SyncVG () {
		
		DebugConsole ("Sync VG");

		FuelSDK.SyncVirtualGoods ();
	}

	public void LaunchFuelWithScore (long score)
	{
		// Construct the match results dictionary using the cached match
		// data obtained from the SdkCompletedWithMatch() callback.
		Dictionary<string, object> matchResult = new Dictionary<string, object> ();
		matchResult.Add ("tournamentID", m_listener.MatchID);
		matchResult.Add ("matchID", m_listener.TournamentID);
		
		// The raw score will be used to compare results
		// between match players. This should be a positive
		// integer value.
		matchResult.Add ("score", score);
		
		// Specify a visual score to represent the raw score
		// in a different format in the UI. If no visual score
		// is provided then the raw score will be used.
		matchResult.Add ("visualScore", score.ToString ());
		
		// Post the match results to the Fuel SDK.
		FuelSDK.SubmitMatchResult (matchResult);


		UnityEngine.Debug.Log ("LaunchFuelWithScore : Launch");
		// Launches the Fuel SDK online experience.
		FuelSDK.Launch ();
	}


	public void LaunchFuel ()
	{
		UnityEngine.Debug.Log ("LaunchFuel (exit)");
		// Launches the Fuel SDK online experience.
		FuelSDK.Launch ();
	}

	public void LaunchFuelWin ()
	{
		UnityEngine.Debug.Log ("LaunchFuelWin");
		m_listener.GameScore = 100;
		DebugConsole ("Score = 100");

	}

	public void LaunchFuelLose ()
	{
		UnityEngine.Debug.Log ("LaunchFuelLose");
		m_listener.GameScore = 50;
		DebugConsole ("Score = 50");
	}



	public void GetEvents() {
		List<object> tags = new List<object>();
		tags.Add("blitzMode");
		bool success = FuelSDK.GetEvents(null);
		if(success == true) {
			//Everything is good you can expect your data in the event callback

			DebugConsole ("GetEvents - success");

		}
	}








	public void SyncUserValues()
	{
		bool syncResult = FuelSDK.SyncUserValues();
		if(syncResult == true) {
			// Successful call to the server
		} else {
			// There was a problem contacting the server
		}
	}

	
	private Console getConsoleClass()
	{
		GameObject _consoleObject = GameObject.Find("Console");
		if (_consoleObject != null) 
		{
			Console _consoleScript = _consoleObject.GetComponent<Console> ();
			if(_consoleScript != null) 
			{
				return _consoleScript;
			}
			Debug.Log("_consoleScript == null");
			throw new Exception();
		}
		Debug.Log("_consoleObject = null");
		throw new Exception();
	}

	
}
