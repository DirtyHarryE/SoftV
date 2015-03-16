using UnityEngine;
using System.Collections;

public class StateSplash : State
{
    #region singleton
    private static readonly StateSplash instance = new StateSplash();
    public static StateSplash Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    private SplashMono m_Mono;
    private float m_Timer;
    private bool m_Intro;


    // Use this for initialization
    public override void Init()
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/States/SplashScreen")) as GameObject;
        m_Mono = go.GetComponent<SplashMono>();
        m_Intro = true;
        m_Timer = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (m_Intro)
        {
            m_Timer += Time.deltaTime;

            if (0 <= m_Timer && m_Timer < 3f)
            {
                m_Mono.UCLGroup.gameObject.SetActive(true);
                m_Mono.NHSGroup.gameObject.SetActive(false);
                m_Mono.SoftVGroup.gameObject.SetActive(false);
            }
            else if (3f <= m_Timer && m_Timer < 6f)
            {
                m_Mono.UCLGroup.gameObject.SetActive(false);
                m_Mono.NHSGroup.gameObject.SetActive(true);
                m_Mono.SoftVGroup.gameObject.SetActive(false);
            }
            else if (6f <= m_Timer && m_Timer < 9f)
            {
                m_Mono.UCLGroup.gameObject.SetActive(false);
                m_Mono.NHSGroup.gameObject.SetActive(false);
                m_Mono.SoftVGroup.gameObject.SetActive(true);
            }
            else if (9f <= m_Timer)
            {
                m_Intro = false;
                m_Mono.MainMenu.SetActive(true);
                m_Mono.UCLGroup.gameObject.SetActive(false);
                m_Mono.NHSGroup.gameObject.SetActive(false);
                m_Mono.SoftVGroup.gameObject.SetActive(false);
            }
        }
    }

    public override void Exit()
    {
        UnityEngine.Object.Destroy(m_Mono.gameObject);
    }
    public void StartGame()
    {
        GameController.Instance.ChangeState(GameController.States.ChapterSelect);
    }
    public void GoToMainMenu()
    {
        m_Mono.DoctorsScreen.SetActive(false);
        m_Mono.OptionsScreen.SetActive(false);
        m_Mono.MainMenu.SetActive(true);
    }
    public void GoToOptionMenu()
    {
        m_Mono.DoctorsScreen.SetActive(false);
        m_Mono.OptionsScreen.SetActive(true);
        m_Mono.MainMenu.SetActive(false);
    }
    public void GoToDoctor()
    {
        m_Mono.DoctorsScreen.SetActive(true);
        m_Mono.OptionsScreen.SetActive(false);
        m_Mono.MainMenu.SetActive(false);
    }
}
