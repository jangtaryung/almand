using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 중앙 이벤트 시스템. 시스템 간 직접 참조 없이 메시지를 주고받습니다.
/// 예: 적 사망 → 경험치 획득, 킬카운트, 아이템 드롭 등이 동시에 반응.
/// </summary>
public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Action> _events = new Dictionary<string, Action>();
    private Dictionary<string, Delegate> _typedEvents = new Dictionary<string, Delegate>();

    /// <summary>
    /// 파라미터 없는 이벤트 구독.
    /// </summary>
    public void Subscribe(string eventName, Action listener)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] += listener;
        }
        else
        {
            _events[eventName] = listener;
        }
    }

    /// <summary>
    /// 파라미터 없는 이벤트 구독 해제.
    /// </summary>
    public void Unsubscribe(string eventName, Action listener)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] -= listener;

            if (_events[eventName] == null)
            {
                _events.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// 파라미터 없는 이벤트 발행.
    /// </summary>
    public void Publish(string eventName)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName]?.Invoke();
        }
    }

    /// <summary>
    /// 데이터를 담아서 보내는 이벤트 구독.
    /// 예: Subscribe&lt;int&gt;("OnExpGained", amount => { ... });
    /// </summary>
    public void Subscribe<T>(string eventName, Action<T> listener)
    {
        if (_typedEvents.ContainsKey(eventName))
        {
            _typedEvents[eventName] = Delegate.Combine(_typedEvents[eventName], listener);
        }
        else
        {
            _typedEvents[eventName] = listener;
        }
    }

    /// <summary>
    /// 데이터 이벤트 구독 해제.
    /// </summary>
    public void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        if (_typedEvents.ContainsKey(eventName))
        {
            _typedEvents[eventName] = Delegate.Remove(_typedEvents[eventName], listener);

            if (_typedEvents[eventName] == null)
            {
                _typedEvents.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// 데이터를 담아서 이벤트 발행.
    /// 예: Publish("OnExpGained", 50);
    /// </summary>
    public void Publish<T>(string eventName, T data)
    {
        if (_typedEvents.ContainsKey(eventName))
        {
            (_typedEvents[eventName] as Action<T>)?.Invoke(data);
        }
    }

    /// <summary>
    /// 모든 이벤트 구독 해제. 씬 전환 시 호출 권장.
    /// </summary>
    public void Clear()
    {
        _events.Clear();
        _typedEvents.Clear();
    }
}