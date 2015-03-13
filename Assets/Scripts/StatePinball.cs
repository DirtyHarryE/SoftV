using UnityEngine;
using System.Collections;

public class StatePinball : State
{
    #region singleton
    private static readonly StatePinball instance = new StatePinball();
    public static StatePinball Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    public int ID;


    PinballMono m_PinballMono;
    // Use this for initialization
    public override void Init()
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/States/PinballLevels/" + StateChapterSelect.Instance.Chapters[ID].PrefabName)) as GameObject;
        Debug.Log("Init");
        m_PinballMono = go.GetComponent<PinballMono>();
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Exit()
    {
        StateChallenge.Instance.ResetCoins();
        StateChallenge.Instance.ResetCorrectAnswers();
        StateChallenge.Instance.ResetQuestions();

        UnityEngine.Object.Destroy(m_PinballMono);
    }
}
