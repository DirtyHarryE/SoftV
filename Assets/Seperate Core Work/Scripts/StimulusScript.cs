using UnityEngine;
using System.Collections;

public class StimulusScript : MonoBehaviour {

	GameObject m_goImage;
	GameObject m_goRectangle;
	Animator m_anim;

	//----------------------------------------------------------------------------------------------------
	// Use this for initialization
	//----------------------------------------------------------------------------------------------------
	void Start () 
	{
		m_goImage = transform.FindChild("Image").gameObject;
		m_goRectangle = transform.FindChild("Rectangle").gameObject;
		m_anim = GetComponent<Animator> ();
	}	

	//----------------------------------------------------------------------------------------------------
	// Update is called once per frame
	//----------------------------------------------------------------------------------------------------
	void Update () 
	{
	
	}

	//----------------------------------------------------------------------------------------------------
	// SetStimulusImage: set stimulus's image
	//----------------------------------------------------------------------------------------------------
	public void SetStimulusImage (string strImage) 
	{
		if (!m_goImage)
			m_goImage = transform.FindChild("Image").gameObject;

		// retrieve image from the Resource folder
		m_goImage.GetComponent<SpriteRenderer> ().sprite = Resources.Load(strImage, typeof(Sprite)) as Sprite;
	}

	//----------------------------------------------------------------------------------------------------
	// ShowStimulus: show / hide stimulus using animation
	//----------------------------------------------------------------------------------------------------
	public void ShowStimulus (bool bShow) 
	{
		if (!m_anim)
			m_anim = GetComponent<Animator> ();			

		m_anim.SetBool ("bSpawn", bShow); 
	}
}
