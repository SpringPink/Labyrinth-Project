using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeNode nodePrefab;
    [SerializeField] private Vector2Int mazeSize;
    [SerializeField] private float nodeSize;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Trident;
    [SerializeField] private GameObject Ennemi;
    [SerializeField] private float heightOffset = 2.0f;
    private float distance;
    private MazeNode spawnNode = null;
    private int maxAttempts = 100;
    private int attempts = 0;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(GenerateMaze(mazeSize));
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Cr�er les Nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f))*nodeSize;
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                newNode.transform.localScale = Vector3.one * nodeSize;
                nodes.Add(newNode);

                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Starting Nodes
        MazeNode startingNode = nodes[Random.Range(0, nodes.Count)];
        currentPath.Add(startingNode);

        // Instantiate Player at first node location
        Instantiate(Player, startingNode.transform.position, Quaternion.identity);

        // Choose random node dif from start
        do
        {
            spawnNode = nodes[Random.Range(0, nodes.Count)];

            // Calcul Start Node to Dif Node
            distance = Vector3.Distance(startingNode.transform.position, spawnNode.transform.position);

            attempts++;
            if (attempts > maxAttempts)
            {
                Debug.LogWarning("Al�atoire.");
                break;
            }

        } while (spawnNode == startingNode || distance < 7 * nodeSize);

        // Positionnement du Trident
        Vector3 spawnPosition = spawnNode.transform.position + new Vector3(0, heightOffset, 0);
        Instantiate(Trident, spawnPosition, Quaternion.identity);
        Instantiate(Ennemi, spawnNode.transform.position, Quaternion.identity);

        while (completedNodes.Count < nodes.Count)
        {
            // Check Nodes Next to Current Nodes
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.x;

            if (currentNodeX < size.x - 1)
            {
                // Check Nodes to the right of Current Nodes
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }

            if (currentNodeX > 0)
            {
                // Check Nodes to the left of Current Nodes
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }

            if (currentNodeY < size.y - 1)
            {
                // Check Nodes above the Current Nodes
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                // Check Nodes below the Current Nodes
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;

                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}
