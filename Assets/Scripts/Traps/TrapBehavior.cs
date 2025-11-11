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
    private Animator animator;
    private NetworkObject playerObject;
    public float destroyCD = 5;

    public int activationDistance;
    private bool hasActivated;
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
                UI.SetActive(true);
                if(playerBehavior.interact && !hasActivated)
                {
                    animator.SetTrigger("ActivateTrap");
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
        Destroy(UI);
        hasActivated = true;
        //StartCoroutine(DestroyTrap());
    }

    //private IEnumerator DestroyTrap()
    //{
    //    yield return new WaitForSeconds(destroyCD);
    //    Destroy(trapObject);
    //}
}
