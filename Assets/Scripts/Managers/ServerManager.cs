using UnityEngine;
using Fusion;
using System.Collections.Generic;
public class ServerManager : NetworkBehaviour
{
    public static ServerManager instance;
    public GameObject _telaNull;
    public GameObject mageSpawn;
    public GameObject firstCheckPoint;
    public List<NetworkObject> traps;

    private void Awake()
    {
        instance = this;
        GameManager.instance.serverManager = this;
    }
    public void MagePlayerSetup(NetworkObject player)
    {
        for (int i = 0; i < traps.Count; i++) 
        {
            traps[i].GetComponent<TrapBehavior>().playerBehavior = player.GetComponent<MagePlayerBehavior>();
        }
    }
}
