using UnityEditor.Analytics;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ServerManager serverManager;
    public CheckPointManager checkPointManager;
    public HudManager hudManager;
    public NetworkRunner Runner;
    NetworkSceneInfo _info = new NetworkSceneInfo();

    public GameObject serverPrefab;

    public string nickname;
    private void Awake()
    {
        instance = this;
        hudManager = instance.gameObject.GetComponent<HudManager>();
        checkPointManager = instance.gameObject.GetComponent<CheckPointManager>();
        var _sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        _info.AddSceneRef(_sceneRef, LoadSceneMode.Single);
    }
    public void StartSharedGame()
    {
        Runner.StartGame(new StartGameArgs()
        {
            Scene = _info,
            GameMode = GameMode.Shared
        });
    }
    public void StartSPGame()
    {
        Runner.StartGame(new StartGameArgs()
        {
            Scene = _info,
            GameMode = GameMode.Single
        });
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
    public void ChangeNick(GameObject input)
    {
        nickname = input.GetComponent<TextMeshProUGUI>().text;
    }
}
