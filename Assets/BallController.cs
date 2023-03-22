using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;

    NetworkController gameController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(-1, 0) * 200);

        gameController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 15)
        {
            rb.velocity = rb.velocity.normalized * 15;
        }
    }

    void ResetPostion()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LimitLeft"))
        {
            ResetPostion();
            gameController.rightPoints += 1;
            rb.AddForce(new Vector3(1, 0) * 200);
        }

        if (collision.gameObject.CompareTag("LimitRight"))
        {
            ResetPostion();
            gameController.leftPoints += 1;
            rb.AddForce(new Vector3(-1, 0) * 200);
        }
    }


}
