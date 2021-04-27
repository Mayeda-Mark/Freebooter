using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayer : MonoBehaviour
{
    //[SerializeField] private string sceneName = default;
    //public LevelLoader ll;

    private void Update()
    {
        //ll.LoadNextLevel();
        StartCoroutine(StartNextScene());      
    }

    public IEnumerator StartNextScene()
    {
        yield return new WaitForSeconds(11);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
