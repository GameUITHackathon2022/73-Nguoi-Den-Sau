using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController:MonoBehaviour
{
	protected Vector3 dragpoint;
    protected bool dragging=false;
    protected Rigidbody2D rb;
	public float maxVelocity=20f;
	public float speed;
    public float jumpForce;
    protected Vector2 moveDir;
    
    
    [SerializeField]AudioClip[] audioClips;
    [SerializeField]AudioSource playerAudioSource;
   
    public bool isFlying;
   
    public LayerMask GroundLayer;
    //public bool isControlledLocal;
    public bool isControlled
    {
        get
        {
            print(this.gameObject.name + (CameraControl.instance.currentPlayer == this) +" canswap? "+ CameraControl.instance.isSwappable);
            return CameraControl.instance.currentPlayer.transform == this.transform;
        }
        set
        {
            print(this.gameObject.name + CameraControl.instance.currentPlayer);
            if (CameraControl.instance.isSwappable&&value)
            {
                CameraControl.instance.currentPlayer = this;
                print(this.gameObject.name);
                StartCoroutine(CameraControl.instance.ResetIsSwappable(3f));
            }
               
        }
    }
    protected void Fly(float horizontalInput, float verticalInput)
    {
        Vector2 input = new Vector2(horizontalInput, verticalInput);
        if (input.magnitude > 0)
        {
            rb.AddForce(input * speed);

        }
    }
    protected bool isGround
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 1, GroundLayer);
        }
    }

    //   void Move(float horizontalInput, float verticalInput)
    //   {


    //       Vector2 input = new Vector2(horizontalInput, verticalInput);

    //       if(!isFlying)
    //       {
    //           if (isGround)
    //           {
    //               RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, GroundLayer);
    //               Vector2.Perpendicular(hit.normal);
    //               moveDir = -Vector2.Perpendicular(hit.normal) * Mathf.Sign(horizontalInput);//smooth movement on surfaces
    //           } 
    //           else
    //               moveDir = new Vector2(horizontalInput, 0);
    //       }
    //       else
    //       {
    //           moveDir = input;
    //       }

    //       if (input.magnitude > 0)
    //       {
    //		rb.AddForce(moveDir *speed);
    //           //if(horizontalInput != 0)
    //           //    transform.localScale = new Vector2(Mathf.Sign(horizontalInput),1);
    //           if(spriteRenderer != null)
    //           {
    //               if (horizontalInput < 0)
    //                   spriteRenderer.flipX = true;
    //               else if (horizontalInput > 0)
    //                   spriteRenderer.flipX = false;
    //           }

    //       }
    //}
    //private void FixedUpdate()
    //{
    //    if(isControlled)
    //        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //    if (isFlying)
    //        rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    //}
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && doublJumpCount >0 && isControlled)
    //    {
    //        print(doublJumpCount);
    //        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //        animator.Play("JumpShroom");
    //        doublJumpCount--;
    //    }
    //    if (isGround)
    //        doublJumpCount = doubleJumps;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("touch");
    //    if (collision.gameObject.CompareTag("Water"))
    //        particle.SetActive(true);
    //}
    //protected void SwapSoul(PlayerController playercontroller)
    //{
    //    if (isControlled && CameraControl.instance.isSwappable)
    //    {
    //        //print(isSwappable);
    //        playercontroller.isControlled = true;
    //        isControlled = false;
    //        CameraControl.instance.isSwappable = false;
    //        Invoke(nameof(ResetSwappable), 2f);
    //    }
    //    else if (!isControlled && CameraControl.instance.isSwappable)
    //    {
    //        playercontroller.isControlled = false;
    //        isControlled = true;
    //        CameraControl.instance.isSwappable = false;
    //        Invoke(nameof(ResetSwappable), 2f);
    //    }
    //}
    protected void SwapSoul2()
    {
        print("found player control");
        if (CameraControl.instance.isSwappable)
        {
            print("isswappable");
            if (!isControlled)
            {
                print("iscontrol false");
                isControlled = true;
            }
            else
            {
                isControlled = false;
            }
            //else
            //{
            //    isControlled = true;
            //}
   
            //Invoke(nameof(ResetSwappable), 2f);
        }
    }
    //public virtual void OnTriggerEnter2D(Collider2D collision)
    //{



    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("touch");
    //    if (collision.gameObject.TryGetComponent(out PlayerController playercontroller))
    //    {

    //    }
    //}
    //void ResetSwappable()
    //{
    //    CameraControlinstanceisSwappable = true;
    //}
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            print("touchdsaaaaaaaaaaaaaaaaaa");
            SwapSoul2();

        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //        particle.SetActive(false);
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //        particle.SetActive(true);
    //}



    //   void Update(){


    //	if(Input.GetMouseButtonDown(0)){
    //		Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //		RaycastHit2D[] hits=Physics2D.RaycastAll(mousePosition,Vector2.zero);
    //		for(int i=0;i<hits.Length;i++){ 
    //			if(hits[i].collider!=null && hits[i].collider.gameObject==gameObject){
    //				dragpoint=mousePosition-transform.position;
    //				this.dragging=true;
    //				break;
    //			}
    //		}
    //	}else if(Input.GetMouseButtonUp(0) && this.dragging){
    //		this.dragging=false;
    //	}
    //	if(this.dragging){
    //		Vector3 dragPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition)-dragpoint;
    //		dragPosition.z=transform.position.z;
    //		Vector2 force=(Vector2)(dragPosition-transform.position);
    //		rb.velocity+=force*2f;
    //		//Limiting maximum velocity so the object doesn't move unnaturally fast
    //		if(rb.velocity.magnitude>maxVelocity) rb.velocity*=maxVelocity/rb.velocity.magnitude;
    //		//Damping the velocity
    //		rb.velocity*=0.91f;
    //	}

    //	//if(transform.position.x>20f || transform.position.x<-20f || transform.position.y<-20f){ 
    //	//	transform.position=new Vector3(0f,9f,transform.position.z);
    //	//	rb.velocity=Vector2.zero;
    //	//}

    //}
}