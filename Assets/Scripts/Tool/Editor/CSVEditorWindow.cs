using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions; // Thêm để xử lý regex cho CSV

public class CSVEditorWindow : EditorWindow
{
    private string csvFilePath;
    private List<List<string>> csvData = new List<List<string>>();
    private List<float> columnWidths = new List<float>(); // Danh sách lưu chiều rộng của các cột

    private bool isResizingColumn = false;
    private int resizingColumnIndex = -1;
    private Vector2 previousMousePosition;

    [MenuItem("Tools/CSV Editor")]
    public static void ShowWindow()
    {
        CSVEditorWindow window = GetWindow<CSVEditorWindow>("CSV Editor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV File Path", EditorStyles.boldLabel);
        csvFilePath = EditorGUILayout.TextField("Path:", csvFilePath);

        if (GUILayout.Button("Load CSV"))
        {
            LoadCSVFile();
        }

        if (csvData.Count > 0)
        {
            DrawCSVData();
        }

        if (csvData.Count > 0 && GUILayout.Button("Save CSV"))
        {
            SaveCSVFile();
        }
    }

    private void LoadCSVFile()
    {
        if (File.Exists(csvFilePath))
        {
            csvData.Clear();
            columnWidths.Clear(); // Xóa danh sách chiều rộng cũ khi tải file mới
            string[] lines = File.ReadAllLines(csvFilePath);

            foreach (string line in lines)
            {
                List<string> cells = ParseCSVLine(line);
                csvData.Add(cells);

                // Khởi tạo chiều rộng mặc định cho các cột
                while (columnWidths.Count < cells.Count)
                {
                    columnWidths.Add(100f); // Giá trị mặc định là 100
                }
            }

            Debug.Log("CSV loaded successfully.");
        }
        else
        {
            Debug.LogError("File not found at path: " + csvFilePath);
        }
    }

    private List<string> ParseCSVLine(string line)
    {
        List<string> cells = new List<string>();
        MatchCollection matches = Regex.Matches(line, @"(?<=^|,)(\""(?:[^\""]|\""\"")*\""|[^,]*)");

        foreach (Match match in matches)
        {
            string value = match.Value;

            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2).Replace("\"\"", "\"");
            }

            value = Regex.Replace(value, @"(?<=\d),(?=\d)", ".");

            if (float.TryParse(value, out float floatValue))
            {
                value = floatValue.ToString();
            }

            cells.Add(value);
        }

        return cells;
    }

    private void DrawCSVData()
    {
        EditorGUILayout.Space();
        GUILayout.Label("CSV Data", EditorStyles.boldLabel);

        for (int row = 0; row < csvData.Count; row++)
        {
            EditorGUILayout.BeginHorizontal(); // Bắt đầu hàng

            for (int col = 0; col < csvData[row].Count; col++)
            {
                // Hiển thị ô dữ liệu với chiều rộng thay đổi được
                csvData[row][col] = EditorGUILayout.TextField(csvData[row][col], GUILayout.Width(columnWidths[col]));

                // Vẽ đường phân cách giữa các cột để người dùng kéo
                Rect dividerRect = GUILayoutUtility.GetRect(5f, 20f, GUILayout.ExpandHeight(true));
                EditorGUIUtility.AddCursorRect(dividerRect, MouseCursor.ResizeHorizontal);

                // Kiểm tra sự kiện chuột cho việc kéo thả
                if (Event.current.type == EventType.MouseDown && dividerRect.Contains(Event.current.mousePosition))
                {
                    isResizingColumn = true;
                    resizingColumnIndex = col;
                    previousMousePosition = Event.current.mousePosition;
                }
            }

            GUILayout.FlexibleSpace(); // Đảm bảo các ô căn chỉnh về phía bên trái
            EditorGUILayout.EndHorizontal(); // Kết thúc hàng
        }

        // Cập nhật chiều rộng của cột khi đang kéo
        if (isResizingColumn && Event.current.type == EventType.MouseDrag)
        {
            float delta = Event.current.mousePosition.x - previousMousePosition.x;
            columnWidths[resizingColumnIndex] = Mathf.Max(20f, columnWidths[resizingColumnIndex] + delta);
            previousMousePosition = Event.current.mousePosition;
            Repaint();
        }

        // Kết thúc việc kéo thả
        if (Event.current.type == EventType.MouseUp)
        {
            isResizingColumn = false;
            resizingColumnIndex = -1;
        }
    }

    private void SaveCSVFile()
    {
        List<string> lines = new List<string>();

        foreach (var row in csvData)
        {
            List<string> lineData = new List<string>();

            foreach (var cell in row)
            {
                string formattedCell = cell.Contains(",") ? cell.Replace(",", ".") : cell;
                formattedCell = formattedCell.Contains(",") || formattedCell.Contains(".") ? $"\"{formattedCell}\"" : formattedCell;
                lineData.Add(formattedCell);
            }

            lines.Add(string.Join(",", lineData));
        }

        File.WriteAllLines(csvFilePath, lines);
        Debug.Log("CSV saved successfully.");
    }
}
