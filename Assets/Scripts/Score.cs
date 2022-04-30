using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public float currentScore = 0f;
    public TextMeshProUGUI text;
    void Start()
    {
        text.text = "Score: " + currentScore;
    }

    // Update is called once per frame
    public void addPoints(float p)
    {
        currentScore += p;
        text.text = "Score: " + currentScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
