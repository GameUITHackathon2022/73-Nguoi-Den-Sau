using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (isControlled)
            Fly(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (isFlying)
            rb.gravityScale = Mathf.Clamp(transform.position.y / 15, 0, 100);
    }
}
