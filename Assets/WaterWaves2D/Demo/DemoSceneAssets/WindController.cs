using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindController : PlayerController
{
    public List<SpriteRenderer> sprClouds;
    [SerializeField] GameObject WindCurrent;
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
        CameraControl.instance.CameraZoomOut(3, new Vector3(0, 10, -10),0);
        foreach (var cloud in sprClouds)
        {
            cloud.DOColor(new Color32(255, 255, 255, (byte)0.3f), 5f);
        }
        //CameraControl.instance.SwitchTarget(0);
        CameraControl.instance.players[2].gameObject.SetActive(true);//light appears
        CameraControl.instance.ChangeCheckPointPos(0);
        WindCurrent.GetComponent<SpriteRenderer>().DOFade(0,10).OnComplete(()=> WindCurrent.SetActive(false));
        
        this.gameObject.SetActive(false);
    }
}
