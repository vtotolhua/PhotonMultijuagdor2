using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class HostGame : MonoBehaviour
{
	List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
	bool matchCreated;
	NetworkMatch networkMatch;
	
	void Awake()
	{
		networkMatch = gameObject.AddComponent<NetworkMatch>();
	}
	
	void OnGUI()
	{
		// You would normally not join a match you created yourself but this is possible here for demonstration purposes.
		if(GUILayout.Button("Create Room"))
		{
			string name = "NewRoom";
			uint size = 4;
			bool advertise = true;
			string password = "";

			networkMatch.CreateMatch(name, size, advertise, password, "", "", 0, 0, OnMatchCreate);
		}

		if (GUILayout.Button("List rooms"))
		{
			networkMatch.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		}
		
		if (matchList.Count > 0)
		{
			GUILayout.Label("Current rooms");
		}
		foreach (var match in matchList)
		{
			if (GUILayout.Button(match.name))
			{
				networkMatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
			}
		}
	}
	
	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Create match succeeded");
			matchCreated = true;
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken(matchInfo.accessToken.ToString()));
			NetworkServer.Listen(matchInfo, 9000);
		}
		else
		{
			Debug.LogError ("Create match failed");
		}
	}
	
	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success && matches != null)
		{
			networkMatch.JoinMatch(matches[0].networkId, "", "", "", 0, 0, OnMatchJoined);
		}
	}
	
	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken(matchInfo.accessToken.GetByteString()));
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(matchInfo);
		}
		else
		{
			Debug.LogError("Join match failed");
		}
	}
	
	public void OnConnected(NetworkMessage msg)
	{
		Debug.Log("Connected!");
	}
}