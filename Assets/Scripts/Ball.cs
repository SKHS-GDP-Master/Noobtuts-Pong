using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
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
        // Recalculate speed on each frame so that we can
        // troubleshoot escaping ball bug.
        var vel = rb.velocity.normalized; // Extract angular part of the velocity.
        vel *= speed;
        rb.velocity = vel;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        if (col.gameObject.name == "RacketLeft" || col.gameObject.name == "RacketRight") {
            float y = hitFactor(gameObject, col);
            Vector2 dir = Vector2.one;

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
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (b.transform.position.y - p.gameObject.transform.position.y) / p.collider.bounds.size.y;
    }
}