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
        GameObject mageSpawn;
        Vector3 spawnLoc;
        if (playerRef == Runner.LocalPlayer)
        {
            SceneManager.UnloadSceneAsync("Menu");
            firstCheckpoint = ServerManager.instance.firstCheckPoint;
            mageSpawn = ServerManager.instance.mageSpawn;
            playerPrefab = GameManager.instance.playerPrefab;
            if (playerPrefab.GetComponent<MagePlayerBehavior>())
                spawnLoc = mageSpawn.transform.position + Vector3.back;
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