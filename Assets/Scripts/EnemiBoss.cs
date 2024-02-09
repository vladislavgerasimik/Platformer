using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiBoss : MonoBehaviour
{

    
        [SerializeField] private float _targetDistance;
        [SerializeField] private Player _player;
        [SerializeField] private float _speed;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _disActiveSprite;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Vector3 _startPosition;

        void Start()
        {
            _startPosition = transform.position;
        }


        void Update()
        {
            if (Vector2.Distance(transform.position, _player.transform.position) < _targetDistance)
            {
                _spriteRenderer.sprite = _activeSprite;
                int direction = 1;
                if (transform.position.x > _player.transform.position.x)
                {
                    direction = -1;
                }
                _rigidbody2D.velocity = new Vector2(direction * _speed * Time.deltaTime, _rigidbody2D.velocity.y);
            }
            else
            {
                int direction = 1;
                if (transform.position.x > _startPosition.x)
                {
                    direction = -1;
                }
                _rigidbody2D.velocity = new Vector2(direction * _speed * Time.deltaTime, _rigidbody2D.velocity.y);
                if (Vector2.Distance(transform.position, _startPosition) < 1)
                {
                    _spriteRenderer.sprite = _disActiveSprite;
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }


        }
    }


