using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : PlayerController
{
    int doublJumpCount;
    public int doubleJumps;
    [SerializeField] Animator animator;
    // Update is called once per frame
    [SerializeField] ParticleSystem bubbleParticle;
    [SerializeField] ParticleSystem splashParticle;
    public SpriteRenderer spriteRenderer;
    ParticleSystem.EmissionModule emissionBubble;
    ParticleSystem.EmissionModule emissionSplash;
    private void Start()
    {
        isControlled = true;
        rb = GetComponent<Rigidbody2D>();
        if (bubbleParticle != null)
            emissionBubble.rateOverTime = 0;
        if (splashParticle != null)
            emissionSplash.rateOverTime = 0;
        //animator = GetComponent<Animator>();
        doublJumpCount = doubleJumps;
    }
    private void FixedUpdate()
    {
        if (isControlled)
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (isFlying)
            rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && doublJumpCount > 0 && isControlled)
        {
            print(doublJumpCount);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.Play("JumpShroom");
            doublJumpCount--;
        }
        if (isGround)
            doublJumpCount = doubleJumps;
    }

    void Move(float horizontalInput, float verticalInput)
    {
        Vector2 input = new Vector2(horizontalInput, verticalInput);
        if (isGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, GroundLayer);
            Vector2.Perpendicular(hit.normal);
            moveDir = -Vector2.Perpendicular(hit.normal) * Mathf.Sign(horizontalInput);//smooth movement on surfaces
        }
        else
            moveDir = new Vector2(horizontalInput, 0);

        if (input.magnitude > 0)
        {
            rb.AddForce(moveDir * speed);
            //if(horizontalInput != 0)
            //    transform.localScale = new Vector2(Mathf.Sign(horizontalInput),1);
            if (spriteRenderer != null)
            {
                if (horizontalInput < 0)
                    spriteRenderer.flipX = true;
                else if (horizontalInput > 0)
                    spriteRenderer.flipX = false;
            }

        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bubbleParticle != null)
            emissionBubble = bubbleParticle.emission;
        if (splashParticle != null)
            emissionSplash = splashParticle.emission;
    }
    IEnumerator SplashWater(float time)
    {
        emissionSplash.rateOverTime = 70;
        yield return new WaitForSeconds(time);
        emissionSplash.rateOverTime = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {

            emissionBubble.rateOverTime = 0;
            StartCoroutine(SplashWater(0.5f));
            //if (!isFlying)
            //{
            //    emissionBubble.rateOverTime = 0;
            //    StartCoroutine(SplashWater(0.5f));
            //}

            //playerAudioSource.PlayOneShot(audioClips[Random.Range(0, 3)]);
        }

    }
    private void OnDrawGizmos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, GroundLayer);
        Vector2.Perpendicular(hit.normal);
        var input = -Vector2.Perpendicular(hit.normal) * Mathf.Sign(Input.GetAxis("Horizontal"));
        Gizmos.DrawLine(hit.point, hit.point + input);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.TryGetComponent(out PlayerController playercontroller))
        //{

        //    SwapSoul(playercontroller);

        //}
        if (collision.gameObject.CompareTag("Water"))
        {
            emissionBubble.rateOverTime = 20;
            StartCoroutine(SplashWater(0.5f));
            //playerAudioSource.PlayOneShot(audioClips[1]);
        }

    }
    
}
