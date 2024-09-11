using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChunker : MonoBehaviour
{
    public Tilemap tilemap;    // Tilemap gốc cần chia
    public int chunkSize = 10; // Kích thước mỗi chunk (ví dụ 10x10 tiles)
    private int orderInLayer;  // Biến lưu trữ giá trị Order in Layer của tilemap gốc

    void Start()
    {
        // Lưu giá trị Order in Layer của tilemap gốc
        orderInLayer = tilemap.GetComponent<TilemapRenderer>().sortingOrder;

        ChunkTilemap();
    }

    void ChunkTilemap()
    {
        // Lấy vùng giới hạn của tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        // Vòng lặp qua các chunk
        for (int x = bounds.xMin; x < bounds.xMax; x += chunkSize)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y += chunkSize)
            {
                CreateChunk(x, y, chunkSize, allTiles, bounds);
            }
        }

        // Sau khi chia, bạn có thể ẩn tilemap gốc nếu cần
        tilemap.gameObject.SetActive(false);
    }

    void CreateChunk(int startX, int startY, int chunkSize, TileBase[] allTiles, BoundsInt bounds)
    {
        // Tạo đối tượng mới cho mỗi chunk
        GameObject chunk = new GameObject("Chunk_" + startX + "_" + startY);
        chunk.transform.parent = transform;

        // Thêm Grid và Tilemap components vào chunk
        Grid chunkGrid = chunk.AddComponent<Grid>();
        Tilemap chunkTilemap = chunk.AddComponent<Tilemap>();
        TilemapRenderer renderer = chunk.AddComponent<TilemapRenderer>();

        // Sao chép giá trị Order in Layer từ tilemap gốc
        renderer.sortingOrder = orderInLayer;

        // Đánh dấu chunk là Static để Unity thực hiện Occlusion Culling
        chunk.isStatic = true;

        // Gán tile vào chunk
        TileBase[] chunkTiles = new TileBase[chunkSize * chunkSize];
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                int tileX = startX + x;
                int tileY = startY + y;

                if (tileX < bounds.xMax && tileY < bounds.yMax)
                {
                    int index = (tileX - bounds.xMin) + (tileY - bounds.yMin) * bounds.size.x;
                    chunkTilemap.SetTile(new Vector3Int(tileX, tileY, 0), allTiles[index]);
                }
            }
        }
    }
}
