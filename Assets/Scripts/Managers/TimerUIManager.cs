using Fusion;
using System;
using TMPro;
using UnityEngine;

public class TimerUIManager : NetworkBehaviour
{
    public float currentTime;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        ServerManager.instance.timerHuds.Add(this);
    }
    public void Update()
    {
    }
}
