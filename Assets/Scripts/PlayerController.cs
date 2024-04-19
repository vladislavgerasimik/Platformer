using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;
    public float _jumpPower;

    private Vector2 targetVelocity;
    private bool grounded;
    private Vector2 groundNormal;
    private Rigidbody2D _rigidbody2D;
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    
    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;

    private bool _isMovementPlatform;
    private Player player;


    void OnEnable()
    {        
        player = GetComponent<Player>();
        player._isDead += TEST;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        player._isDead -= TEST;
    }

    private void TEST()
    {
        Debug.Log(111111);
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
       /* if (_isMovementPlatform == false)
            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * 8, 0);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Velocity.y = _jumpPower;*/
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.x, groundNormal.y);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition;

        Movement(move, true);
    }


    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = _rigidbody2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                float slopeAngle = Vector2.Angle(Vector2.up, currentNormal);
                if (slopeAngle <= 45f)
                {
                    if (currentNormal.y > MinGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0)
                    {
                        Velocity = Velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
                else
                {
                    if (currentNormal.y > MinGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0)
                    {
                        Velocity = Velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }
        _rigidbody2D.position = _rigidbody2D.position + move.normalized * distance;
        }

    }
}

