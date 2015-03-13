using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Chapter
{
    public ChapterIconMono Mono;
    public string Name;
    public string PrefabName;
    public string PictureName;
    public int UnlockPerc;

    public bool[] JigsawPeicesUnlocked = new bool[12];

    public Chapter(string name, string prefabName, string picture, int initPerc = 0)
    {
        Name = name;
        PrefabName = prefabName;
        PictureName = picture;

        float perc = (float)initPerc / 100f;
        perc *= JigsawPeicesUnlocked.Length;
        for (int i = 0; i < JigsawPeicesUnlocked.Length; i++)
        {
            JigsawPeicesUnlocked[i] = (i < perc);
        }
        UnlockPerc = initPerc;
    }
}

public class StateChapterSelect : State
{
    #region singleton
    private static readonly StateChapterSelect instance = new StateChapterSelect();
    public static StateChapterSelect Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion


    private Chapter[] m_Chapters = new Chapter[]{
        new Chapter("Chapter One", "Pinball_1", "01", 100),
        new Chapter("Chapter Two", "Pinball_2","02",100),
        new Chapter("Chapter Three", "Pinball_3","03",90),
        new Chapter("Chapter Four", "Pinball_1","01"),
        new Chapter("Chapter Five", "Pinball_2","02"),
        new Chapter("Chapter Six", "Pinball_3","03")
    };
    public Chapter[] Chapters { get { return m_Chapters; } }

    ChapterSelectMono m_Mono;

	// Use this for initialization
	public override void Init () {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/States/ChapterSelect")) as GameObject;
        m_Mono = go.GetComponent<ChapterSelectMono>();

        for (int i = 0; i < m_Chapters.Length; i++)
        {
            GameObject chapterGO = GameObject.Instantiate(Resources.Load("Prefabs/Chapter")) as GameObject;

            m_Chapters[i].Mono = chapterGO.GetComponent<ChapterIconMono>();
            m_Chapters[i].Mono.transform.parent = m_Mono.Grid.transform;

            m_Chapters[i].Mono.transform.localScale = Vector2.one;

            m_Chapters[i].Mono.ID = i;
            m_Chapters[i].Mono.Image.sprite = Resources.Load<Sprite>("PinballBackgrounds/" + m_Chapters[i].PictureName);
            m_Chapters[i].Mono.Title.text = m_Chapters[i].Name;
            m_Chapters[i].Mono.Percentage.text = m_Chapters[i].UnlockPerc.ToString() + "%";

            for (int j = 0; j < m_Chapters[i].JigsawPeicesUnlocked.Length; j++)
            {
                m_Chapters[i].Mono.JigsawPieces[j].SetActive(m_Chapters[i].JigsawPeicesUnlocked[j]);
                m_Chapters[i].Mono.JigsawPieces[j].transform.GetChild(0).GetComponent<Image>().sprite = m_Chapters[i].Mono.Image.sprite;
            }
        }
        float preferred = m_Mono.Grid.cellSize.x * m_Chapters.Length;
        Debug.Log("Pos 1 : [" + m_Mono.Grid.transform.localPosition + "]; Preferred Width : [" + preferred + "];");

        m_Mono.Grid.transform.localPosition += new Vector3(preferred / 2, 0, 0);

        Debug.Log("Pos 2 : [" + m_Mono.Grid.transform.localPosition + "];");
	}
	
	// Update is called once per frame
    public override void Update()
    {

    }

    public override void Exit()
    {
        UnityEngine.Object.Destroy(m_Mono.gameObject);
    }

    public void PressButton(int ID)
    {
        if (m_Chapters[ID].UnlockPerc >= 100)
        {
            GameController.Instance.ChangeState(GameController.States.StateChallenge);
        }
        StatePinball.Instance.ID = ID;
    }
}
