using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactableObject : MonoBehaviour
{
    public void React()
    {
        gameObject.SetActive(false);
    }
}
