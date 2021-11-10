using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = new Vector3(player.transform.position.x, 0, 0) + offset;
    }
}
