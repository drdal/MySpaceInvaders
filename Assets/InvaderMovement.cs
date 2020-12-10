using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvaderMovement : MonoBehaviour
{
    private AudioSource moveSound;      // Reference to sound
    float myTime = 0;   // Time counter
    public float moveSpeed = 1.0f; // invader speed
    private GameObject[] invaders; // array to store ref to invaders
    private float directionX = 1f; // Horizontal movement
    private float directionY = 0f; // Vertical movement
    //public float levelSpeed = 10f; // speedcontroller
    int level = 0;


    // Start is called before the first frame update
    void Start()
    {
        moveSound = GetComponent<AudioSource>(); // lets make the invaders noisy
        level = GameObject.Find("GameController").GetComponent<GameControlScript>().levelUp();
    }

    // Update is called once per frame
    void Update()
    {

        myTime += Time.deltaTime; // Increase timecounter by time passed since last frame

        if(myTime > moveSpeed) // if enough time passed
        {   
            // Init vars for max and min position of the invaders to keep them on screen
            float minX = 100;
            float maxX = -100;
            invaders = GameObject.FindGameObjectsWithTag("Enemy"); // find all invaders in scene
            int numVaders = invaders.Length; // Count number of invaders left

            // if there is still invaders...
            if(numVaders > 0)
            {
                //moveSpeed = (float)numVaders / levelSpeed; // Adjust speed by how many are left
                moveSpeed = numVaders / (5f + 2 * (float)level);
                

                // find min and max position foreach invader
                foreach(GameObject vader in invaders)
                {
                    float thisX = vader.GetComponent<Rigidbody2D>().position.x;
                    if(thisX > maxX) { maxX = thisX; }
                    if(thisX < minX) { minX = thisX; }
                }

                // if min/max x position is too far right/left change direction
                if(maxX > 8) { directionX = -1f; directionY = -1f; }
                if(minX < -8) { directionX = 1f; directionY = -1f; }

                moveSound.Play(); // Play the invader move sound

                // Now move each invader
                foreach(GameObject vader in invaders)
                {
                    vader.GetComponent<Rigidbody2D>().transform.Translate(new Vector3(directionX, directionY, 0));
                }
                directionY = 0f; // reset vertical movement
            }
            else // No more vaders left
            {
                SceneManager.LoadScene("SampleScene"); // reload scene
            }
            myTime = 0; // reset time counter

        }
    }
}
