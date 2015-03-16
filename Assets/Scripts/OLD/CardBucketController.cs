using UnityEngine;
using System.Collections;

public class CardBucketController : MonoBehaviour {

    public int threshold;

    private int timesEntered;
    private float alphaValue;
    private float increaseAmount;
    private SpriteRenderer sprite;
    private Transform progressionColour;
    private float progressionStart;
    private float progressionMoveAmount;

    private int m_JigsawPeiceToUnlock = -1;
    public int JigsawPeiceToUnlock
    {
        get { return m_JigsawPeiceToUnlock; }
        set { m_JigsawPeiceToUnlock = value; }
    }
    private int m_LevelToUnlock = -1;
    public int LevelToUnlock
    {
        get { return m_LevelToUnlock; }
        set { m_LevelToUnlock = value; }
    }

	void Start () 
    {
        GetChildComponents();

        timesEntered = 0;
        alphaValue = 0;
        //set the amount to increase alpha based on the threshold (alpha between 0 and 1)
        increaseAmount = 1 / (float)threshold;

        //calculate amount to move our progression by
        progressionMoveAmount = Mathf.Abs(progressionStart/(float)threshold);
	}

    void GetChildComponents()
    {
        //quick way of grabbing child objects we need
        foreach (Transform child in transform)
        {
             if (child.CompareTag("colour")) sprite = child.GetComponent<SpriteRenderer>();
             if (child.CompareTag("progression"))
             {
                 progressionColour = child;
                 progressionStart = child.localPosition.y;
             }
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //destroy the coin once it's fallen into the bucket and add to our counter
        Destroy(other.gameObject);
        timesEntered++;
        increaseSpriteAlpha();
        MoveProgression();

        if (timesEntered == threshold)
        {
            if (StatePinball.Instance.ID < StateChapterSelect.Instance.Chapters.Length )
            {
                if (m_JigsawPeiceToUnlock != -1)
                {
                    StateChapterSelect.Instance.Chapters[m_LevelToUnlock].JigsawPeicesUnlocked[m_JigsawPeiceToUnlock] = true;
                }
                //int curPerc = StateChapterSelect.Instance.Chapters[StatePinball.Instance.ID+1].UnlockPerc;
                //StateChapterSelect.Instance.Chapters[StatePinball.Instance.ID+1].UnlockPerc = curPerc == 100 ? 0 : curPerc + 10;
            }
        }
    }

    void MoveProgression()
    {
        //move up our progression colour
        if(progressionColour.localPosition.y < 0) progressionColour.position = progressionColour.position + new Vector3(0, progressionMoveAmount, 0);
    }

    void increaseSpriteAlpha()
    {
        //increase the alpha of the renderer up to a max of 1
        alphaValue += increaseAmount;
        sprite.color = new Color(1, 1, 1, Mathf.Min(alphaValue, 1));
    }
}
