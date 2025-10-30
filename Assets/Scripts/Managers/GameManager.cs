using UnityEditor.Analytics;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HudManager hud;
    public NetworkRunner Runner;
    NetworkSceneInfo _info = new NetworkSceneInfo();


    private void Awake()
    {
        instance = this;
        hud = this.gameObject.GetComponent<HudManager>();
        var _sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        _info.AddSceneRef(_sceneRef, LoadSceneMode.Single);
    }
    public void StartGame()
    {
        Runner.StartGame(new StartGameArgs()
        {
            Scene = _info,
            GameMode = GameMode.Shared
        });
    }
    public void HudChange(GameObject tela)
    {
        hud.ChangeCanvas(tela);
    }
}
