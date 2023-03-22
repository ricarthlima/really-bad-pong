using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity;

    Rigidbody2D rb;
    PhotonView photonView;

    bool needToMove = false;
    Vector2 directionToMove;
    float amountToMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                directionToMove = Vector2.down;
                amountToMove = 150;
                needToMove = true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                directionToMove = Vector2.up;
                amountToMove = 150;
                needToMove = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (rb.velocity.magnitude < 7.5)
                {
                    directionToMove = rb.velocity.normalized;
                    rb.AddForce(directionToMove * 1000);
                }

            }
        }
        
    }

    private void FixedUpdate()
    {
        if (needToMove)
        {
            needToMove = false;
            rb.AddForce(directionToMove * amountToMove);
        }
    }
}
