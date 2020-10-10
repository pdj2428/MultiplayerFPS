using System.Collections;
using UnityEngine;

public class Killfeed : MonoBehaviour
{
    [SerializeField]
    GameObject killfeedItemprefab;

    void Start()
    {
        GameManager.instance.onPlayerKilledCallback += OnKill;
    }

    public void OnKill(string player, string source)
    {
        GameObject go = (GameObject)Instantiate(killfeedItemprefab, this.transform);
        go.GetComponent<KillfeedItem>().Setup(player, source);

        Destroy(go, 4f);
    }
}
