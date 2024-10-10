using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Bước 1: Tạo một Attribute cho biến ReadOnly
public class ReadOnlyAttribute : PropertyAttribute
{
}

// Bước 2: Tạo một Drawer để kiểm soát cách hiển thị trong Inspector
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Vô hiệu hóa để biến không thể chỉnh sửa
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true; // Bật lại tính năng chỉnh sửa
    }
}
#endif
