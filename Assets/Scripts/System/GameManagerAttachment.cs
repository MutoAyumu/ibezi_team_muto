using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    #region ƒfƒŠƒQ[ƒg
    public delegate void MonoEvent();
    MonoEvent _updateEvent;
    #endregion

    private void Awake()
    {
        GameManager.Instance.InitUpdateCallback(this);
        GameManager.Instance.Init(this);
    }
    private void Update()
    {
        _updateEvent?.Invoke();
    }

    /// <summary>
    /// Update‚ÅŒÄ‚Ñ‚½‚¢ˆ—‚ğ“o˜^‚µ‚Ä‚¨‚­
    /// </summary>
    public void InitCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
}
