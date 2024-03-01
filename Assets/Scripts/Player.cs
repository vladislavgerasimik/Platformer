using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _legs;
    [SerializeField] private Transform _rightLegsPosition;
    [SerializeField] private Transform _leftLegsPosition;
    [SerializeField] private LayerMask _maskGround;
    [SerializeField] private float _radiusLegs;
    [SerializeField] private PhysicsMaterial2D _physicsMaterial2D;
    [SerializeField] private Coin _coin;
    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreRecord;
    [SerializeField] private CheckPoint _CheckPoint;
    [SerializeField] private int _live = 4;
    [SerializeField] private TMP_Text _liveScore;
    [SerializeField] private GameObject _endGame;
    [SerializeField] private GameObject _endLevel;
    [SerializeField] private bool _dubleJump;
    [SerializeField] private int _jumpCount;
    private int _CurrentJumpCount;
    private bool _isShield;
    [SerializeField] private float _timer;
    private float _timeShield = 10;
    [SerializeField] private GameObject _shieldObject;
    private bool _inEnemi;
    private bool _isJump;

    public event UnityAction _isDead;

    private void Awake()
    {
        //_physicsMaterial2D.friction = 0;
        _CurrentJumpCount = _jumpCount;
    }


    void Update()
    {
        MoveSelection();
        Jump();

        /*if (_isJump == true)
        {
            _physicsMaterial2D.friction = 0;
        }
        else 
        {
            _physicsMaterial2D.friction = 1;
        }
        */
        if(_isShield == true)
        {   
            _timer += Time.deltaTime;
            if(_timer >= _timeShield)
            {
                _isShield = false;
                _shieldObject.SetActive(false);
                if(_inEnemi == true)
                {
                    _live -= 1;
                    transform.position = _CheckPoint.transform.position;
                    _liveScore.text = ($"∆изни: {_live}");
                }
            }
        }
      //if(_isJump == false)
      //  {
      //      _CurrentJumpCount = _jumpCount;
      //  }
    }


    public void MoveSelection()
    {
        RaycastHit2D hit = Physics2D.Raycast(_legs.position, Vector2.down, _radiusLegs);
        Vector2 normal = hit.normal;
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("isRun", true);
            //ќЅЏя—Ќ»“№!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (_isJump == false)
             {
                 _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
             }
             else
             {
                 _rigidbody.velocity = new Vector2(-_speed * 1.1f, _rigidbody.velocity.y);
             }

            GetComponent<SpriteRenderer>().flipX = true;

            if (hit.collider != null)
            {
                if (angle > 91 || angle < 89)
                {
                    _rigidbody.velocity = new Vector2(-_speed / 1f, _rigidbody.velocity.y);
                }
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("isRun", true);
            //ќЅЏя—Ќ»“№!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (_isJump == false)
            {
                _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(_speed * 1.1f, _rigidbody.velocity.y);
            }

            GetComponent<SpriteRenderer>().flipX = false;


            if (hit.collider != null)
            {
                if (angle > 91 || angle < 89)
                {
                    _rigidbody.velocity = new Vector2(_speed / 1f, _rigidbody.velocity.y);
                }
            }
        }
        else
        {
            _animator.SetBool("isRun", false);

            //ќЅЏя—Ќ»“№!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (_isJump == true)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else if (hit.collider != null)
            {
                if (angle < 91 && angle > 89)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                else
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
                }
            }
        }  
    }

    public void Jump()
    {
        if (_dubleJump == false)
        {
            if (Input.GetKey(KeyCode.Space) && _isJump == false)
            {            
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                
            }
        }
        if (_dubleJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _CurrentJumpCount > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _CurrentJumpCount -= 1;
            }
        }
        _animator.SetBool("isJump", _isJump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);

        Gizmos.DrawSphere(_legs.position, _radiusLegs);
    }

    private void FixedUpdate()
    {
        _isJump = !Physics2D.OverlapCircle(_legs.position, _radiusLegs, _maskGround);
        
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _score += 10;
            _scoreRecord.text = ($"—чет: {_score}");

            Destroy(coin.gameObject);


        }
        
        if (collision.TryGetComponent(out Enemies enemies) && _live > 0 && _isShield == false)
        {
            _rigidbody.velocity = Vector2.zero;
            _live -= 1;
            transform.position = _CheckPoint.transform.position;
            _liveScore.text = ($"∆изни: {_live}");
            _isDead?.Invoke();
         
        }        
        if(_live == 0)
        {
            _endGame.SetActive(true);
            Time.timeScale = 0;
        }
        else if (_isShield == true && collision.TryGetComponent(out Enemies _enemies) && _live > 0)
        {
            _inEnemi = true;
        }
        if (collision.TryGetComponent(out CheckPoint checkpoint))
        {
            _CheckPoint.GetComponent<SpriteRenderer>().color = Color.red;

            _CheckPoint = checkpoint;

            _CheckPoint.GetComponent<SpriteRenderer>().color = Color.green;

        }
        if (collision.TryGetComponent(out Live live))
        {
            _live += 1;
            _liveScore.text = ($"∆изни: {_live}");
            Destroy(live.gameObject);
        }
        if (collision.TryGetComponent(out End end))
        {
            _endLevel.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.TryGetComponent(out Shield shield))
        {

            _shieldObject.SetActive(true);
            _isShield = true;
            Destroy(shield.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemies enemies))
        {

            _inEnemi = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemies enemies) && _live > 0 && _isShield == false)
        {

            _live -= 1;
            transform.position = _CheckPoint.transform.position;
            _liveScore.text = ($"∆изни: {_live}");
            _isDead?.Invoke();


        }
        else if (_isShield == true && collision.collider.TryGetComponent(out Enemies _enemies) && _live > 0)
        {
            _inEnemi = true;
        }
        if (collision.collider.TryGetComponent(out CompositeCollider2D ground))
        {

            _CurrentJumpCount = _jumpCount;


        }

    }
}



//¬ скрипте Shop сделать так, чтобы когда игрок входит в триггер, то по€вл€лась панелька с диалогом

//—делать так, чтобы продавец поворачивалс€ в сторону игрока