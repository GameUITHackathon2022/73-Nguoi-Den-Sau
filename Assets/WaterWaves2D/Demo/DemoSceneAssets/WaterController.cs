using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : PlayerController
{
    bool inWater;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    private void FixedUpdate()
    {
        if (isControlled && inWater)
            Fly(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //if (isFlying)
        //    rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
