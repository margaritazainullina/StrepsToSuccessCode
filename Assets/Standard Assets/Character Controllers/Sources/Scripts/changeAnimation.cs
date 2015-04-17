﻿using UnityEngine;
using System.Collections;
using System;


public class changeAnimation : MonoBehaviour
{
    protected Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public bool position = false;
    public bool positionOffice = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (!position && transform.position.x < 1000.2 && transform.position.x > 997.6
                && transform.position.z < 1105 && transform.position.z > 1102.6)
        {
            if (Application.loadedLevelName != "office")
            {
                Application.LoadLevel("office");

                //StartCoroutine(DisplayLoadingScreen("office"));

                position = true;
            }
        }
        if (!positionOffice && transform.position.x < 378.5 && transform.position.x > 377.5
                && transform.position.z < 188.5 && transform.position.z > 185.5)
        {
            if (Application.loadedLevelName != "city")
            {
                Application.LoadLevel("city");
                positionOffice = true;

            }
            /*Dialoguer.EndDialogue();
    Dialoguer.StartDialogue(1);*/
        }

    }

	    void OnLevelWasLoaded(string level)
    {
      if (level == "city")
        {
            transform.position = new Vector3(992.9827f, 31.0f, 1109.696f);
        }
    }

    IEnumerator DisplayLoadingScreen(string levelToLoad)
    {
        AsyncOperation async = Application.LoadLevelAsync(levelToLoad);

        while (!async.isDone)
        {


        }

        Debug.Log("async");
        yield return null;
    }

}
