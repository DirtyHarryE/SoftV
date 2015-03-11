using UnityEngine;
using System.Collections;

public class StateSplash : State
{
    #region singleton
    private static readonly StateSplash instance = new StateSplash();
    public static StateSplash Instance
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
