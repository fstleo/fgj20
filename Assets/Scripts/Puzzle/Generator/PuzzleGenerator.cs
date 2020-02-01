using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    private const float GRID_STEP = 1f;

    [SerializeField]
    private PickableItem[] _itemsPool;

    [SerializeField]
    private GameObject _itemDragPlane;

    public GameObject GeneratePuzzle(int width, int height, int layers)
    {
        GameObject puzzle = new GameObject("Puzzle");
        GameObject topLayer = null;
        for (int layerIndex = 0; layerIndex < layers; ++layerIndex)
        {
            var puzzleCellDescs = GenerateLayer(width, height);
            GameObject layer = new GameObject("Layer" + layerIndex);
            topLayer = layer;
            layer.transform.parent = puzzle.transform;
            layer.transform.position = layerIndex * GRID_STEP * Vector3.up;
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
                    item.transform.parent = layer.transform;
                    item.transform.localPosition = new Vector3((i - width / 2f) * GRID_STEP, 0f, j * GRID_STEP);
                    if (desc.isVertical)
                    {
                        item.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                    }
                }
            }
        }

        GameObject dragPlane = Instantiate(_itemDragPlane);
        dragPlane.transform.parent = puzzle.transform;
        dragPlane.transform.position = topLayer.transform.position + Vector3.up * GRID_STEP;
        return puzzle;
    }

    private PuzzleCellDesc[,] GenerateLayer(int width, int height)
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