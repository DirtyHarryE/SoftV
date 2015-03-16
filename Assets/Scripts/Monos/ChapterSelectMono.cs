using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChapterSelectMono : MonoBehaviour {


    [SerializeField]
    private GridLayoutGroup m_Grid;
    public GridLayoutGroup Grid
    {
        get { return m_Grid; }
    }
    [SerializeField]
    private Scrollbar m_Bar;
    public Scrollbar Scrollbar { get { return m_Bar; } }
}
