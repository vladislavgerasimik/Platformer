using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PhysicsMaterial2D _physicsMaterial2D;

    [Header("Parametrs Move")]
    [SerializeField] private float _speed;
     
    

    [Header("Jumps")]
    [SerializeField] private bool _dubleJump;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _legs;
    [SerializeField] private LayerMask _maskGround;
    [SerializeField] private float _radiusLegs;
    private bool _isJump;
    private bool _isDJump;

    [Header("Parametrs GameObjects")]
    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreRecord;
    [SerializeField] private CheckPoint _CheckPoint;
    [SerializeField] private int _live = 4;
    [SerializeField] private TMP_Text _liveScore;
    [SerializeField] private GameObject _endGame;
    [SerializeField] private GameObject _endLevel;
    [SerializeField] private Transform _leftSide;
    [SerializeField] private Transform _rightSide;
    [SerializeField] private Vector3 _cubeSize;
    [SerializeField] private float _radiusCheck;
   
    private bool _inEnemi;

    [Header("Shield Object")]
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private float _timer;
    private float _timeShield = 10;
    private bool _isShield;



    public event UnityAction _isDead;

    void Update()
    {
        //_isJump = !Physics2D.OverlapCircle(_legs.position, _radiusLegs, _maskGround);

        MoveSelection();
        Jump(); 
        if(_isShield == true)
        {   
            _timer += Time.deltaTime;
            if(_timer >= _timeShield)
            {
                _isShield = false;
                _shieldObject.SetActive(false);
                _timer = 0;
                if(_inEnemi == true)
                {
                    _live -= 1;
                    transform.position = _CheckPoint.transform.position;
                    _liveScore.text = ($"∆ËÁÌË: {_live}");
                }
            }
        }  
    }


    public void MoveSelection()
    {
        RaycastHit2D hit = Physics2D.Raycast(_legs.position, Vector2.down, _radiusLegs);
        Vector2 normal = hit.normal;
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("isRun", true);
            //Œ¡⁄ﬂ—Õ»“‹!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (_isJump == false)
             {
                 _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
             }
             else
             {
                if (Physics2D.OverlapCircle(_leftSide.position, _radiusCheck, _maskGround))
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
                else
                {
                    _rigidbody.velocity = new Vector2(-_speed * 1.1f, _rigidbody.velocity.y);
                }
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
            //Œ¡⁄ﬂ—Õ»“‹!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (_isJump == false)
            {
                _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
            }
            else
            {
                if (Physics2D.OverlapCircle(_rightSide.position, _radiusCheck, _maskGround))
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
                else
                {
                _rigidbody.velocity = new Vector2(_speed * 1.1f, _rigidbody.velocity.y);

                }
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

            //Œ¡⁄ﬂ—Õ»“‹!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
                //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                
            }
        }
        if (_dubleJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isJump == false)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _isDJump = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _isDJump == false)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpPower * 1.5F, ForceMode2D.Impulse);
                _isDJump = true;
            }
        }
        _animator.SetBool("isJump", _isJump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(_legs.position, _radiusLegs);

        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawSphere(_leftSide.position, _radiusCheck);
        Gizmos.DrawSphere(_rightSide.position, _radiusCheck);
    }

    private void FixedUpdate()
    {
        _isJump = !Physics2D.OverlapCircle(_legs.position, _radiusLegs, _maskGround);
        
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCoin(collision);
        EnemiMove(collision);

        if (collision.TryGetComponent(out Enemies enemies) && _live > 0 && _isShield == false)
        {

            LiveOut();
         
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
            LiveUp();
            Destroy(live.gameObject);
        }
        if (collision.TryGetComponent(out End end))
        {
            _endLevel.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.TryGetComponent(out Shield shield))
        {
            Shield();
            Destroy(shield.gameObject);
        }
    }

    private void CheckCoin(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _score += 10;
            _scoreRecord.text = ($"—˜ÂÚ: {_score}");

            Destroy(coin.gameObject);
        }
    }
    private void LiveUp()
    {
        _live += 1;
        _liveScore.text = ($"∆ËÁÌË: {_live}");
    }

    private void EnemiMove(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemies enemies) && _live > 0 && _isShield == false)
        {
            LiveOut();
        } 
    }

    private void LiveOut()
    {
        _rigidbody.velocity = Vector2.zero;
        _live -= 1;
        transform.position = _CheckPoint.transform.position;
        _liveScore.text = ($"∆ËÁÌË: {_live}");
        _isDead?.Invoke();
        Shield();
        _timer = 7;
        if (_live == 0)
        {
            _endGame.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void Shield()
    {
        _shieldObject.SetActive(true);
        _isShield = true;
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
            LiveOut();
        }
        else if (_isShield == true && collision.collider.TryGetComponent(out Enemies _enemies) && _live > 0)
        {
            _inEnemi = true;
        }
       

    }
}





