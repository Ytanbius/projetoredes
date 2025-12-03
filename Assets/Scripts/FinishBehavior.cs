using UnityEngine;
using Fusion;
using TMPro;

public class FinishBehavior : NetworkBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Pontuacao(KnightPlayerBehavior player)
    {
        pointsText.text = player.points.ToString();
    }
}
