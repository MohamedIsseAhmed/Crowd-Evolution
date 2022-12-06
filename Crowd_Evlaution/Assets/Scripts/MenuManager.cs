using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private float levelComplateTween = 0.1f;
    [SerializeField] private float waitTimeScale = 1f;
    [SerializeField] private GameManager gameManagerSO;
    private void Start()
    {
      //  LastEnemyTracker.instance.OnAllEnemiesDied += Ýnstance_OnAllEnemiesDied;
    }
    private void OnEnable()
    {
        gameManagerSO.OnLevelComplated += GameManagerSO_OnLevelComplated;
        gameManagerSO.OnFailed += GameManagerSO_OnFailed;
    }

    private void GameManagerSO_OnFailed(object sender, System.EventArgs e)
    {
        StartCoroutine(TweenLevelComplatedCoroutineScaleL(FailPanel));
    }

    private void GameManagerSO_OnLevelComplated(object sender, System.EventArgs e)
    {
        StartCoroutine(TweenLevelComplatedCoroutineScaleL(levelComplatedPanel));
    }

    private void Ýnstance_OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        StartCoroutine(TweenLevelComplatedCoroutineScaleL(levelComplatedPanel));
    }
    private IEnumerator TweenLevelComplatedCoroutineScaleL(GameObject targetUIElement)
    {
        yield return new WaitForSeconds(waitTimeScale);
        targetUIElement.SetActive(true);
        targetUIElement.transform.DOScale(Vector3.one, levelComplateTween);
    }
    private void OnDisable()
    {
        gameManagerSO.OnLevelComplated -= GameManagerSO_OnLevelComplated;
        gameManagerSO.OnFailed -= GameManagerSO_OnFailed;
    }
}
