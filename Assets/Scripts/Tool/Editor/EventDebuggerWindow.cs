using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class EventDebuggerWindow : EditorWindow
{
    private Vector2 scrollPos; // Vị trí cuộn của bảng
    private Dictionary<string, Delegate> eventDictionary;

    [MenuItem("Tools/Event Debugger")]
    public static void ShowWindow()
    {
        GetWindow<EventDebuggerWindow>("Event Debugger");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh Events List"))
        {
            RefreshEventList();
        }

        if (eventDictionary != null && eventDictionary.Count > 0)
        {
            GUILayout.Label("Active Events", EditorStyles.boldLabel);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300));

            // Tạo bảng hiển thị các sự kiện và số lượng listener
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Event Name", EditorStyles.miniBoldLabel, GUILayout.Width(200));
            GUILayout.Label("Listener Count", EditorStyles.miniBoldLabel, GUILayout.Width(100));
            GUILayout.Label("Delegate Type", EditorStyles.miniBoldLabel, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();

            foreach (var entry in eventDictionary)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(entry.Key, GUILayout.Width(200));

                int listenerCount = entry.Value != null ? entry.Value.GetInvocationList().Length : 0;
                GUILayout.Label(listenerCount.ToString(), GUILayout.Width(100));

                GUILayout.Label(entry.Value.GetType().Name, GUILayout.Width(200)); // Hiển thị loại delegate
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("No active events to display. Please refresh.", EditorStyles.helpBox);
        }
    }

    private void RefreshEventList()
    {
        // Lấy thông tin sự kiện từ GenericEventManager
        eventDictionary = GetActiveEvents();
    }

    private Dictionary<string, Delegate> GetActiveEvents()
    {
        // Sử dụng Reflection để truy cập vào eventDictionary từ GenericEventManager
        var fieldInfo = typeof(GenericEventManager).GetField("eventDictionary", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        return fieldInfo?.GetValue(null) as Dictionary<string, Delegate>;
    }
}
