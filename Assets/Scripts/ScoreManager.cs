using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int _score;
    private static ScoreManager _instance;
    [SerializeField] private TMP_Text _scoreField;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        _score += 10 * (int)Time.timeScale;
        _scoreField.text = GetScore().ToString("000000");
    }
    public static ScoreManager Instance { get { return _instance; } }
    public static void ResetScore()
    {
        _score = 0;
    }
    public static void CollectCoin(int _coinRewardAmount)
    {
        _score += _coinRewardAmount;
    }
    public static int GetScore() => _score;

}
