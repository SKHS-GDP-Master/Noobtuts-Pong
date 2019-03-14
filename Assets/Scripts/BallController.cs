using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 30;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Initial Velocity
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Recalculate speed on each frame to help with
        // troubleshooting the escaping ball. This lets
        // us pick up speed changes even if the ball is
        // no longer colliding with the paddles.
        var vel = rb.velocity.normalized; // Extract angular part of the velocity.
        vel *= speed;
        rb.velocity = vel;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a paddle, then:
        //   col.gameObject is the paddle
        //   col.transform.position is the paddle's position
        //   col.collider is the paddle's collider

        if (col.gameObject.name == "PaddleLeft" || col.gameObject.name == "PaddleRight") {
            float y = hitFactor(gameObject, col);
            Vector2 dir;

            // Hit the left Racket?
            if (col.gameObject.name == "RacketLeft") {
                // Calculate direction, make length=1 via .normalized
                dir = new Vector2(1, y).normalized;
            } else {
                // Calculate direction, make length=1 via .normalized
                dir = new Vector2(-1, y).normalized;
            }

            // Set Velocity with dir * speed
            rb.velocity = dir * speed;
        }
    }

    float hitFactor(GameObject b, Collision2D p) {
        // ascii art:
        // ||  1 <- at the top of the paddle
        // ||
        // ||  0 <- at the middle of the paddle
        // ||
        // || -1 <- at the bottom of the paddle
        return (b.transform.position.y - p.gameObject.transform.position.y) / p.collider.bounds.size.y;
    }
}