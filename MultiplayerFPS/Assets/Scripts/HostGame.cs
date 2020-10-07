using UnityEngine;
using Mirror;

public class HostGame : MonoBehaviour
{
    [SerializeField]
    private int roomSize = 6;

    private string createRoomName;
    private string joinRoomName;

    private NetworkManager networkManager;
    
    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void SetCreateRoomName (string _name)
    {
        createRoomName = _name;
    }

    public void CreateRoom ()
    {
        if (createRoomName != "" && createRoomName != null)
        {
            Debug.Log("Creating Room: " + createRoomName + " with room for " + roomSize + " Players");

            networkManager.maxConnections = roomSize;
            NetworkClient.Connect(createRoomName);
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
            Debug.Log("Joinning Room: " + joinRoomName + " with room for " + roomSize + " Players");

            NetworkClient.Connect(createRoomName);
            networkManager.StartClient();
        }
    }
}
