using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    private bool onGround;
    private Rigidbody playerRb;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float horizontalInput;
    [SerializeField] float rotationSpeed = 500;
    [SerializeField] private bool isDead;
    [SerializeField] private float speed = 10f;
    MeteoriteSpawnerScript meteoriteSpawner;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        meteoriteSpawner = GameObject.Find("MeteoriteSpawner").GetComponent<MeteoriteSpawnerScript>();
    }

    

    void Update()
    {

        horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0) ;
        float magnitude = movementDirection.magnitude;
        movementDirection.Normalize();

        
        playerRb.transform.Translate(movementDirection * speed * Time.deltaTime * magnitude, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            meteoriteSpawner.GameOver();
        }
    }
}
