using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invaderScript : MonoBehaviour
{
    public GameObject bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 6000f) < 1f)
        {
            var bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            bomb.GetComponent<Rigidbody2D>().velocity = transform.up * (-3);
            Destroy(bomb, 3f);
        }
    }
}
