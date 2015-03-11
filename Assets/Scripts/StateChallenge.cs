using UnityEngine;
using System.Collections;

public class StateChallenge : State
{
    #region singleton
    private static readonly StateChallenge instance = new StateChallenge();
    public static StateChallenge Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    GameObject m_GO;
    // Use this for initialization
    public override void Init()
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/States/Challenge")) as GameObject;

        m_GO = go;
        Debug.Log("Init");
        coins = 0;
        questions = 0;
        correctAnswers = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("Coins : [" + coins + "];");
    }

    public override void Exit()
    {
        UnityEngine.Object.Destroy(m_GO);
    }







    private int coins ;
    private int questions ;
    private int correctAnswers ;

    public void AddCoin(int amount)
    {
        Debug.Log("Add Coin : [" + amount + "|" + coins + "];");
        coins += amount;
    }

    public void ResetCoins()
    {
        Debug.Log("ResetCoins : [" + coins + "];");
        coins = 0;
    }

    public int GetCoinsEarned()
    {
        Debug.Log("GetCoinsEarned : [" + coins + "];");
        return coins;
    }

    public void SetQuestionAmount(int amount)
    {
        questions = amount;
    }

    public int GetQuestionAmount()
    {
        return questions;
    }
    public void ResetQuestions()
    {
        questions = 0;
    }

    public void CorrectAnswer()
    {
        correctAnswers++;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }
    public void ResetCorrectAnswers()
    {
        correctAnswers = 0;
    }
}
