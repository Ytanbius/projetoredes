using UnityEngine;
using Fusion;
using System.Collections.Generic;
public class ServerManager : NetworkBehaviour
{
    public static ServerManager instance;
    public GameObject _telaNull;
    public List<NetworkObject> players;

    private void Awake()
    {
        instance = this;
    }
    public void SpawnPlayer(GameObject playerPrefab)
    {
        players.Add(Runner.Spawn(playerPrefab, new Vector2(0, 1), Quaternion.identity));
        players[players.Count - 1].name = "User: " + GameManager.instance.nickname;
        GameManager.instance.HudChange(_telaNull);
    }
}
