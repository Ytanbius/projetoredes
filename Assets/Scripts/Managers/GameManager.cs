using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
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
