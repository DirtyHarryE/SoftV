using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChapterIconMono : MonoBehaviour
{

    [SerializeField]
    private Image m_Image;
    public Image Image { get { return m_Image; } }

    [SerializeField]
    private Text m_Title, m_Percentage;
    public Text Title { get { return m_Title; } }
    public Text Percentage { get { return m_Percentage; } }


    [SerializeField]
    private GameObject[] m_JigsawPieces;
    public GameObject[] JigsawPieces
    {
        get { return m_JigsawPieces; }
    }

    public int ID;

    public void PressButton()
    {
        (GameController.Instance.GetState(GameController.States.ChapterSelect) as StateChapterSelect).PressButton(ID);
    }
}
