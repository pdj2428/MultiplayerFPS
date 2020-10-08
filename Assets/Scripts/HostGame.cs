using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Net.Sockets;
using UnityEngine.XR.WSA.Input;

public class HostGame : MonoBehaviour
{
    private string LobbySceneName = "Lobby";

    [SerializeField]
    private int roomSize = 6;

    private string createRoomName;
    private string joinRoomName;

    private NetworkManager networkManager;

    private bool host = false;
    private bool client = false;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void SetCreateRoomName (string _name)
    {
        createRoomName = _name;
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name != LobbySceneName && networkManager.networkAddress != createRoomName && host)
        {
            networkManager.maxConnections = roomSize;
            networkManager.networkAddress = createRoomName;
        }
        else if (SceneManager.GetActiveScene().name != LobbySceneName && networkManager.networkAddress != createRoomName && client)
        {
            networkManager.networkAddress = joinRoomName;
        }
    }

    public void CreateRoom ()
    {
        if (createRoomName != "" && createRoomName != null)
        {
            host = true;
            Debug.Log("Creating Room: " + createRoomName + " with room for " + roomSize + " Players");

            networkManager.StartHost();
        }
    }

    public void SetJoinRoomName(string _name)
    {
        joinRoomName = _name;
    }

    public void JoinRoom()
    {
        if (joinRoomName != "" && joinRoomName != null)
        {
            client = true;
            Debug.Log("Joinning Room: " + joinRoomName + " with room for " + roomSize + " Players");

            networkManager.StartClient();
        }
    }
}
