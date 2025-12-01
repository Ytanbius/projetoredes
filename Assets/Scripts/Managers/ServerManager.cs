using UnityEngine;
using Fusion;
using System.Collections.Generic;
using TMPro;
using System;
public class ServerManager : NetworkBehaviour
{
    public static ServerManager instance;
    public GameObject _telaNull;
    public GameObject mageSpawn;
    public GameObject firstCheckPoint;
    public List<NetworkObject> traps;

    public List<NetworkObject> knightsPlayers;
    public NetworkObject magePlayer;

    [Header("Timer")]
    public bool timerActive;
    [Networked] float currentTime { get; set; }
    public float _startMinutes;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
        GameManager.instance.serverManager = this;
        currentTime = _startMinutes * 60;
    }
    public override void FixedUpdateNetwork()
    {
        if (timerActive)
        {
            currentTime -= Runner.DeltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
    }
    public void MagePlayerSetup(NetworkObject player)
    {
        magePlayer = player;
        for (int i = 0; i < traps.Count; i++) 
        {
            traps[i].GetComponent<TrapBehavior>().playerBehavior = player.GetComponent<MagePlayerBehavior>();
        }
    }
}
