using UnityEngine;
using System.Collections;

public class StateReward : State
{
    #region singleton
    private static readonly StateReward instance = new StateReward();
    public static StateReward Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion


    // Use this for initialization
    public override void Init()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
