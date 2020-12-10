using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 0.1f;          // Hastighed på rumskib
    public GameObject bulletPrefab;     // reference to bullet asset
    float shotDelay = 0f;               // Shot delay counter
    public GameObject explosionPrefab; // ref to explosion asset

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        shotDelay += Time.deltaTime; // increase the shotDelay by time passed since last frame

        if (Input.GetKeyDown(KeyCode.Space)) // If space is pressed
        {
            if(shotDelay > 0.5f) // if it is more than 1 second since last bullet was fired
            {
                shotDelay = 0f; // reset shotDelay

                var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // Create bullet from asset
                bullet.GetComponent<Rigidbody2D>().velocity = transform.up * 6; // make bullet fly up
                Destroy(bullet, 2.0f); // set the bullet to selfdestruct after 2 seconds
            }

        }

        // Store the current horizontal input in a float
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Keep within screen bounds
        if(rb2d.transform.position.x > 9){ moveHorizontal = -1; }
        if(rb2d.transform.position.x < -9){ moveHorizontal = 1; }

        // Move ship
        rb2d.transform.Translate(new Vector3(moveHorizontal * speed, 0, 0));

    }

    // Check if ship was hit by enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;

        if(hit.tag == "Enemy" || hit.tag == "Bomb")
        {
            var explosion = Instantiate(explosionPrefab, hit.transform.position, hit.transform.rotation); // Create explosion
            Destroy(hit); // destroy enemy hit
            Destroy(explosion, 1f); // Set explosion to self destruct after 1 sec

            int lives = GameObject.Find("GameController").GetComponent<GameControlScript>().lifeLost();
            if (lives < 1)
            {
                GameObject.Find("GameController").GetComponent<GameControlScript>().endGame();
                Destroy(gameObject); // destroy ship
            }
        }
    }
}
