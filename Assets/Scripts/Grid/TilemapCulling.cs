using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCulling : MonoBehaviour
{
    public Camera mainCamera;       // Camera chính của game
    public int padding = 2;         // Số tile đệm ngoài màn hình để hiển thị thêm

    private Vector3Int minVisibleTile;
    private Vector3Int maxVisibleTile;

    public List<Tilemap> tiles; // Tilemap mà bạn muốn culling
    private Vector3 lastCameraPosition;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;   // Nếu chưa chỉ định, tự động lấy camera chính
        }

        // Lưu vị trí ban đầu của camera
        lastCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        if(tiles.Count > 0)
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                CullingTilemap(tiles[i]);
               
            }
            // Cập nhật vị trí mới của camera
            lastCameraPosition = mainCamera.transform.position;
        }
    }

    void CullingTilemap(Tilemap tilemap)
    {
        // Xác định giới hạn camera (view bounds)
        Vector3 camMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 camMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Chuyển đổi các giới hạn này thành tọa độ của tilemap
        minVisibleTile = tilemap.WorldToCell(camMin) - new Vector3Int(padding, padding, 0);
        maxVisibleTile = tilemap.WorldToCell(camMax) + new Vector3Int(padding, padding, 0);

        // Loop qua tất cả các tile trong tilemap và kiểm tra xem nó có nằm trong tầm nhìn không
        BoundsInt tilemapBounds = tilemap.cellBounds;

        for (int x = tilemapBounds.xMin; x < tilemapBounds.xMax; x++)
        {
            for (int y = tilemapBounds.yMin; y < tilemapBounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                // Nếu tile nằm trong giới hạn camera, hiển thị nó
                if (tilePos.x >= minVisibleTile.x && tilePos.x <= maxVisibleTile.x &&
                    tilePos.y >= minVisibleTile.y && tilePos.y <= maxVisibleTile.y)
                {
                    if (tilemap.HasTile(tilePos))
                    {
                        tilemap.SetTileFlags(tilePos, TileFlags.None);
                        tilemap.SetColor(tilePos, Color.white);  // Đảm bảo hiển thị tile
                    }
                }
                else
                {
                    if (tilemap.HasTile(tilePos))
                    {
                        tilemap.SetTileFlags(tilePos, TileFlags.None);
                        tilemap.SetColor(tilePos, Color.clear);  // Ẩn tile ngoài phạm vi camera
                    }
                }
            }
        }
    }
}
