using UnityEngine;
using Fusion;
using System.Collections;
using NUnit.Framework.Constraints;

public class TrapBehavior : NetworkBehaviour
{
    public GameObject trapPrefab;
    public NetworkObject trapObject;
    public GameObject UI;
    public MagePlayerBehavior playerBehavior;
    public CanvasGroup escolhaUI;
    private Animator animator;
    private NetworkObject playerObject;
    public float destroyCD = 5;

    public int activationDistance;
    public bool hasActivated;
    public bool canActivate;
    private void Update()
    {
        if (playerObject == null)
        {
            playerObject = playerBehavior.GetComponent<NetworkObject>();
            animator = playerObject.GetComponent<Animator>();
        }
        checkDistance();
    }
    private void checkDistance()
    {
        if (playerBehavior.Object.HasStateAuthority && playerBehavior)
        {
            if(Mathf.Abs(playerObject.transform.position.x - this.transform.position.x) < activationDistance)
            {
                playerBehavior.nearestTrap = this;
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
            }
        }
    }
    public void onActivate()
    {
        animator.SetTrigger("ActivateTrap");
        Runner.Spawn(trapPrefab, this.transform.position, Quaternion.identity);
        Destroy(UI);
        escolhaUI.alpha = 0;
        escolhaUI.blocksRaycasts = false;
        hasActivated = true;
        //StartCoroutine(DestroyTrap());
    }

    //private IEnumerator DestroyTrap()
    //{
    //    yield return new WaitForSeconds(destroyCD);
    //    Destroy(trapObject);
    //}
}
