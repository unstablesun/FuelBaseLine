using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKSimpleJSON;

public class FuelListener : FuelSDKListener 
{
	private FuelOBJ m_fuelObj;


	public string TournamentID { get; set; }
	public string MatchID { get; set; }

	public int GameScore { get; set; }

	public int EquationType { get; set; }
	public float GameTime { get; set; }
	public int Progression { get; set; }


	public FuelListener(FuelOBJ fuelOBJ) {
		m_fuelObj = fuelOBJ;
	}

	public override void OnCompeteUICompletedWithExit ()
	{
		// Sdk completed gracefully with no further action.
		// The game should handle this event by returning the
		// player to the game’s main scene.        
	}


	public override void OnCompeteUICompletedWithMatch (Dictionary<string, object> matchInfo)
	{
		// Sdk completed with a match.
		UnityEngine.Debug.Log ("OnCompeteUICompletedWithMatch : matchInfo");

		// Extract the match information and cache if for
		// later when posting the match score.
		TournamentID = matchInfo ["tournamentID"].ToString();
		MatchID = matchInfo ["matchID"].ToString();
		
		// Extract the params data.
		string paramsJSON = matchInfo ["params"].ToString();
		JSONNode json = JSONNode.Parse (paramsJSON);
		
		// Extract the match seed value to be used for any
		// randomization seeding. The seed value will be
		// the same for each match player.
		long seed = 0;
		
		// Must parse long values manually since SimpleJSON
		// doesn't yet provide this function automatically.
		if (!long.TryParse(json ["seed"], out seed))
		{
			// invalid string encoded long value, defaults to 0
		}
		
		// Extract the match round value.
		int round = json ["round"].AsInt;
		
		// Extract the ads allowed flag to be used to
		// determine if in-game ads should be allowed in
		// this match.
		bool adsAllowed = json ["adsAllowed"].AsBool;
		
		// Extract the fair play flag to be used to
		// determine if a level playing field between the
		// match players should be enforced.
		bool fairPlay = json ["fairPlay"].AsBool;
		
		// Extract the options data.
		JSONClass options = json ["options"].AsObject;
		
		// Extract the player's public profile data.
		JSONClass you = json ["you"].AsObject;
		string yourNickname = you ["name"];
		string yourAvatarURL = you ["avatar"];
		
		// Extract the opponent's public profile data.
		JSONClass them = json ["them"].AsObject;
		string theirNickname = them ["name"];
		string theirAvatarURL = them ["avatar"];
		
		// Play the game and pass any extracted match
		// data as necessary.
		//startMultiplayerGame();

		m_fuelObj.LaunchFuelWithScore(GameScore);

	}


	public override void OnCompeteChallengeCount (int count)
	{
		UnityEngine.Debug.Log ("OnCompeteChallengeCount");

		if (count > 0) {
			// Show a counter in the UI to represent the
			// number of open challenges waiting to be
			// played.

			m_fuelObj.DebugConsole("Count = " + count);

		} else {
			// Hide the counter.
			m_fuelObj.DebugConsole("Count = " + count);

		}
	}




    public override void OnCompeteTournamentInfo (Dictionary<string, string> tournamentInfo)
    {
        if ((tournamentInfo == null) || (tournamentInfo.Count == 0)) {
            // There is no tournament currently running or scheduled.
            // Display a regular multiplayer button.
			m_fuelObj.DebugConsole("no tournament info");
		} else {
            // A tournament is currently running or is the
            // information for the next scheduled tournament.

            // Extract the tournament data.
            string name = tournamentInfo["name"];
            string campaignName = tournamentInfo["campaignName"];
            string sponsorName = tournamentInfo["sponsorName"];
            string startDate = tournamentInfo["startDate"];
            string endDate = tournamentInfo["endDate"];
            string logo = tournamentInfo["logo"];

            // Display a tournament multiplayer button.

			m_fuelObj.DebugConsole("tournament info");
			m_fuelObj.DebugConsole(name);
			m_fuelObj.DebugConsole(campaignName);
			m_fuelObj.DebugConsole(sponsorName);
			m_fuelObj.DebugConsole(startDate);
			m_fuelObj.DebugConsole(endDate);
			m_fuelObj.DebugConsole(logo);

		}
    }

	public override void OnVirtualGoodList (string transactionID, List<object> virtualGoods)
	{
		bool inventoryUpdated = true;
		
		// Update the player's local (and/or remote) virtual goods inventory.

		m_fuelObj.DebugConsole(transactionID);

		
		foreach (string value in virtualGoods)
		{
			m_fuelObj.DebugConsole(value);
		}

		FuelSDK.AcknowledgeVirtualGoods(transactionID, inventoryUpdated);

		
		if (inventoryUpdated) {
			// Notify the user of awarded virtual goods.
			
			// If an unknown or unsupported virtual good ID is encountered,
			// then prompt the user to update their game to the latest
			// version in order to use that virtual good.
		} else {
			// Undo update to the player's local (and/or remote) virtual
			// goods inventory.
		}
	}










	
	
	public void GetLeaderBoard(string Id) {
		bool success = FuelSDK.GetLeaderBoard( Id );
		if(success == true) {
			//Everything is good you can expect your data in the event callback
		}
	}
	
	public void GetMission(string Id) {
		bool success = FuelSDK.GetMission( Id );
		if(success == true) {
			//Everything is good you can expect your data in the event callback
		}
	}
	
	public void GetQuest(string Id) {
		bool success = FuelSDK.GetQuest( Id );
		if(success == true) {
			//Everything is good you can expect your data in the event callback
		}
	}
	
	private enum EventType
	{
		leaderboard = 0,
		mission = 1,
		quest = 2
	};

	public override void OnIgniteEvents (List<object> events)
	{
		m_fuelObj.DebugConsole("OnIgniteEvents");
		if (events == null) {
			Debug.Log ("OnIgniteEvents - undefined list of events");
			return;
		}
		
		if (events.Count == 0) {
			Debug.Log ("OnIgniteEvents - empty list of events");
			return;
		}
		
		foreach (object eventObject in events) {
			Dictionary<string, object> eventInfo = eventObject as Dictionary<string, object>;
			
			if (eventInfo == null) {
				Debug.Log ("OnIgniteEvents - invalid event data type: " + eventObject.GetType ().Name);
				continue;
			}
			
			object eventIdObject = eventInfo["id"];
			
			if (eventIdObject == null) {
				Debug.Log ("OnIgniteEvents - missing expected event ID");
				continue;
			}
			
			if (!(eventIdObject is string)) {
				Debug.Log ("OnIgniteEvents - invalid event ID data type: " + eventIdObject.GetType ().Name);
				continue;
			}
			
			string eventId = (string)eventIdObject;
			
			object eventTypeObject = eventInfo["type"];
			
			if (eventTypeObject == null) {
				Debug.Log ("OnIgniteEvents - missing expected event type");
				continue;
			}
			
			if (!(eventTypeObject is long)) {
				Debug.Log ("OnIgniteEvents - invalid event type data type: " + eventTypeObject.GetType ().Name);
				continue;
			}
			
			long eventTypeLong = (long)eventTypeObject;
			
			int eventTypeValue = (int)eventTypeLong;
			
			if (!Enum.IsDefined (typeof (EventType), eventTypeValue)) {
				Debug.Log ("OnIgniteEvents - unsupported event type value: " + eventTypeValue.ToString ());
				continue;
			}
			
			EventType eventType = (EventType)eventTypeValue;
			
			object eventJoinedObject = eventInfo["joined"];
			
			if (eventJoinedObject == null) {
				Debug.Log ("OnIgniteEvents - missing expected event joined status");
				continue;
			}
			
			if (!(eventJoinedObject is bool)) {
				Debug.Log ("OnIgniteEvents - invalid event joined data type: " + eventJoinedObject.GetType ().Name);
				continue;
			}
			
			bool eventJoined = (bool)eventJoinedObject;
			
			string eventTypeString = eventType.ToString ();
			
			if (eventJoined) {
				Debug.Log ("OnIgniteEvents - player is joined in event of type '" + eventTypeString + "' with event ID: " + eventId);
				
				switch (eventType) {
				case EventType.leaderboard:
					m_fuelObj.DebugConsole(eventId);
					break;
				case EventType.mission:
					m_fuelObj.DebugConsole(eventId);
					break;
				case EventType.quest:
					m_fuelObj.DebugConsole(eventId);
					break;
				default:
					Debug.Log ("OnIgniteEvents - unsupported event type: " + eventTypeString);
					continue;
				}
			} else {
				Debug.Log ("OnIgniteEvents - player can opt-in to join event of type '" + eventTypeString + "' with event ID: " + eventId);
				m_fuelObj.DebugConsole(eventId);
			}
		}

	}

	public override void OnIgniteLeaderBoard (Dictionary<string, object> leaderBoard)
	{
	}
	
	public override void OnIgniteMission (Dictionary<string, object> mission)
	{
		if( mission.ContainsKey("id") ) {
		}
		if( mission.ContainsKey("progress") ){
		}
		if( mission.ContainsKey( "metadata" ) ) {
		}
		/*
		if( mission.ContainsKey("rules") ) {
			missionData.Rules = new Dictionary<string, RuleData>();
			List<object> rulesList = missionDict["rules"] as List<object>;
			foreach(object rule in rulesList ) {
				Dictionary<string,object> ruleDict = rule as Dictionary<string, object>;
				
				if( ruleDict.ContainsKey("id") ) {
					string Id = Convert.ToString( ruleDict["id"] );
				}
				if( ruleDict.ContainsKey("score") ) {
					string Score = Convert.ToInt32( ruleDict["score"] );
				}
				if( ruleDict.ContainsKey("target") ) {
					int Target = Convert.ToInt32( ruleDict["target"] );
				}
				if( ruleDict.ContainsKey("achieved") ) {
					bool Achieved = Convert.ToBoolean( ruleDict["achieved"] );
				}
				if( ruleDict.ContainsKey("variable") ) {
					string Variable = Convert.ToString( ruleDict["variable"] );
				}
				if( ruleDict.ContainsKey("kind") ) {
					string Kind = Convert.ToString( ruleDict["kind"] );
				}
				if( ruleDict.ContainsKey( "metadata" ) ) {
					string metadataString = Convert.ToString( ruleDict["metadata"] );
				}
			}
		}
		*/
	}
	
	public override void OnIgniteQuest (Dictionary<string, object> quest)
	{
	}
	
	public override void OnIgniteJoinEvent (string eventID, bool joinStatus)
	{
	}


	public override void OnUserValues (Dictionary<string, string> conditions, Dictionary<string, string> variables)
	{
		String _equationType = "equationType";
		String _gameTime = "gameTime";
		String _progression = "progression";

		string value;
		if (variables.TryGetValue (_equationType, out value)) {
			EquationType = int.Parse (value.ToString ());
			m_fuelObj.DebugConsole("equationType = " + EquationType.ToString());

		} else {
			Debug.Log("EquationType not found in userValueInfo");
		}

		if (variables.TryGetValue (_gameTime, out value)) {
			GameTime = float.Parse(value.ToString());
			m_fuelObj.DebugConsole("gameTime = " + GameTime.ToString());
		} else {
			Debug.Log("GameTime not found in userValueInfo");
		}

		if (variables.TryGetValue (_progression, out value)) {
			Progression = int.Parse(value.ToString());
			m_fuelObj.DebugConsole("progression = " + Progression.ToString());
		} else {
			Debug.Log("Progression not found in userValueInfo");
		}

	}

	public override void OnSocialLogin (bool allowCache)
	{
		//m_fuelObj.OnSocialLogin ();
	}

	
}
