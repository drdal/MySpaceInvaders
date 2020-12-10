using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickToStart : MonoBehaviour
{
    public Object sceneToGoTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (sceneToGoTo.name == "SampleScene") // reset score, when starting a new game
            {
                GameObject.Find("GameController").GetComponent<GameControlScript>().resetLifeIndicator();
                GameObject.Find("GameController").GetComponent<GameControlScript>().resetScore();
                SceneManager.LoadScene(sceneToGoTo.name);
            }
            else if (sceneToGoTo.name == "StartScene") // After Game Over
            {
                StartCoroutine(WaitBeforeLoadingSceneCoroutine(3, sceneToGoTo.name));
            }
            else {
                SceneManager.LoadScene(sceneToGoTo.name);
            }
        }
        
    }

    IEnumerator WaitBeforeLoadingSceneCoroutine(int seconds, string sceneName)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for X seconds.
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


}
