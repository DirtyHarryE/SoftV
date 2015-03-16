using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class HorizontalScrollSnap : MonoBehaviour
{
    private bool /*This really*/ m_IsDragging;

    private ScrollRect m_ScrollRect;
    private GridLayoutGroup m_Grid;

    private RectTransform m_ContentRect;

    private float m_Intertia;

    void Start()
    {
        m_ScrollRect = this.GetComponent<ScrollRect>();

        m_Intertia = m_ScrollRect.decelerationRate;
        m_ScrollRect.inertia = false;

        m_Grid = m_ScrollRect.content.GetComponent<GridLayoutGroup>();
        m_ContentRect = m_ScrollRect.content.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (m_IsDragging)
        {
            Vector3 pos = m_ContentRect.transform.localPosition;
            bool even = m_Grid.transform.childCount % 2 == 0;
            float newX = Mathf.Round (pos.x / m_Grid.cellSize.x) * m_Grid.cellSize.x;

            if (m_Grid.transform.childCount % 2 == 0)
            {
                newX += (m_Grid.cellSize.x / 2f);
            }

            //Debug.Log("Rounding : [" + pos.x + " -> " + newX + "];");

            m_ContentRect.transform.localPosition = Vector3.Lerp(pos, new Vector3(newX, pos.y), m_Intertia);
        }
    }


    public void Drag()
    {
        m_IsDragging = false;
    }
    public void DragEnd()
    {
        m_IsDragging = true;
    }
}