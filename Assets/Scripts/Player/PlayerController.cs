using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;
    [SerializeField] private float mmoveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrall;
    private PlayerControll playerControll;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isDashing = false;

    private Animator myAnimator;
    private SpriteRenderer mySprite;
    private bool facingLeft = false;

    // call vào các component trong các assets

    private void Awake()
    {
        Instance = this;
        playerControll = new PlayerControll();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerControll.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        playerControll.Enable();
    }

    private void OnDisable()
    {
        playerControll.Disable();
    }

    private void Update()
    {
        PlayerInput();

    }

    private void FixedUpdate()
    {
        AddjustPlayerFacingDirection();
        Move();
    }

    // xac dinh cho nhan vat doc duoc 2 animation x va y 
    private void PlayerInput()
    {
        movement = playerControll.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    // call den x va y ben trong position, va gan chuyen dong cho nhan vat 
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (mmoveSpeed * Time.fixedDeltaTime));
    }

    // huong nhan vat den tro chuot

    private void AddjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            //fil nguoi choi
            mySprite.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySprite.flipX = false;
            FacingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            mmoveSpeed *= dashSpeed;
            myTrall.emitting = true;
            StartCoroutine(EndDash());
        }
    }

    private IEnumerator EndDash()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        mmoveSpeed /= dashSpeed;
        myTrall.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
