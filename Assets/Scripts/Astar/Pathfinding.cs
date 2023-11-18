using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public Transform seeker, target;
    public GameObject aStar;
	Grid grid;
    List<GridNode> gridPath;

	void Awake() {
		grid = aStar.GetComponent<Grid> ();
        //grid = GetComponent<Grid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position);
	}

    public List<GridNode> GetPath() {
		return gridPath;
	}

    void FindPath(Vector3 startPos, Vector3 targetPos) {
		GridNode startNode = grid.NodeFromWorldPoint(startPos);
		GridNode targetNode = grid.NodeFromWorldPoint(targetPos);

		List<GridNode> openSet = new List<GridNode>();
		HashSet<GridNode> closedSet = new HashSet<GridNode>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			GridNode node = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode) {
                RetracePath(startNode,targetNode);
				return;
			}

			foreach (GridNode neighbour in grid.GetNeighbours(node)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(GridNode startNode, GridNode endNode) {
		List<GridNode> path = new List<GridNode>();
		GridNode currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
        
		grid.path = path;
        gridPath = path;
	}

	int GetDistance(GridNode nodeA, GridNode nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}