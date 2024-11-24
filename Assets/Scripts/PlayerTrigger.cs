using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerGameOver : MonoBehaviour
{
    private Image fadeWhiteImage;
    private Image fadeBlackImage;

    [SerializeField] private string whiteImageName = "White";
    [SerializeField] private string blackImageName = "Black"; 

    private void Start()
    {
        fadeWhiteImage = GameObject.Find(whiteImageName)?.GetComponent<Image>();
        fadeBlackImage = GameObject.Find(blackImageName)?.GetComponent<Image>();

        if (fadeWhiteImage == null)
        {
            Debug.LogError($"WhiteImage");
        }

        if (fadeBlackImage == null)
        {
            Debug.LogError($"BlackImage");
        }

        if (fadeWhiteImage != null) fadeWhiteImage.gameObject.SetActive(false);
        if (fadeBlackImage != null) fadeBlackImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trident") && fadeWhiteImage != null)
        {
            fadeWhiteImage.gameObject.SetActive(true);
            StartCoroutine(BackToMenu());
        }

        if (other.CompareTag("Enemy") && fadeBlackImage != null)
        {
            fadeBlackImage.gameObject.SetActive(true);
            StartCoroutine(ResetLevel());
        }
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadSceneAsync(0);
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(1);
    }
}