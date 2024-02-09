using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrecer : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    void Update()
    {
        Vector3 newPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 4*Time.deltaTime);
    }
}
