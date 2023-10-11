using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaliTrigger : MonoBehaviour
{
    [SerializeField] GameObject _failGroup;
    [SerializeField] TMP_Text _score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            _failGroup.SetActive(true);
            _score.text = ScoreManager.GetScore().ToString("000000");
            Time.timeScale = 0.0f;
        }
    }
    public void RestartScene()
    {
        Time.timeScale = 1.0f;
        ScoreManager.ResetScore();
        SceneManager.LoadScene(0);
    }
}
