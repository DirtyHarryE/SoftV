using UnityEngine;
using System.Collections;

public class PinballMono : MonoBehaviour {

    [SerializeField]
    private CardBucketController[] m_Buckets;
    public CardBucketController[] Buckets { get { return m_Buckets; } }

    [SerializeField]
    private RectTransform[] m_JigsawParents;
    public RectTransform[] JigsawParents { get { return m_JigsawParents; } }

    [SerializeField]
    private GameObject[] m_Levels;
    public GameObject[] Levels { get { return m_Levels; } }

    [SerializeField]
    private Canvas m_Canvas;
    public Canvas Canvas { get { return m_Canvas; } }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
