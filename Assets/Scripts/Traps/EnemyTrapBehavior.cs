using UnityEngine;
using Fusion;
using System.Collections;
using UnityEngine.Rendering;

public class EnemyTrapBehavior : NetworkBehaviour
{
    public GameObject enemy;
    public GameObject prefab;
    public int numberToSpawn;
    public int spawned;
    private bool interval = false;
    public float angle = 0;

    private void Update()
    {
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        if(!interval)
        {
            StartCoroutine(SpawnInterval(rotation));
            if (angle == 0)
                angle = 180;
            else if (angle == 180)
                angle = 0;
        }
        if(spawned == numberToSpawn)
            Destroy(prefab);
    }

    IEnumerator SpawnInterval(Quaternion rotation)
    {
        interval = true;
        yield return new WaitForSeconds(1f);
        Runner.Spawn(enemy, transform.position, rotation);
        spawned++;
        interval = false;
    }
}
