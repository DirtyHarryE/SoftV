using UnityEngine;
using System.Collections;


public class GameController {

    #region singleton
    private static readonly GameController instance = new GameController();
    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion


    private State[] m_States = new State[]{
        StateSplash.Instance,
        StateChapterSelect.Instance,
        StateChallenge.Instance,
        StatePinball.Instance,
        StateReward.Instance
    };
    public enum States
    {
        Splash,
        ChapterSelect,
        StateChallenge,
        StatePinball,
        StateReward
    };
    private State m_CurState;

    public State GetState(States state)
    {
        return m_States[(int)state];
    }

    public void ChangeState(States state)
    {
        Debug.Log("Change State");
        if (m_CurState!=null)
        {
            m_CurState.Exit();
        }
        m_CurState = m_States[(int)state];
        m_CurState.Init();
    }






    public void Init()
    {
        ChangeState(States.Splash);
    }
    public void Update()
    {
        if (m_CurState != null)
        {
            m_CurState.Update();
        }
    }

}
