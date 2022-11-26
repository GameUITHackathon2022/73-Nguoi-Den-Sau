using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController:MonoBehaviour
{
    protected Joystick analog;
    protected bool firstTimeSwap = true;
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
            if(CameraControl.instance.currentPlayer!=null)
                return CameraControl.instance.currentPlayer.transform == this.transform;
            return false;
        }
        set
        {
           
            if (CameraControl.instance.isSwappable&&value)
            {
                CameraControl.instance.currentPlayer = this;
               
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
    //void FadeAway()
    //{
    //    this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(()=> this.gameObject.SetActive(false););
        
    //}
  
    protected void SwapSoul2()
    {
        if (firstTimeSwap)
        {
            CameraControl.instance.checkPoint.SetActive(true);
            firstTimeSwap = false;
            CameraControl.instance.CameraZoomOut(6, CameraControl.instance.checkPoint.transform.position,this);
            
        }

            
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
      
        }
    }
  
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            print("touchdsaaaaaaaaaaaaaaaaaa");
            SwapSoul2();

        }
    }

   
    public virtual void Action()
    {

    }
}