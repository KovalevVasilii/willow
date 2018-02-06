using UnityEngine;
using System.Collections;
using CnControls;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMobility : MonoBehaviour
{
  
    public float speed = 15f;
    Vector3 position;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.gravityScale = 0;
    }

    void LookAtCursor()
    {

        Quaternion rot = Quaternion.LookRotation(transform.position- position, Vector3.up);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    void Update()
    {
        LookAtCursor();
    }
   

    void FixedUpdate()
    {
        position = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0f);
        transform.position += position * Time.deltaTime*speed;
    }
}
