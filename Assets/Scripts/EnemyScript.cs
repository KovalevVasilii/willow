using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float speed;
    public Transform Player;
  
    void FixedUpdate()
    {   Rigidbody2D r2d = GetComponent<Rigidbody2D>();

        float z = Mathf.Atan2((Player.transform.position.y - transform.position.y),
            (Player.transform.position.x - transform.position.x)) *Mathf.Rad2Deg-90;
            transform.eulerAngles = new Vector3(0, 0, z);
        r2d.AddForce(gameObject.transform.up * speed);
    }
}
