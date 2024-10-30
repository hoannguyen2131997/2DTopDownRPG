using System;
using System.Collections.Generic;

public static class GenericEventManager
{
    // Dictionary lưu trữ các Action với kiểu dữ liệu cụ thể
    private static Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

    // Đăng ký sự kiện với kiểu dữ liệu cụ thể
    public static void Subscribe<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T>)
            {
                eventDictionary[eventName] = (Action<T>)existingDelegate + listener;
            }
            else
            {
                // Cảnh báo nếu loại sự kiện không khớp
                throw new InvalidOperationException($"Event {eventName} has a different delegate type.");
            }
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    // Hủy đăng ký sự kiện với kiểu dữ liệu cụ thể
    public static void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T>)
            {
                eventDictionary[eventName] = (Action<T>)existingDelegate - listener;
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName); // Xóa sự kiện khỏi từ điển nếu không còn listener nào
                }
            }
        }
    }

    // Kích hoạt sự kiện với kiểu dữ liệu cụ thể
    public static void RaiseEvent<T>(string eventName, T eventData)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T> action)
            {
                action.Invoke(eventData);
            }
        }
    }

    // Đăng ký sự kiện với callback kiểu dữ liệu cụ thể
    public static void Subscribe<T, U>(string eventName, Action<T, Action<U>> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T, Action<U>>)
            {
                eventDictionary[eventName] = (Action<T, Action<U>>)existingDelegate + listener;
            }
            else
            {
                throw new InvalidOperationException($"Event {eventName} has a different delegate type.");
            }
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    // Hủy đăng ký sự kiện với callback kiểu dữ liệu cụ thể
    public static void Unsubscribe<T, U>(string eventName, Action<T, Action<U>> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T, Action<U>>)
            {
                eventDictionary[eventName] = (Action<T, Action<U>>)existingDelegate - listener;
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }
    }

    // Kích hoạt sự kiện với dữ liệu và callback để xử lý kết quả trả về
    public static void RaiseEvent<T, U>(string eventName, T eventData, Action<U> resultCallback)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate existingDelegate))
        {
            if (existingDelegate is Action<T, Action<U>> action)
            {
                action.Invoke(eventData, resultCallback);
            }
        }
    }
}
