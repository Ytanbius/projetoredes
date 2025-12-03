using UnityEngine;
using Fusion;
using System.Collections.Generic;
using TMPro;
using System;
public class ServerManager : NetworkBehaviour /*IPlayerJoined*/
{
    public static ServerManager instance;
    public GameObject _telaNull;
    public GameObject mageSpawn;
    public GameObject firstCheckPoint;
    public List<NetworkObject> traps;
    [Networked, Capacity(3)] public NetworkLinkedList<TimerUIManager> timerHuds => default;

    public List<NetworkObject> knightsPlayers;
    public NetworkObject magePlayer;

    [Header("Timer")]
    public bool timerActive;
    public TextMeshProUGUI timerText;
    [Networked, OnChangedRender(nameof(ChangeTimer))] public float currentTime { get; set; }
    public float _startMinutes = 5;
    [Networked] bool _startTimer { get; set; } = true;

    private void Awake()
    {
        instance = this;
        GameManager.instance.serverManager = this;
    }
    public override void FixedUpdateNetwork()
    {
        if (_startTimer)
        {
            currentTime = _startMinutes * 60;
            _startTimer = false;
        }
        if (timerActive)
        {
            currentTime -= Runner.DeltaTime;
        }
    }
    public void ChangeTimer()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
    }

    //public void PlayerJoined(PlayerRef playerRef)
    //{
    //    if(Object.HasStateAuthority)
    //    {
    //        playerRef.
    //    }
    //}
    public void MagePlayerSetup(NetworkObject player)
    {
        for (int i = 0; i < traps.Count; i++) 
        {
            traps[i].GetComponent<TrapBehavior>().playerBehavior = player.GetComponent<MagePlayerBehavior>();
        }
    }
}
