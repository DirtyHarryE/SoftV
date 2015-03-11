using UnityEngine;
using System.Collections;

public class CoinSpawnerB1_Final : MonoBehaviour {

    public GameObject coin;
    private int coinsSpawned = 0;
    private float m_Countdown = 0f;
    private float m_Reset = .5f;

    void Update()
    {
        //check with our score master and see if we've spawned enough coins
        if (StateChallenge.Instance.GetCoinsEarned() > coinsSpawned)
        {
            if (m_Countdown <= 0f)
            {
                SpawnCoin();
                m_Countdown = m_Reset;
            }
            else
            {
                m_Countdown -= Time.deltaTime;
            }
        }
    }

    //method to spawn a coin
    void SpawnCoin()
    {
        GameObject coinGO = GameObject.Instantiate(coin, transform.position, Quaternion.identity) as GameObject;
        coinGO.transform.parent = this.transform;
        coinsSpawned++;
    }
}
