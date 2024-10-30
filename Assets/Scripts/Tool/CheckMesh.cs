using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMesh : MonoBehaviour
{
    [SerializeField] private GameObject objectA; // Đối tượng đầu tiên
    [SerializeField] private GameObject objectB; // Đối tượng thứ hai

    void Start()
    {
        // Lấy mesh từ các MeshFilter của hai đối tượng
        Mesh meshA = objectA.GetComponent<MeshFilter>().sharedMesh;
        Mesh meshB = objectB.GetComponent<MeshFilter>().sharedMesh;

        // Kiểm tra xem hai mesh có giống nhau không
        if (meshA == meshB)
        {
            Debug.Log("Check Mesh - Hai đối tượng có cùng mesh.");
        }
        else
        {
            Debug.Log("Check Mesh - Hai đối tượng có mesh khác nhau.");
        }
    }
}
