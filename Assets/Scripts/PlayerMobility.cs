using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour
{
    public float speed;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
    void FixedUpdate()
    {
        Rigidbody2D r2d= GetComponent<Rigidbody2D>();
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         Debug.Log(mousePosition);
         Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        float input = Input.GetAxis("Vertical");
        r2d.AddForce(gameObject.transform.up * speed * input);
    }

}



