using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int _coinRewardAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun")){
            Debug.Log("Collision");
            ScoreManager.CollectCoin(_coinRewardAmount);
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
