using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastController : MonoBehaviour
{
    private Text toastText;
    private float timer;
    private float startingTimer = 3f;
    private Animator animator;
    private bool firstUpdate;
    void Start()
    {
        DontDestroyOnLoad(this);
        toastText = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (animator.GetNextAnimatorStateInfo(0).IsName("Default"))
        {
            TurnOffToastController();

        }
        //DebugListener();
        ToastTimer();
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
    /*private void DebugListener()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TriggetToast("Testing...");
        }
    }*/
    public void TurnOffToastController()
    {
        animator.SetBool("fadeOut", false);
        animator.SetBool("fadeIn", false);
        this.gameObject.SetActive(false);
    }
}
