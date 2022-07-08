using Assets.Scripts.Utils;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 5;

    [SerializeField]
    private CinemachineVirtualCamera playerCamera;

    [SerializeField]
    private GameObject backgroundMusic;

    private BackgroundMusicController backgroundMusicController;

    private HPUtil hpUtil;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private Rigidbody2D rb2D;

    private Timer attackDelayTimer;

    private float moveSpe;

    private float jumpForce;

    private bool isJumping;

    private float moveX;

    private float moveY;
    private GameObject weapon;

    private const float attackDelayTime = 0.5f;

    // [SerializeField]
    private GameObject hpBarOverLay;

    // Start is called before the first frame update
    void Start()
    {
        hpBarOverLay = transform.Find(Constant.HP_BAR_CURRENT)?.gameObject;

        hpUtil = new HPUtil(hpBarOverLay, maxHP);

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        rb2D = GetComponent<Rigidbody2D>();

        weapon = transform.Find(Constant.WEAPON).gameObject;

        attackDelayTimer = transform.GetComponent<Timer>();

        backgroundMusicController = backgroundMusic.GetComponent<BackgroundMusicController>();

        moveSpe = 3f;
        jumpForce = 50f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal"); // -1 left 0 stand 1 right
        moveY = Input.GetAxisRaw("Vertical");

        if (isJumping)
        {
            animator.SetFloat("Height", 1);
        }
        else
        {
            animator.SetFloat("Height", 0);
        }

        if (attackDelayTimer.IsFinished)
        {
            if (moveY == 0 && moveX == 0 && !isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                if (spriteRenderer.flipX)
                {
                    animator.SetTrigger("AttackRight");
                }
                else
                {
                    animator.SetTrigger("AttackLeft");
                }
                attackDelayTimer.CountDown = attackDelayTime;
                attackDelayTimer.StartCountDown();
            }
        }

        if (hpUtil.CurrentHP == 0 || transform.position.y < -9.6)
        {
            Destroy(gameObject, 0.1f);
            Destroy(playerCamera);
            backgroundMusicController.ChangeTheme(backgroundMusicController.gameOverTheme);
            Destroy(this);
            return;
        }
    }

    private void FixedUpdate()
    {

        if (moveX != 0)
        {
            ChangeSpriteDirection();
            AddMovement();
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (moveY > 0 && !isJumping)
        {
            AddJumpForce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Platform":
                SetCanJump(true);
                break;

            case "HitBox":
                AddJumpForce();
                SetCanJump(true);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.collider.gameObject.tag)
        {
            case "Enemy":
                hpUtil.DamageCalculation(Constant.DAMAGE_DEAL);
                break;
        }
    }

    private void SetCanJump(bool value)
    {
        if (value)
        {
            isJumping = false;
            return;
        }
        isJumping = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Platform":
                SetCanJump(false);
                break;

            case "HitBox":
                AddJumpForce();
                SetCanJump(false);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        /*switch(tag)
        {
            case "Enemy":
                //hpUtil.DamageCalculation(Constant.DAMAGE_DEAL);
                break;
        }*/
    }
    private void AddJumpForce()
    {
        rb2D.velocity = new Vector2(0, 0);
        Vector2 vec2 = new Vector2(0, jumpForce);
        rb2D.AddForce(vec2, ForceMode2D.Impulse);
    }
    private void AddMovement()
    {

        float distance = moveX * moveSpe;

        Vector2 vec2 = new Vector2(distance, 0f);

        rb2D.AddForce(vec2, ForceMode2D.Impulse);
    }

    private void ChangeSpriteDirection()
    {
        if (moveX > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
