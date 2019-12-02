﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject poop;

    private int score;

    [SerializeField]
    private Text scoretxt;
    [SerializeField]
    private Transform objbox;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(768, 1024, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool stopTrigger = true;
    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(CreatepoopRoutine());

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    public void GameStart()
    {
        score = 0;
        scoretxt.text = "Score : " + score;
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }

    public void Score()
    {
        if(stopTrigger)
        score++;
        scoretxt.text = "Score : " + score;
    }

    IEnumerator CreatepoopRoutine()
    {
        while (stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(0.2f);
        } 
    }

    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 5.0f));
        GameObject obj = Instantiate(poop, pos, Quaternion.identity);
        obj.transform.parent = objbox.transform;
        //pos.z = 0.0f;
        //Instantiate(poop, pos, Quaternion.identity);
    }
}
