using UniRx;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuddlerFunction : MonoBehaviour
{
    [SerializeField] float _maxLength;
    [SerializeField] Transform _cullentSize = null;
    [SerializeField] Transform _center = null;
    [SerializeField] LayerMask _enemyLayer = ~0;
    Sequence _sequence;
    Sequence _sequence2;


    private void Start()
    {
        MuddlerLengthMax(_maxLength, 2f);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            MuddlerRotation(1f);
            _sequence.Pause();
        }
    }
    /// <summary>
    /// マドラーの伸縮
    /// </summary>
    /// <param name="min"></param>マドラーの最短尺
    /// <param name="max"></param>マドラーの最長尺
    /// <param name="interval"></param>伸縮にかける時間
    public void MuddlerLengthMax(float maxLength, float interval)
    {
        _sequence = DOTween.Sequence();

        var tweener1 = _cullentSize.DOScaleY(maxLength, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        var tweener2 = _cullentSize.DOLocalMoveY(maxLength / 2, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _sequence.Append(tweener1)
            .Join(tweener2);
    }
    /// <summary>
    /// マドラーの回転
    /// </summary>
    /// <param name="interval"></param>一回転に掛かる時間
    public void MuddlerRotation(float interval)
    {
        _sequence2 = DOTween.Sequence();
        var tweener = _center.DORotate(new Vector3(0f, 0f, -360f), interval, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() => _sequence.Play());
        _sequence2.Append(tweener);
    }
    /// <summary>
    /// マドラーの当たり判定
    /// </summary>
    void HitAction()
    {
        Collider[] enemiesInRenge = Physics.OverlapBox(_center.position, this.transform.localScale, Quaternion.identity, _enemyLayer);
        if (enemiesInRenge.Length > 0)
        {
            foreach (Collider enemy in enemiesInRenge)
            {
                //if (enemy.TryGetComponent<EnemyStatus>(out EnemyStatus enemyHp))
                //{
                //    enemyHp.DamageMethod(_rigitPower, _firePower, _elekePower, _frozenPower);
                //}
            }
        }
    }
}
