using UnityEngine;
using Fusion;
using UnityEditor.SearchService;

public class TrapBehavior : NetworkBehaviour
{
    public GameObject trapPrefab;
    public GameObject UI;
    public MagePlayerBehavior playerBehavior;
    private NetworkObject playerObject;

    public int activationDistance;
    private bool hasActivated;
    private void Update()
    {
        if (playerObject == null)
            playerObject = playerBehavior.GetComponent<NetworkObject>();
        checkDistance();
    }
    private void checkDistance()
    {
        if (playerBehavior.Object.HasStateAuthority && playerBehavior)
        {
            if(Mathf.Abs(playerObject.transform.position.x - this.transform.position.x) < activationDistance)
            {
                UI.SetActive(true);
                if(playerBehavior.interact && !hasActivated)
                {
                    onActivate();
                }
            }
            else
                UI.SetActive(false);
        }
    }
    public void onActivate()
    {
        Runner.Spawn(trapPrefab, this.transform.position, Quaternion.identity);
        hasActivated = true;
    }
}
