

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed to join a random room.");

        // Create a room if we failed to join
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = 2
        });
    }

    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        Debug.Log("Joined a room");

    }
}
