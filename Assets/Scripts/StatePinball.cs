using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/States/Pinball")) as GameObject;

        Chapter nextChapter = StateChapterSelect.Instance.Chapters[ID + 1];

        m_PinballMono = go.GetComponent<PinballMono>();
        for (int i = 0; i < m_PinballMono.Levels.Length; i++)
        {
            m_PinballMono.Levels[i].SetActive(i == ID);
        }



        List<int> intList = new List<int>();
        for (int i = 0; i < nextChapter.JigsawPeicesUnlocked.Length; i++)
        {
            if (nextChapter.JigsawPeicesUnlocked[i] == false)
            {
                intList.Add(i);
            }
        }
        while (intList.Count > 5)
        {
            intList.RemoveAt(Random.Range(0, intList.Count - 1));
        }
        //intList now ordered list of 5 elements or less





        int[] BucketsShuffled;
        {

            List<int> BucketIs = new List<int>(new int[] { 0, 1, 2, 3, 4 });
            BucketsShuffled = new int[BucketIs.Count];

            int count = BucketIs.Count;
            for (int i = 0; i < count; i++)
            {
                int iToRemove = Random.Range(0, BucketIs.Count);
                Debug.Log("Removed : [" + iToRemove + "|"+BucketIs[iToRemove]+"];");
                BucketsShuffled[i] = BucketIs[iToRemove];
                BucketIs.RemoveAt(iToRemove);
            }
        }
        //BucketsShuffled now randomised array of 1 through 5

        for (int i = 0; i < intList.Count; i++)
        {

            m_PinballMono.Buckets[BucketsShuffled[i]].JigsawPeiceToUnlock = intList[i];
            m_PinballMono.Buckets[BucketsShuffled[i]].LevelToUnlock = ID + 1;

            int div = intList[i] / 4;
            int mod = intList[i] % 4;
            string prefabstring = "Prefabs/JigsawPieces/" + mod + "_" + div;

            Debug.Log(i + "| Shuffle : [" + BucketsShuffled[i] + "]; Piece : [" + intList[i] + "]; Jigsaw Prefab Path : [" + prefabstring + "];");


            GameObject jigsawPieceGO = GameObject.Instantiate(Resources.Load(prefabstring)) as GameObject;
            RectTransform jigsawPieceTransform = jigsawPieceGO.GetComponent<RectTransform>();

            if (jigsawPieceTransform.childCount != 0)
            {
                Image img = jigsawPieceTransform.GetChild(0).GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = Resources.Load<Sprite>("PinballBackgrounds/" + StateChapterSelect.Instance.Chapters[ID + 1].PictureName);
                }
            }

            jigsawPieceTransform.parent = m_PinballMono.JigsawParents[BucketsShuffled[i]].transform;
            jigsawPieceTransform.localPosition = Vector2.zero;
            jigsawPieceTransform.localScale = Vector2.one * 0.015f;
        }
        m_PinballMono.Canvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Exit()
    {
        Debug.Log("Exit!");

        UnityEngine.Object.Destroy(m_PinballMono.gameObject);

        Debug.Log("Mono : [" + m_PinballMono + "]; Name : [" + m_PinballMono.name + "];");

        StateChallenge.Instance.ResetCoins();
        StateChallenge.Instance.ResetCorrectAnswers();
        StateChallenge.Instance.ResetQuestions();
    }
}
