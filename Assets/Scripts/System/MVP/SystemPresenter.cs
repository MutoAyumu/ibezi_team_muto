using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SystemPresenter : MonoBehaviour
{
    [SerializeField] MVPText _phaseText;
    [SerializeField] MVPText _scoreText;

    private void Start()
    {
        if(_phaseText)
        {
            GameManager.Instance.Phase.Subscribe(x =>
            {
                _phaseText.SetText($"フェーズ : {x:00}");
            }).AddTo(this);
        }

        if(_scoreText)
        {
            GameManager.Instance.Score.Subscribe(x =>
            {
                _scoreText.SetText($"スコア : {x:000000}");
            }).AddTo(this);
        }
    }
}
