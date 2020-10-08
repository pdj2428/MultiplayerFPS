using UnityEngine;
using Mirror;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour
{
    public static bool IsOn = false;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void LeaveRoom ()
    {
        IsOn = false;
        networkManager.StopHost();

    }
}
