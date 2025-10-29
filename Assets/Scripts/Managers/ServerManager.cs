using UnityEngine;
using Fusion;
public class ServerManager : NetworkBehaviour
{
    public GameObject _telaNull;
    public void SpawnPlayer(GameObject playerPrefab)
    {
        Runner.Spawn(playerPrefab, new Vector2(0, 1), Quaternion.identity);
        GameManager.instance.HudChange(_telaNull);
    }
}
