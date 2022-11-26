using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : PlayerController
{
    

    public List<SpriteRenderer> sprClouds;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }




#if UNITY_ANDROID
 private void FixedUpdate()
    {

        if (isControlled)
            Fly(CameraControl.instance.moveAnalog.Horizontal, CameraControl.instance.moveAnalog.Vertical);
        if (isFlying)
            rb.gravityScale = Mathf.Clamp(transform.position.y / 100, 0, 100);
    }
#elif UNITY_STANDALONE_WIN
    private void FixedUpdate()
    {

        if (isControlled)
            Fly(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (isFlying)
            rb.gravityScale = Mathf.Clamp(transform.position.y / 100, 0, 100);
    }
#endif


    //Action
    public override void Action()
    {
        //light on
        CameraControl.instance.LightAction();
        CameraControl.instance.CameraZoomOut(5, new Vector3(60, 10, -10));
        CameraControl.instance.players[4].gameObject.SetActive(true);//water appears
        CameraControl.instance.ChangeCheckPointPos(1);

        this.gameObject.SetActive(false);
    }
}
