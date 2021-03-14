using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastController : MonoBehaviour
{
    [SerializeField] Text toastText;
    private float timer;
    private float startingTimer = 3f;
    [SerializeField] Animator animator;
    private bool firstUpdate, awake;
    [SerializeField] GameObject toastCanvass;
    void Start()
    {
        int numToasts = FindObjectsOfType<ToastController>().Length;
        if(numToasts > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        /*toastText = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();*/
    }
    void Update()
    {
        if(awake)
        {
            if (animator.GetNextAnimatorStateInfo(0).IsName("Default"))
            {
                TurnOffToastController();
            }
        }
        ToastTimer();
        if(Input.GetKeyDown(KeyCode.L))
        {
            TriggetToast("Testing");
        }
    }
    private void ToastTimer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            animator.SetBool("fadeOut", true);
            animator.SetBool("fadeIn", false);
            firstUpdate = true;
        } else
        {
            animator.SetBool("fadeOut", false);
        }
    }
    private void RestartTimer()
    {
        timer = startingTimer;
    }
    public void TriggetToast(string newToastText)
    {
        awake = true;
        toastCanvass.SetActive(true);
        animator.SetBool("fadeOut", false);
        RestartTimer();
        if(firstUpdate)
        {
            toastText.text = newToastText;
            animator.SetBool("fadeIn", true);
            firstUpdate = false;
        }
        else
        {
            toastText.text += "\n" + newToastText;
        }
    }
    public void TurnOffToastController()
    {
        animator.SetBool("fadeOut", false);
        animator.SetBool("fadeIn", false);
        toastCanvass.SetActive(false);
        awake = false;
    }
}
