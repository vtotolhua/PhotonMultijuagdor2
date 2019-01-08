﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonHand : MonoBehaviour {

    public PhotonButtons photonB;
    public GameObject [] mainPlayer;
    private GameObject player;
    
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
            player = mainPlayer[0];
            SpawnPlayer();    
        }

        if (scene.name == "Helicoptero")
        {
            player = mainPlayer[1];
            SpawnHelicopter();
        }
    }

    private void SpawnPlayer ()
    {
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
        if (PhotonNetwork.playerList.Length == 0) {
            player.tag = "jugador1";
        }
        if (PhotonNetwork.playerList.Length == 1)
        {
            player.tag = "jugador1";
        }
    }

    private void SpawnHelicopter()
    {
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
    }
}
