using UnityEngine;

public class HudManager : MonoBehaviour
{
    public CanvasGroup _curTela;
    public void ChangeCanvas(GameObject tela)
    {
        _curTela.alpha = 0;
        _curTela.blocksRaycasts = false;
        _curTela = tela.GetComponent<CanvasGroup>();
        _curTela.alpha = 1;
        _curTela.blocksRaycasts = true;
    }
}
