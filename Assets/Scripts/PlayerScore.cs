using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour
{
    int lastKills = 0;
    int lastDeath = 0;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
    }

    private void OnDestroy()
    {
        if (player != null)
            SyncNow();
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }
    }

    void SyncNow()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataReceived);
        }
    }

    void OnDataReceived(string data)
    {
        if (player.kills > lastKills && player.kills > lastDeath)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeath;

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeaths(data);

        int newKills = killsSinceLast + kills;
        int newDeath = deathsSinceLast + deaths;

        string newData = DataTranslator.ValuesToData(newKills, newDeath);

        Debug.Log("Syncing " + newData);

        lastKills = player.kills;
        lastDeath = player.deaths;

        UserAccountManager.instance.SendData(newData);
    }
}
