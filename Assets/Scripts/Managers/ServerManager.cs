using UnityEngine;
using Fusion;
using System.Collections.Generic;
public class ServerManager : NetworkBehaviour
{
    public static ServerManager instance;
    public GameObject _telaNull;
    public List<NetworkObject> traps;

    private NetworkObject player;

    private Vector2 spawnLoc;

    private void Awake()
    {
        instance = this;
    }
    public void SpawnPlayer(GameObject playerPrefab)
    {
        if (playerPrefab.GetComponent<MagePlayerBehavior>())
            spawnLoc = new Vector2(0, 3);
        else
            spawnLoc = new Vector2(0, 0);
        player = Runner.Spawn(playerPrefab, spawnLoc, Quaternion.identity, this.Object.StateAuthority);
        if (player.GetComponent<MagePlayerBehavior>())
        {
            Debug.Log("mage");
            MagePlayerSetup(player);
        }
        GameManager.instance.HudChange(_telaNull);
    }
    private void MagePlayerSetup(NetworkObject player)
    {
        for (int i = 0; i < traps.Count; i++) 
        {
            traps[i].GetComponent<TrapBehavior>().playerBehavior = player.GetComponent<MagePlayerBehavior>();
        }
    }
}
