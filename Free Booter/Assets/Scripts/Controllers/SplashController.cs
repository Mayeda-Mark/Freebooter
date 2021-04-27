using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    public void AdvanceScene()
    {
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }
}
