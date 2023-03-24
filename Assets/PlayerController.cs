using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity;

    Rigidbody2D rb;
    PhotonView photonView;

    bool needToMoveX = false;
    bool needToMoveY = false;

    float moveY = 0;
    float moveX = 0;

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
                moveY = -1;
                amountToMove = 150;
                needToMoveY = true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveY = 1;
                amountToMove = 150;
                needToMoveY = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (rb.velocity.magnitude < 7.5)
                {
                    rb.AddForce(rb.velocity.normalized * 4000);
                }

            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1;
                amountToMove = 150;
                needToMoveX = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX = 1;
                amountToMove = 150;
                needToMoveX = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (needToMoveX)
        {
            needToMoveX = false;
            rb.AddForce(new Vector2(moveX, 0) * amountToMove);
        }

        if (needToMoveY)
        {
            needToMoveY = false;
            rb.AddForce(new Vector2(0, moveY) * amountToMove);
        }
    }
}
