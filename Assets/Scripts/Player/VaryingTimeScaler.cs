using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VaryingTimeScaler : MonoBehaviour
{
    [SerializeField] float _duration = 0.2f;
    [SerializeField] Ease _ease = Ease.Linear;

    [SerializeField] float _delay = 0.4f;

    float _timeScale;

    private void Awake()
    {
        _timeScale = Time.timeScale;
    }

    public void SlowDown()
    {
        DOTween.To(() => _timeScale, value => _timeScale = value, 0.1f, _duration)
            .SetEase(_ease)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(_delay, () => SpeedUp());
            });
    }

    void SpeedUp()
    {
        DOTween.To(() => _timeScale, value => _timeScale = value, 1f, _duration)
            .SetEase(_ease)
            .SetUpdate(true);
    }
    private void Update()
    {
        Time.timeScale = _timeScale;
    }
}
