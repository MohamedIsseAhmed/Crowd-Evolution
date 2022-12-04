using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private float levelComplateTween = 0.1f;
    [SerializeField] private float waitTimeScale = 1f;

    private void Start()
    {
        LastEnemyTracker.instance.OnAllEnemiesDied += Ưnstance_OnAllEnemiesDied;
    }

    private void Ưnstance_OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        StartCoroutine(TweenLevelComplatedCoroutineScaleL());
    }
    private IEnumerator TweenLevelComplatedCoroutineScaleL()
    {
        yield return new WaitForSeconds(waitTimeScale);
        levelComplatedPanel.SetActive(true);
        levelComplatedPanel.transform.DOScale(Vector3.one, levelComplateTween);
    }
}
