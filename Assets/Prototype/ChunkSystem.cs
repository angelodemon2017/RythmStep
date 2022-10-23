using System.Collections.Generic;
using UnityEngine;

public class ChunkSystem : MonoBehaviour
{
    public Chunk prefabChunk;

    public List<Chunk> chunks;

    private Vector2 currentCenter = new Vector2(0, 0);
    Vector3 tempVect;

    void Start()
    {
        Add(-1, -1);
        Add(0, -1);
        Add(1, -1);
        Add(-1, 1);
        Add(0, 1);
        Add(1, 1);
        Add(-1, 0);
        Add(1, 0);
    }

    private void Add(int hor, int vert)
    {
        tempVect.x = currentCenter.x + (hor == 0 ? 0 : (hor == 1 ? 20 : -20));
        tempVect.y = currentCenter.y + (vert == 0 ? 0 : (vert == 1 ? 20 : -20));

        chunks.Add(Instantiate(prefabChunk, tempVect, Quaternion.identity));
    }

    private Direct tempDirect;

    public void Move(Vector2 stepPoint) 
    {
        tempDirect = Direct.none;

        if (stepPoint.x > currentCenter.x && (stepPoint.x - currentCenter.x) > 10f)
        {
            tempDirect = Direct.right;
        }
        if (stepPoint.x < currentCenter.x && (currentCenter.x - stepPoint.x) > 10f)
        {
            tempDirect = Direct.left;
        }
        if (stepPoint.y > currentCenter.y && (stepPoint.y - currentCenter.y) > 10f)
        {
            tempDirect = Direct.up;
        }
        if (stepPoint.y < currentCenter.y && (currentCenter.y - stepPoint.y) > 10f)
        {
            tempDirect = Direct.down;
        }

        Remove(tempDirect);
        ChangeCenterPoint(tempDirect);
        AddChunks(tempDirect);
    }

    private void AddChunks(Direct directAdding) 
    {
        switch (directAdding)
        {
            case Direct.up:
                Add(-1, 1);
                Add(0, 1);
                Add(1, 1);
                break;
            case Direct.left:
                Add(-1, -1);
                Add(-1, 0);
                Add(-1, 1);
                break;
            case Direct.right:
                Add(1, -1);
                Add(1, 0);
                Add(1, 1);
                break;
            case Direct.down:
                Add(-1, -1);
                Add(0, -1);
                Add(1, -1);
                break;
        }
    }

    private void ChangeCenterPoint(Direct directNewCenter) 
    {
        switch (directNewCenter)
        {
            case Direct.up:
                currentCenter += Vector2.up * 20;
                break;
            case Direct.left:
                currentCenter += Vector2.left * 20;
                break;
            case Direct.right:
                currentCenter += Vector2.right * 20;
                break;
            case Direct.down:
                currentCenter += Vector2.down * 20;
                break;
        }
    }

    private List<Chunk> chunksForRemove = new List<Chunk>();
    private void Remove(Direct directRemoving) 
    {
        chunksForRemove.Clear();
        foreach (var chunk in chunks)
        {
            switch (directRemoving)
            {
                case Direct.up:
                    if (chunk.transform.position.y < currentCenter.y - 1f)
                    {
                        chunksForRemove.Add(chunk);
                        Destroy(chunk.gameObject);
                    }
                    break;
                case Direct.left:
                    if (chunk.transform.position.x > currentCenter.x + 1f)
                    {
                        chunksForRemove.Add(chunk);
                        Destroy(chunk.gameObject);
                    }
                    break;
                case Direct.right:
                    if (chunk.transform.position.x < currentCenter.x - 1f)
                    {
                        chunksForRemove.Add(chunk);
                        Destroy(chunk.gameObject);
                    }
                    break;
                case Direct.down:
                    if (chunk.transform.position.y > currentCenter.y + 1f)
                    {
                        chunksForRemove.Add(chunk);
                        Destroy(chunk.gameObject);
                    }
                    break;
            }
        }
        foreach (var cr in chunksForRemove)
            chunks.Remove(cr);
    }
}

enum Direct 
{
    none,
    up,
    left,
    right,
    down
}