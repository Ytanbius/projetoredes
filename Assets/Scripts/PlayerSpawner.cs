using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    private NetworkObject player;

    public void PlayerJoined(PlayerRef playerRef)
    {
        GameObject firstCheckpoint;
        Vector3 spawnLoc;
        if (playerRef == Runner.LocalPlayer)
        {
            SceneManager.UnloadSceneAsync("Menu");
            firstCheckpoint = ServerManager.instance.firstCheckPoint;
            playerPrefab = GameManager.instance.playerPrefab;
            if (playerPrefab.GetComponent<MagePlayerBehavior>())
                spawnLoc = new Vector3(0, 3, -1);
            else
                spawnLoc = firstCheckpoint.transform.position + Vector3.back;
            player = Runner.Spawn(playerPrefab, spawnLoc, Quaternion.identity, playerRef);
            if (player.GetComponent<MagePlayerBehavior>())
            {
                Debug.Log("mage");
                ServerManager.instance.MagePlayerSetup(player);
            }
        }
       
    }
}