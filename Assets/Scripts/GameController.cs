using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gamePlayerDeath() {
        //wait for a few seconds, then reload the level
        StartCoroutine(restartLevel());
        BroadcastMessage("PlayerDeath");
    }

    private IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void gameNextLevel() {
        StartCoroutine(loadNextLevel());
        BroadcastMessage("NextLevel");
    }
    
    private IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }
}
