using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static GameManager instance;
    public CheckPointManager checkPointManager;
    public HudManager hudManager;
    public NetworkRunner Runner;
    public ServerManager serverManager;
    public InputManager input;
    NetworkSceneInfo _info = new NetworkSceneInfo();

    public GameObject playerPrefab;

    public string nickname;
    private void Awake()
    {
        //SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Single);
        DontDestroyOnLoad(this.gameObject);
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        hudManager = instance.gameObject.GetComponent<HudManager>();
        checkPointManager = instance.gameObject.GetComponent<CheckPointManager>();
        input = instance.gameObject.GetComponent<InputManager>();
    }
    public void StartSharedGame(GameObject prefab)
    {
        playerPrefab = prefab;
        var _sceneRef = SceneRef.FromIndex(1);
        _info.AddSceneRef(_sceneRef, LoadSceneMode.Single);
        Runner.StartGame(new StartGameArgs()
        {
            Scene = _info,
            GameMode = GameMode.Shared
        });
    }
    public void StartSPGame(GameObject prefab)
    {
        playerPrefab = prefab;
        var _sceneRef = SceneRef.FromIndex(1);
        _info.AddSceneRef(_sceneRef, LoadSceneMode.Single);
        Runner.StartGame(new StartGameArgs()
        {
            Scene = _info,
            GameMode = GameMode.Single
        });
    }
    public void LeaveRoom()
    {
        Runner.Shutdown();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        SceneManager.LoadScene("Menu");
    }
    public void HudChange(GameObject tela)
    {
        hudManager.ChangeCanvas(tela);
    }
    public void LoadLastCheckPoint(GameObject player, GameObject checkpoint)
    {
        KnightPlayerBehavior playerBehavior;
        playerBehavior = player.GetComponent<KnightPlayerBehavior>();
        playerBehavior.enabled = false;
        checkPointManager.LoadLastCheckPoint(player, checkpoint);
        playerBehavior.enabled = true;
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
}
