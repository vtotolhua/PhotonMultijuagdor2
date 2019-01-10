using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonHand : MonoBehaviour {

    public PhotonButtons photonB;
    public GameObject [] mainPlayer = null;
    private GameObject player = null;
        
    //private int numJug = 0;

    private void Awake() {
        DontDestroyOnLoad(this.transform);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    //Esto lo teniamos para dar la opcion al jugador de poner un nombre al room
    /*public void createNewRoom() {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 4 }, null);
    }*/

    public void joinOrCreateRoom() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        //PhotonNetwork.JoinOrCreateRoom(photonB.createRoomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom(photonB.createRoomName, roomOptions, TypedLobby.Default);
    }
    
    public void moveScene() {
        //PhotonNetwork.LoadLevel("EscenaUno");
        PhotonNetwork.LoadLevel(photonB.escena);
    }

    private void OnJoinedRoom()
    {
          moveScene();
//        Debug.Log("Estamos en el room" + photonB.createRoomInput.text);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        
        if ( scene.name == "EscenaUno")
        {
            Debug.Log("Player count inicio " + PhotonNetwork.room.PlayerCount);

            if (PhotonNetwork.room.PlayerCount == 1)
            //if ( GameObject.FindGameObjectWithTag("jugador1") == null)
            {
                player = mainPlayer[0];
                Debug.Log("player 1 " + player.name);
                Debug.Log("Player count " + PhotonNetwork.room.PlayerCount);
                SpawnPlayer();
            }

            if (PhotonNetwork.room.PlayerCount == 2)
            {
                player = mainPlayer[1];
                Debug.Log("player 2 " + player.name);
                Debug.Log("Player count " + PhotonNetwork.room.PlayerCount);
                SpawnPlayer();
            } 
        }

        if (scene.name == "Helicoptero")
        {
            player = mainPlayer[2];
            SpawnHelicopter();
        }

        if (scene.name == "MoonKart") {
            player = mainPlayer[3];
            SpawnPlayer();
        }
    }

    private void SpawnPlayer ()
    {
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
        /*Debug.Log("Num Jugadores " + PhotonNetwork.playerList.Length);
        if (PhotonNetwork.playerName == "jugador1")
        {
            PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
            PhotonNetwork.playerName = "jugador1";
            player.tag = "jugador1";
            Debug.Log("Nom Jugador " + PhotonNetwork.playerName);
            Debug.Log("Tag Jugador " + player.tag);
        }
        if (PhotonNetwork.playerList.Length == 1)
        {
            PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
            PhotonNetwork.playerName = "jugador2"; 
            player.tag = "jugador2";
            Debug.Log("Nom Jugador " + PhotonNetwork.playerName);
            Debug.Log("Tag Jugador " + player.tag);
        }*/
    }

    private void SpawnHelicopter()
    {
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
    }
}
