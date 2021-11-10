using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteScript : MonoBehaviour
{
    [SerializeField] float speed = 1;
    MeteoriteSpawnerScript meteoriteSpawnerScript;
    [SerializeField] private ParticleSystem exposionParticle;
    [SerializeField]Vector3 whereToStrike;


    private void OnEnable()
    {
        
        meteoriteSpawnerScript = GameObject.Find("MeteoriteSpawner").GetComponent<MeteoriteSpawnerScript>();
        transform.position = meteoriteSpawnerScript.SetPos(10);
        whereToStrike = meteoriteSpawnerScript.SetPos(0);
        Vector3 lookDirection = (whereToStrike - transform.position).normalized;
    }
   
    private void Update()
    {
        Vector3 strike = (whereToStrike - transform.position).normalized;
        transform.Translate(strike * speed * Time.deltaTime, Space.World);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            Instantiate(exposionParticle, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Sensor"))
        {
            gameObject.SetActive(false);
        }
    }
}
