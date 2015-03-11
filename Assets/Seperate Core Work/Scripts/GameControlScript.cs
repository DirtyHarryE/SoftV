using UnityEngine;
using System.Collections;


public class GameControlScript : MonoBehaviour 
{
	// structure represent one trial/challenge 
	public struct stTrial {
		public string[] arrStimulus;  // each trial has 4 stimulus
		public string strAudio;   // audio of the target stimulus
		public int intTargetIdx;  // index of the target stimulus in arrStimulus
	}
	// array of 6 trials/challenges
	stTrial[] m_arrTrial = new stTrial[6];

	// index of the current displayed trial/challenge
	int m_intCurIdx = -1;

	// references to the script of each stimulus
	StimulusScript m_stimulusScript1;
	StimulusScript m_stimulusScript2;
	StimulusScript m_stimulusScript3;
	StimulusScript m_stimulusScript4;

	//RectangleSmallRed prefab
	//public GameObject m_RectangleSmallRed;

	// feedback (ticks) to show correct answer
	ParticleSystem m_particleSyst;

    // feedback (cross) to show incorrect answer
    ParticleSystem m_particleSystIncorrect;

	// flags to control the visibility of btnRestart
	bool bShowBtnRestart = false;

	// flags to control the visibility of btnRestart
	bool bShowBtnRepeat = false;

	//----------------------------------------------------------------------------------------------------
	// Use this for initialization
	//----------------------------------------------------------------------------------------------------
	void Start () 
	{
		m_stimulusScript1 = GameObject.Find("Stimulus1").GetComponent<StimulusScript>();
		m_stimulusScript2 = GameObject.Find("Stimulus2").GetComponent<StimulusScript>();
		m_stimulusScript3 = GameObject.Find("Stimulus3").GetComponent<StimulusScript>();
		m_stimulusScript4 = GameObject.Find("Stimulus4").GetComponent<StimulusScript>();

		// set particlesystem layer's order
		m_particleSyst = GameObject.Find("ParticleFeedback").GetComponent<ParticleSystem>();
		m_particleSyst.renderer.sortingLayerID = GameObject.Find("Stimulus1").transform.FindChild("Image").gameObject.renderer.sortingLayerID;
		m_particleSyst.renderer.sortingOrder = GameObject.Find("Stimulus1").transform.FindChild("Image").gameObject.renderer.sortingOrder + 1;

        m_particleSystIncorrect = GameObject.Find("ParticleFeedbackIncorrect").GetComponent<ParticleSystem>();
        m_particleSystIncorrect.renderer.sortingLayerID = GameObject.Find("Stimulus1").transform.FindChild("Image").gameObject.renderer.sortingLayerID;
        m_particleSystIncorrect.renderer.sortingOrder = GameObject.Find("Stimulus1").transform.FindChild("Image").gameObject.renderer.sortingOrder + 1;

		// set stimuli's images, audio and target index for all trials/challenges
		InitTrials ();

		// restart game
		RestartGame ();

	}

	//----------------------------------------------------------------------------------------------------
	// InitTrials: set stimuli's images, audio and target index for all trials/challenges
	//----------------------------------------------------------------------------------------------------
	void InitTrials()
	{
		m_arrTrial [0].arrStimulus = new string[4];
		m_arrTrial [0].arrStimulus [0] = "rose";
		m_arrTrial [0].arrStimulus [1] = "tree";
		m_arrTrial [0].arrStimulus [2] = "flower1";
		m_arrTrial [0].arrStimulus [3] = "flower2";
		m_arrTrial [0].strAudio = "rose"; 
		m_arrTrial [0].intTargetIdx = 0;

		m_arrTrial [1].arrStimulus = new string[4];
		m_arrTrial [1].arrStimulus [0] = "bag";
		m_arrTrial [1].arrStimulus [1] = "food";
		m_arrTrial [1].arrStimulus [2] = "brain";
		m_arrTrial [1].arrStimulus [3] = "art";
		m_arrTrial [1].strAudio = "art"; 
		m_arrTrial [1].intTargetIdx = 3;

		m_arrTrial [2].arrStimulus = new string[4];
		m_arrTrial [2].arrStimulus [0] = "own";
		m_arrTrial [2].arrStimulus [1] = "seat";
		m_arrTrial [2].arrStimulus [2] = "star";
		m_arrTrial [2].arrStimulus [3] = "box";
		m_arrTrial [2].strAudio = "seat"; 
		m_arrTrial [2].intTargetIdx = 1;

		m_arrTrial [3].arrStimulus = new string[4];
		m_arrTrial [3].arrStimulus [0] = "ball";
		m_arrTrial [3].arrStimulus [1] = "level";
		m_arrTrial [3].arrStimulus [2] = "clock";
		m_arrTrial [3].arrStimulus [3] = "south";
		m_arrTrial [3].strAudio = "clock"; 
		m_arrTrial [3].intTargetIdx = 2;

		m_arrTrial [4].arrStimulus = new string[4];
		m_arrTrial [4].arrStimulus [0] = "heart";
		m_arrTrial [4].arrStimulus [1] = "sphere";
		m_arrTrial [4].arrStimulus [2] = "shape1";
		m_arrTrial [4].arrStimulus [3] = "star";
		m_arrTrial [4].strAudio = "star"; 
		m_arrTrial [4].intTargetIdx = 3;

		m_arrTrial [5].arrStimulus = new string[4];
		m_arrTrial [5].arrStimulus [0] = "gift";
		m_arrTrial [5].arrStimulus [1] = "card";
		m_arrTrial [5].arrStimulus [2] = "case";
		m_arrTrial [5].arrStimulus [3] = "box";
		m_arrTrial [5].strAudio = "gift"; 
		m_arrTrial [5].intTargetIdx = 0;

        //tell the score master how many questions we're asking
        StateChallenge.Instance.SetQuestionAmount(m_arrTrial.Length);

	}

	//----------------------------------------------------------------------------------------------------
	// RestartGame: restart game
	//----------------------------------------------------------------------------------------------------
	void RestartGame()
	{
		bShowBtnRestart = false;		
		bShowBtnRepeat = false;
		m_intCurIdx = -1;
				
		SetNextTrial ();
	}

	//----------------------------------------------------------------------------------------------------
	// Update is called once per frame
	//----------------------------------------------------------------------------------------------------
	void Update () 
	{
		// touch screen
		if (Input.touchCount > 0) {
			for (var i = 0; i < Input.touchCount; ++i) {
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position), Vector2.zero);
					// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
					if (hitInfo) 
					{
						ShowFeedback(hitInfo);										
					}
				}
			}
		}
		// mouse click
		else if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			if(hitInfo)
			{
				ShowFeedback(hitInfo);					
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	// OnGUI
	//----------------------------------------------------------------------------------------------------
	void OnGUI () 
	{
		// set up font size based on screen resolution
		float fDefaultResolutionHeight = 800; 
		float fDefaultFontSize = 22; 
		int intFontSize = (int)(Screen.height / fDefaultResolutionHeight  * fDefaultFontSize);
						
		// button style
		GUIStyle styleBtn = new GUIStyle(GUI.skin.button);
		styleBtn.fontSize = intFontSize; 
		
		// set up button width & height relative to screen size
		float fBtnWidth = Screen.width / 12;
		float fBtnHeight = Screen.height / 20;
		
		// show restart button at the end of game
		if (bShowBtnRestart) 
		{
			Rect rectBtn = new Rect(Screen.width - Screen.width/10, Screen.height/30, fBtnWidth, fBtnHeight);
			if (GUI.Button (rectBtn, "Restart", styleBtn)) {
				RestartGame();
			}

			// show well-done label
			GUIStyle styleLabel = new GUIStyle ();
			styleLabel.fontSize = intFontSize; 
			float fLabelWidth = Screen.width / 10;
			float fLabelHeight = Screen.height / 10;
			Rect rectLabel = new Rect(Screen.width/1.6f - fLabelWidth/2, Screen.height/2 - fLabelHeight/2, fLabelWidth, fLabelHeight);
            //GUI.Label (rectLabel , "Well done!", styleLabel);
            //Debug.Log("Load Next Scene Now");
            //GameController.Instance.ChangeState(GameController.States.StatePinball);
		}
		
		// show repeat button during trial
		if (bShowBtnRepeat) 
		{
			Rect rectBtn = new Rect(Screen.width/1.6f - fBtnWidth/2, Screen.height/2 - fBtnHeight/2, fBtnWidth, fBtnHeight);
			if (GUI.Button (rectBtn, "Repeat", styleBtn)) {
				PlayAudio();
			}
		}
	}
	
	//----------------------------------------------------------------------------------------------------
	// ShowFeedback
	//--------------------------------------------------    --------------------------------------------------
	void ShowFeedback(RaycastHit2D hitInfo)
	{
		bShowBtnRepeat = false;

		if (ConvertStimulusTagToIdx (hitInfo.collider.gameObject.tag) == m_arrTrial [m_intCurIdx].intTargetIdx) {
			// show feedback if user has a correct answer

            //tell the score master to add coins and keep track we got a question right
            StateChallenge.Instance.AddCoin(3);
            StateChallenge.Instance.CorrectAnswer();

			m_particleSyst.transform.position = hitInfo.collider.gameObject.transform.position;
			m_particleSyst.Play ();
			StartCoroutine ("Wait1");
		} else {
            // show feedback if user hasn't got a right answer
            m_particleSystIncorrect.transform.position = hitInfo.collider.gameObject.transform.position;
            m_particleSystIncorrect.Play();
            StartCoroutine("Wait1");
		}
	}

	//----------------------------------------------------------------------------------------------------
	// ConvertStimulusTagToIdx
	//----------------------------------------------------------------------------------------------------
	int ConvertStimulusTagToIdx(string strTag)
	{
		int intIdx = -1;
		if (strTag == "Stimulus1")
			intIdx = 0;
		else if (strTag == "Stimulus2")
			intIdx = 1;
		else if (strTag == "Stimulus3")
			intIdx = 2;
		else if (strTag == "Stimulus4")
			intIdx = 3;

		return (intIdx);
	}

	//----------------------------------------------------------------------------------------------------
	// Wait1
	//----------------------------------------------------------------------------------------------------
	IEnumerator Wait1 ()
	{		
		// Wait for 2 sec
		yield return new WaitForSeconds(2f);

		// set all stimuli to invisible
		ShowStimuli (false);

		// continue next trial/challenge
		SetNextTrial ();
	}

	//----------------------------------------------------------------------------------------------------
	// SetNextTrial: continue next trial/challenge
	//----------------------------------------------------------------------------------------------------
	void SetNextTrial()
	{
		// increase current index
		m_intCurIdx++;

		// check if end of game
		if (m_intCurIdx >= 6) 
		{
			ShowStimuli (false);
			bShowBtnRestart = true;
            GameController.Instance.ChangeState(GameController.States.StatePinball);
			return;
		}

		//GameObject.Find("Background Sphere").renderer.enabled = true;
		ShowStimuli (false);
		// show stimuli's images and play target audio
		Invoke("ShowNextTrial", 1f);
	}

	//----------------------------------------------------------------------------------------------------
	// ShowStimuli: set stimuli to visible / invisible
	//----------------------------------------------------------------------------------------------------
	void ShowStimuli(bool bShow)
	{		 
		m_stimulusScript1.ShowStimulus (bShow);
		m_stimulusScript2.ShowStimulus (bShow);
		m_stimulusScript3.ShowStimulus (bShow);
		m_stimulusScript4.ShowStimulus (bShow);
	}

	//----------------------------------------------------------------------------------------------------
	// ShowNextTrial: show stimuli's images and play target audio
	//----------------------------------------------------------------------------------------------------
	public void ShowNextTrial()
	{
		ShowStimuli (true);

		m_stimulusScript1.SetStimulusImage ("Images/" + m_arrTrial [m_intCurIdx].arrStimulus[0]);
		m_stimulusScript2.SetStimulusImage ("Images/" + m_arrTrial [m_intCurIdx].arrStimulus[1]);
		m_stimulusScript3.SetStimulusImage ("Images/" + m_arrTrial [m_intCurIdx].arrStimulus[2]);
		m_stimulusScript4.SetStimulusImage ("Images/" + m_arrTrial [m_intCurIdx].arrStimulus[3]);
		
		PlayAudio (1f);
	}

	//----------------------------------------------------------------------------------------------------
	// PlayAudio: play target audio 
	//----------------------------------------------------------------------------------------------------
	void PlayAudio(float fDelay = 0)
	{
		string strAudio = "Audio/" + m_arrTrial [m_intCurIdx].strAudio;
		audio.clip = Resources.Load(strAudio) as AudioClip;
		//audio.Play();	
		audio.PlayDelayed(fDelay);
		
		bShowBtnRepeat = true;
	}

	/*{
		float scale_factor= 0.07f;  
		float MAXSCALE = 6.0f, MIN_SCALE = 0.6f; // zoom-in and zoom-out limits

		Vector3 scale = new Vector3(parentObject.transform.localScale.x + scale_factor*-1, parentObject.transform.localScale.y + scale_factor*-1, 1);
		scale.x = (scale.x < MIN_SCALE) ? MIN_SCALE : scale.x;
		scale.y = (scale.y < MIN_SCALE) ? MIN_SCALE : scale.y;
		//m_goRectangleRed1.transform.localScale = Vector3.zero;
		
		//create a meteor at a random x position
		//Vector3 pos = transform.position;
		//Instantiate(m_RectangleSmallRed, new Vector3(pos.x + Random.Range(-6, 6), pos.y, pos.z), Quaternion.identity);
		
		//Instantiate(m_RectangleSmallRed, new Vector3(Screen.width / 2, Screen.height / 2, pos.z), Quaternion.identity);
		//Instantiate(m_RectangleSmallRed, new Vector3(GameObject.Find("Stimulus1").transform.position.x, 5, 0), Quaternion.identity);
		//Instantiate(m_RectangleSmallRed, new Vector3(GameObject.Find("Stimulus2").transform.position.x, 5, 0), Quaternion.identity);
	}*/

}
