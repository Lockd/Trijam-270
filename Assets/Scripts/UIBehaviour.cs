using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hint;
    [SerializeField] private GameObject UIContainer;
    private float currentScore = 0;
    public bool isPlaying = false;
    public static UIBehaviour instance;
    public UnityEvent onGameOverEvent = new UnityEvent();
    public UnityEvent onGameStartEvent = new UnityEvent();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.Mouse0))
        {
            onGameStart();
        }
    }

    private void onGameStart()
    {
        onGameStartEvent.Invoke();
        UIContainer.SetActive(false);
        hint.text = "Game Over!\nClick anywhere to try again!";
        isPlaying = true;
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    public void onFlyKill()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void onGameWin()
    {
        hint.text = "Congratulations, you won!\nClick to go again";
        onGameOver();
    }

    public void onGameOver()
    {
        onGameOverEvent.Invoke();
        isPlaying = false;
        UIContainer.SetActive(true);
    }
}
