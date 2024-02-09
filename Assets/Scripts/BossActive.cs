using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    [SerializeField] private EnemiAI _enemiAI;
    [SerializeField] private AudioSource _BossSound;
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {

            _enemiAI.WorkActivated();
            _BossSound.Play();

        }
    }
}

