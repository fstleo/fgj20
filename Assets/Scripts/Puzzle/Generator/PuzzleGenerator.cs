using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    [SerializeField]
    private PickableItem[] _itemsPool;

    [SerializeField]
    private GameObject _itemDragPlane;

    private void Start()
    {
        int width = 4;
        int height = 4;
        float gridStep = 1;
        var puzzleCellDescs = GenerateLayer(width, height);
        GameObject puzzle = new GameObject("Puzzle");
        GameObject layer1 = new GameObject("Layer1");
        layer1.transform.parent = puzzle.transform;
        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                PuzzleCellDesc desc = puzzleCellDescs[i, j];
                if (desc.item == null)
                {
                    continue;
                }

                PickableItem item = Instantiate(desc.item);
                item.transform.parent = layer1.transform;
                item.transform.position = new Vector3((i - width / 2f) * gridStep, 0f, j * gridStep);
                if (desc.isVertical)
                {
                    item.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }
            }
        }

        GameObject dragPlane = Instantiate(_itemDragPlane);
        dragPlane.transform.parent = puzzle.transform;
        dragPlane.transform.position = layer1.transform.position + Vector3.up * gridStep;
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