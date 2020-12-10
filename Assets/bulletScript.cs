using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;

        if(hit.tag == "Enemy"){
            GameObject.Find("GameController").GetComponent<GameControlScript>().updateScore(10);
            var explosion = Instantiate(explosionPrefab, hit.transform.position, hit.transform.rotation);
            Destroy(hit);
            Destroy(explosion, 1.0f);
            Destroy(gameObject);
        }
    }
}
