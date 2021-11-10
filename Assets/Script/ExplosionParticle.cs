using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(SpawnExplosion());
    }

    // Update is called once per frame
    IEnumerator SpawnExplosion()
    {


        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
