using UnityEngine;
using System.Collections;

public class B3Controller : MonoBehaviour {

    //quick way of setting up an end screen grabbing data from the score master
    public TextMesh questions, coins;

    private float countRate = 0.2f;
    private float nextCount;
    private int correctQuestionCount = 0;
    private int totalCoins = 0;

    void Update()
    {
        CountCorrectQuestions();
        CountCoins();
    }

    void CountCorrectQuestions()
    {
        while (correctQuestionCount < StateChallenge.Instance.GetCorrectAnswers() && Time.time > nextCount)
        {
            nextCount = Time.time + countRate;
            correctQuestionCount++;
        }

        questions.text = "Questions: " + correctQuestionCount + "/" + StateChallenge.Instance.GetQuestionAmount();
    }

    void CountCoins()
    {
        while (totalCoins < StateChallenge.Instance.GetCoinsEarned() && Time.time > nextCount)
        {
            nextCount = Time.time + countRate/2f;
            totalCoins++;
        }

        coins.text = "Coins Earned: " + totalCoins;

    }
	
}
