using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class FuelDebugOverride : MonoBehaviour
{
	public enum Enviro
	{
		eProduction,
		eSandbox,
		eInternal,
		eDevelopment
	};
	
	private static bool m_bInitialized;
	
	private static Enviro mCurrentEnviro = Enviro.eProduction;
	private static string mEnviro;
	private static string mGameKey;
	private static string mGameSecret;
	private static string mDevBox;
	
	
	#if UNITY_IPHONE
	//[DllImport ("__Internal")]
	//public static extern void iOSSetDebugEnironment(string SdkUrl, string GrantooHost, string TournamentHost, string ChallengeHost, string CdnHost, string TransactionHost, string DynamicsHost);
	#endif
	
	static FuelDebugOverride ()
	{
		m_bInitialized = false;
	}
	
	private static void SetEnviro (string enviro)
	{
		if(enviro == "production")
			mCurrentEnviro = Enviro.eProduction;
		else if(enviro == "sandbox")
			mCurrentEnviro = Enviro.eSandbox;
		else if(enviro == "internal")
			mCurrentEnviro = Enviro.eInternal;
		else if(enviro == "development")
			mCurrentEnviro = Enviro.eDevelopment;
	}
	
	public static string GetGameKey ()
	{
		return mGameKey;
	}
	
	public static string GetGameSecret ()
	{
		return mGameSecret;
	}
	
	public static bool SetDebugServers ()
	{
		//get the build info stored in the DebugBuild prefab 
		GameObject prefab = Instantiate(Resources.Load("DebugBuild", typeof(GameObject))) as GameObject;
		if (prefab == null)
			return false;

		/*
		DebugBuildData _debugData = prefab.GetComponent<DebugBuildData>();
		if (_debugData == null)
			return false;
		
		mGameKey = _debugData.GameKey;
		mGameSecret = _debugData.GameSecret;
		mDevBox = _debugData.Devbox;
		mEnviro = _debugData.Enviro;
		*/


		if (mGameKey == null || mGameSecret == null || mDevBox == null || mEnviro == null)
			return false;
		
		SetEnviro (mEnviro);
		
		Debug.Log ("DEBUG BUILD SYSTEM :: mEnviro = " + mEnviro);
		Debug.Log ("DEBUG BUILD SYSTEM :: mGameKey = " + mGameKey);
		Debug.Log ("DEBUG BUILD SYSTEM :: mGameSecret = " + mGameSecret);
		Debug.Log ("DEBUG BUILD SYSTEM :: mDevBox = " + mDevBox);
		Debug.Log ("DEBUG BUILD SYSTEM :: Enviro = " + mCurrentEnviro);
		
		//default - eProduction
		string SdkUrl = "https://api.fuelpowered.com/sdk/";
		string GrantooHost = "https://api.fuelpowered.com/api/v1";
		string TournamentHost = "https://api.fuelpowered.com/api/v1";
		string ChallengeHost = "https://challenge.fuelpowered.com/v1";
		string CdnHost = "http://cdn.fuelpowered.com/api/v1";
		string TransactionHost = "https://transaction.fuelpowered.com/api";
		string DynamicsHost = "https://api.fuelpowered.com/api/v2";
		
		switch (mCurrentEnviro) {
		case Enviro.eSandbox:
			SdkUrl = "https://api-sandbox.fuelpowered.com/sdk/";
			GrantooHost = "https://api-sandbox.fuelpowered.com/api/v1";
			TournamentHost = "https://api-sandbox.fuelpowered.com/api/v1";
			ChallengeHost = "https://challenge-sandbox.fuelpowered.com/v1";
			CdnHost = "http://cdn-sandbox.fuelpowered.com/api/v1";
			TransactionHost = "https://transaction-sandbox.fuelpowered.com/api";
			DynamicsHost = "https://api-sandbox.fuelpowered.com/api/v2";
			break;
		case Enviro.eInternal:
			SdkUrl = "https://api-internal.fuelpowered.com/sdk/";
			GrantooHost = "https://api-internal.fuelpowered.com/api/v1";
			TournamentHost = "https://api-internal.fuelpowered.com/api/v1";
			ChallengeHost = "https://challenge-internal.fuelpowered.com/v1";
			CdnHost = "http://cdn-internal.fuelpowered.com/api/v1";
			TransactionHost = "https://transaction-internal.fuelpowered.com/api";
			DynamicsHost = "https://apiV2-internal.fuelpowered.com/api/v2";
			break;
		case Enviro.eDevelopment:
			SdkUrl = "http://api-devbox.fuelpowered.com:"+mDevBox+"/sdk/";
			GrantooHost = "http://api-devbox.fuelpowered.com:"+mDevBox+"/api/v1";
			TournamentHost = "http://api-devbox.fuelpowered.com:"+mDevBox+"/api/v1";
			ChallengeHost = "http://challenge-internal.fuelpowered.com/v1";
			CdnHost = "http://cdn-devbox.fuelpowered.com/api/v1";
			TransactionHost = "http://transaction-internal.fuelpowered.com/api";
			DynamicsHost = "http://apiV2-internal.fuelpowered.com/api/v2";
			break;
			
		}
		
		Debug.Log ("DEBUG BUILD SYSTEM :: SetDebugServers");
		#if UNITY_IPHONE
		//iOSSetDebugEnironment (SdkUrl, GrantooHost, TournamentHost, ChallengeHost, CdnHost, TransactionHost, DynamicsHost);
		#endif
		
		return true;
	}
	
	
	void Awake ()
	{
		if (!m_bInitialized) {
			GameObject.DontDestroyOnLoad (gameObject);
			m_bInitialized = true;
		} else {
			GameObject.Destroy (gameObject);	
		}
		
		Debug.Log ("DEBUG BUILD SYSTEM :: Awake");
	}
	
	
	
	
}



