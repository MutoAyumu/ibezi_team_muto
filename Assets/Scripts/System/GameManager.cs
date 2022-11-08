using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameManager
{
    /* TODO
    ポーズの管理
    フェーズの管理
    */

    #region プロパティ
    /// <summary>
    /// インスタンス
    /// </summary>
    public static GameManager Instance => new GameManager();
    /// <summary>
    /// 現在のフェーズ
    /// </summary>
    public IReadOnlyReactiveProperty<int> Phase => _phase;
    public IReadOnlyReactiveProperty<int> Score => _score;

    #endregion

    #region 変数

    IntReactiveProperty _phase = new IntReactiveProperty();
    IntReactiveProperty _score = new IntReactiveProperty();

    bool _isPause;

    #endregion

    #region イベント

    /// <summary>
    /// ポーズ時の処理
    /// </summary>
    public event Action OnPause;

    /// <summary>
    /// ポーズ解除時の処理
    /// </summary>
    public event Action OnResume;

    #endregion

    //コンストラクタ
    public GameManager()
    {
        Debug.Log("New GameManagaer");
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="attachment"></param>
    public void Init(GameManagerAttachment attachment)
    {

    }

    void OnUpdate()
    {
        //とりあえずはポーズ処理だけ
        if(Input.GetButtonDown("Cancel"))
        {
            //ポーズ中
            if (_isPause)
            {
                OnResume?.Invoke();
                Debug.Log("ポーズ解除");
            }
            else
            {
                OnPause?.Invoke();
                Debug.Log("ポーズ開始");
            }

            //逆にする
            _isPause = !_isPause;
        }
    }

    #region コールバック

    public void InitUpdateCallback(GameManagerAttachment attachment)
    {
        attachment.InitCallBack(OnUpdate);
    }

    #endregion
}
