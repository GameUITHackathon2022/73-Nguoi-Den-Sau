using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodController : PlayerController
{
    [SerializeField] Material grass;
    [SerializeField] WaterWaves2D water;
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
            rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    }
#elif UNITY_STANDALONE_WIN
    private void FixedUpdate()
    {

        if (isControlled)
            Fly(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (isFlying)
            rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    }
#endif
    public override void Action()
    {
        CameraControl.instance.CameraZoomOut(10, new Vector3(40, 10, -10), 0,30);
        DOVirtual.Float(0, 1, 10f, v => grass.SetFloat("_Fade", v)).SetEase(Ease.InQuad);
        this.gameObject.SetActive(false);
        water.transform.DOMoveY(-23, 6);
    }
}
