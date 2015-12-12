using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    private static readonly List<Apple> apples = new List<Apple>();

    [SerializeField]
    private GameObject applePrefab = null;

    [SerializeField]
    private Transform minPositionTransform = null;

    [SerializeField]
    private Transform maxPositionTransform = null;

    private List<SpawnNode> spawnNodes;

    public static List<Apple> Apples
    {
        get
        {
            return apples;
        }
    }

    public void StartSpawner()
    {
        StartCoroutine(Spawner());
    }

    public void StopSpawner()
    {
        StopAllCoroutines();
    }

    protected virtual void OnDrawGizmos()
    {
        if (spawnNodes != null)
        {
            for (int i = 0; i < spawnNodes.Count; ++i)
            {
                SpawnNode node = spawnNodes[i];

                Gizmos.color = node.IsOccupied ? Color.red : Color.green;
                Gizmos.DrawSphere(node.Position, 0.1f);
            }
        }
    }


    protected virtual void Start()
    {
        spawnNodes = GetSpawnNodes(10, 10, minPositionTransform.position, maxPositionTransform.position);
    }

    private SpawnNode GetRandomNode()
    {
        SpawnNode node = null;

        if (applePrefab != null && spawnNodes != null && spawnNodes.Count > 0)
        {
            for (int i = 0; i < 10; ++i)
            {
                node = spawnNodes[Random.Range(0, spawnNodes.Count)];

                if (node.IsOccupied == false)
                {
                    break;
                }
                node = null;
            }
        }

        return node;
    }

    private List<SpawnNode> GetSpawnNodes(int columns, int rows, Vector2 min, Vector2 max)
    {
        List<SpawnNode> nodes = new List<SpawnNode>();
        if (columns > 1 && rows > 1 && max.x > min.x && max.y > min.y)
        {
            float xSpacing = (max.x - min.x) / (columns - 1);
            float ySpacing = (max.y - min.y) / (rows - 1);

            for (int y = 0; y < rows; ++y)
            {
                for (int x = 0; x < columns; ++x)
                {
                    float xPos = min.x + (xSpacing * x) + Random.Range(-0.1f, 0.1f);
                    float yPos = min.y + (ySpacing * y) + Random.Range(-0.1f, 0.1f);

                    nodes.Add(new SpawnNode(new Vector2(xPos, yPos)));
                }
            }
        }
        return nodes;
    }

    private void SpawnApple()
    {
        if (applePrefab != null && spawnNodes != null && spawnNodes.Count > 0)
        {
            SpawnNode node = GetRandomNode();
            if (node != null)
            {
                GameObject appleObject = Instantiate(applePrefab, node.Position, Quaternion.identity) as GameObject;
                if (appleObject != null)
                {
                    Apple apple = appleObject.GetComponent<Apple>();
                    if (apple != null)
                    {
                        apple.SpawnNode = node;
                    }
                }
            }
        }
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            SpawnApple();
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}