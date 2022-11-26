using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterController : PlayerController
{
    [SerializeField] WaterWaves2D water;
    [SerializeField] GameObject windCurrent;
    bool inWater = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }


#if UNITY_ANDROID
 private void FixedUpdate()
    {

        if (isControlled)
            Fly(CameraControl.instance.moveAnalog.Horizontal, CameraControl.instance.moveAnalog.Vertical);
       
    }
#elif UNITY_STANDALONE_WIN
    private void FixedUpdate()
    {

        if (isControlled)
            Fly(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      
    }
#endif
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = true;
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
            rb.gravityScale = 1f;
        }
    }
    public override void Action()
    {
        
        CameraControl.instance.players[3].gameObject.SetActive(true);//wood appears
        CameraControl.instance.CameraZoomOut(5, new Vector3(30, -6, -10),0);

        DOVirtual.Color(water.outColor, new Color(0.2f, 0.8f, 1, 0.3f), 6, v => { water.outColor = v; water.Generate(); });
        DOVirtual.Color(water.inColor, new Color(0, 0.3f, 1, 0.4f), 6, v => { water.inColor = v; water.Generate(); });
        //water.colors[1] = new Color(0.2f, 0.8f, 1, 0.3f);
        //water.colors[0] = new Color(0, 0.3f, 1, 0.4f);
        //water.inColor = new Color(0.2f, 0.8f, 1, 0.3f);
        //water.outColor = new Color(0.2f, 0.8f, 1, 0.3f);

        //water.Generate();
        water.transform.DOMoveY(-20, 6);
        //water.BuildRectengularMesh(false);
        CameraControl.instance.ChangeCheckPointPos(2);
        //CameraControl.instance.SwitchTarget(3);
        this.gameObject.SetActive(false);
        windCurrent.SetActive(false);
        
       
        //water.colors.Clear();
        //water.colors.Add(inColor);
        //water.colors.Add(outColor);
        //water.colors.Add(lineColor);
       
    }
}
