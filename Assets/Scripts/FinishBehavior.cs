using UnityEngine;
using Fusion;
using TMPro;

public class FinishBehavior : NetworkBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Pontuacao(int points)
    {
        pointsText.text = points.ToString();
    }
}
