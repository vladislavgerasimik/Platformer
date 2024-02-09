using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShooter : MonoBehaviour
{
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _ShootTime;
    [SerializeField] private float _targetDistance;
    [SerializeField] private Player _player;

    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _disActiveSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isWork = true;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }


    void Update()
    {
        if (_isWork == true)
        {


            if (Vector2.Distance(transform.position, _player.transform.position) < _targetDistance)
            {
                _spriteRenderer.sprite = _activeSprite;
                
                if (transform.position.x > _player.transform.position.x)
                {
                  //  Debug.Log("Выстрел");
                }

                
            }
            else
            {                
                
                if (Vector2.Distance(transform.position, _startPosition) < 1)
                {
                    _spriteRenderer.sprite = _disActiveSprite;
                   // Debug.Log("Выстрел");

                }
            }


        }
    }


    public void WorkActivated()
    {
        _isWork = true;
    }

}
