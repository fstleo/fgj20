using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    [SerializeField]
    private PickableItem[] _itemsPool;

    private void Start()
    {
        int width = 4;
        int height = 4;
        var puzzleCellDescs = GenerateLayer(width, height);
        GameObject puzzle = new GameObject("Puzzle");
        GameObject layer1 = new GameObject("Layer1");
        layer1.transform.parent = puzzle.transform;
        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                
            }
        }
    }

    public PuzzleCellDesc[,] GenerateLayer(int width, int height)
    {
        PuzzleCellDesc[,] result = new PuzzleCellDesc[width, height];
        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                if (result[i, j] != null)
                {
                    continue;
                }

                bool isVertical = Random.Range(0, 2) == 0;
                
                int maxSize = 0;
                while (true)
                {
                    ++maxSize;
                    int offsetI = i + Convert.ToInt32(!isVertical) * maxSize;
                    int offsetJ = j + Convert.ToInt32(isVertical) * maxSize;
                    if (offsetI >= width || offsetJ >= height || result[offsetI, offsetJ] != null)
                    {
                        break;
                    }
                }
                
                PickableItem[] availableItems = _itemsPool.Where(pi => pi.size <= maxSize).ToArray();
                PickableItem item = availableItems[Random.Range(0, availableItems.Length)];
                result[i, j] = new PuzzleCellDesc
                {
                    item = item,
                    isVertical = isVertical
                };
                for (int k = 1; k < item.size; ++k)
                {
                    result[i + Convert.ToInt32(!isVertical) * k, j + Convert.ToInt32(isVertical) * k] = new PuzzleCellDesc
                    {
                        item = null,
                        isVertical = isVertical,
                    };
                }
            }
        }

        return result;
    }
}