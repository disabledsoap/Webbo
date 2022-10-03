using System;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;

    public Transform groundCollider;

    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;

    const float k_GroundedRadius = .2f;

    private bool m_Grounded;
    public UnityEvent landEvent;

    public static int curpos = 0;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool isJumping = false;

    [SerializeField] private Transform[] teleports;
    [SerializeField] private Transform startTeleport;

    private Transform currTeleport;
    public static Action show;

    private bool visible = true;
    public static CharacterMovement instance;

    public void hideCharacter()
    {
        visible = false;
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void showCharacter()
    {
        visible = true;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (landEvent == null)
            landEvent = new UnityEvent();

        Teleport();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
        }

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    landEvent.Invoke();
            }
        }

        if (transform.position.y < -35f) { Teleport(); rb.velocity = new Vector2(rb.velocity.x, 0); };
        if (transform.position.y < -3f) horizontalMove = 0;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void Teleport()
    {
        string currPath = Directory.GetCurrentDirectory();
        string checkpointPath = currPath + "\\Assets\\Checkpoints.txt";

        string[] lines = File.ReadAllLines(checkpointPath);
        char teleportIndexChar = lines[0][0];

        int teleportIndex = teleportIndexChar - '0';

        Transform teleportPosition = teleports[teleportIndex];
        transform.position = new Vector3(teleportPosition.position.x, teleportPosition.position.y);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    void OnApplicationQuit()
    {
        string currPath = Directory.GetCurrentDirectory();
        string checkpointPath = currPath + "\\Assets\\Checkpoints.txt";

        string[] lines = { "0" };
        File.WriteAllLines(checkpointPath, lines);
    }
}