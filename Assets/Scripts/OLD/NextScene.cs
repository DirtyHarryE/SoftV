using UnityEngine;
using System.Collections;

//script simply for going to the next scene with a key press
public class NextScene : MonoBehaviour {

    void Update()
    {
        if (Application.loadedLevelName == "B1_Final") RewardScreens();
    }

    void RewardScreens()
    {
        //simulate moving between scenes with a key press
        if (Input.GetKeyDown(KeyCode.Alpha1)) Application.LoadLevel("B2_Standard");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Application.LoadLevel("B2_Bowling");
        if (Input.GetKeyDown(KeyCode.Alpha3)) Application.LoadLevel("B2_60s");
    }
}
