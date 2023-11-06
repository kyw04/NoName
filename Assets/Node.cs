using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Puzzle puzzle;
    public NodeBase nodeBase;
    public float maxDistance;
    private float moveDistance;
    public Image image;
    public int x, y;
    public float size = 100f;
    private Node target = null;

    private void Start()
    {
        if (nodeBase != null)
            image.color = nodeBase.color;
    }

    public void Change(Node node)
    {
        NodeBase temp = this.nodeBase;
        this.nodeBase = node.nodeBase;
        node.nodeBase = temp;
        this.image.color = nodeBase.color;
        node.image.color = node.nodeBase.color;
    }

    public void SetNode(Node node)
    {
        this.nodeBase = node.nodeBase;
        this.image.color = node.image.color;
    }

    public void SetNode(NodeBase nodeBase)
    {
        this.nodeBase = nodeBase;
        this.image.color = nodeBase.color;
    }

    public void SetIndex(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public void Hold()
    {
        if (puzzle.puzzleState != Puzzle.PuzzleState.Idle)
            return;

        gameObject.layer = LayerMask.NameToLayer("Node");
        Vector3 moveDirection = Input.mousePosition - transform.parent.position;
        moveDistance = Vector3.Distance(Input.mousePosition, transform.parent.position);
        moveDistance = maxDistance < moveDistance ? maxDistance : moveDistance;
        transform.position = moveDirection.normalized * moveDistance + transform.parent.position;

        if (moveDistance > maxDistance * 0.5f)
        {
            int[] move = { 0, 1, -1 };
            float halfSize = size * 0.5f;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int newX = move[i] + x;
                    int newY = move[j] + y;
                    if (newX > 4 || newY > 4 || newX < 0 || newY < 0 || (newX == x && newY == y))
                        continue;

                    Collider[] colliders;
                    int index = newX + newY * 5;
                    //Debug.Log($"{newX}, {newY}");

                    if (Mathf.Abs(move[i]) == Mathf.Abs(move[j]))
                    {
                        colliders = Physics.OverlapBox(puzzle.pos[index].position, Vector3.one * size * 0.5f, Quaternion.identity, LayerMask.GetMask("Node"));
                    }
                    else
                    {
                        float sizeX = halfSize * Mathf.Abs(move[i]) + halfSize;
                        float sizeY = halfSize * Mathf.Abs(move[j]) + halfSize;
                        colliders = Physics.OverlapBox(puzzle.pos[index].position, new Vector3(sizeX, sizeY) * 0.5f, Quaternion.identity, LayerMask.GetMask("Node"));
                    }

                    if (colliders.Length > 0)
                    {
                        if (target)
                            target.transform.position = target.transform.parent.position;

                        target = puzzle.pos[index].GetComponentInChildren<Node>();
                        target.transform.position = transform.parent.position;
                        //Debug.Log($"{newX}, {newY}");
                    }
                }
            }
        }
        else
        {
            if (target)
                target.transform.position = target.transform.parent.position;
            target = null;
        }
    }

    public void Put()
    {
        gameObject.layer = LayerMask.NameToLayer("UI");
        if (moveDistance > maxDistance * 0.5f && puzzle.changeCount > 0)
        {
            puzzle.UseCount();
            Change(target);
        }
        
        if (target)
        {
            target.transform.position = target.transform.parent.position;
        }
        transform.position = transform.parent.position;
        moveDistance = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.parent == null)
            return;

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.parent.position, moveDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.parent.position, transform.position);

        Gizmos.color = Color.white;
        for (int i = 1; i <= 8; i++)
        {
            float rad = i * 45f * Mathf.Deg2Rad;
            Gizmos.DrawLine(transform.parent.position, new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * moveDistance + transform.parent.position);
        }

        int[] move = { 0, 1, -1 };
        const float size = 100f;
        const float halfSize = size * 0.5f;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int newX = move[i] + x;
                int newY = move[j] + y;
                if (newX > 4 || newY > 4 || newX < 0 || newY < 0 || (newX == x && newY == y))
                    continue;

                int index = newX + newY * 5;
                //Debug.Log($"{newX}, {newY}");

                if (Mathf.Abs(move[i]) == Mathf.Abs(move[j]))
                {
                    Gizmos.DrawWireCube(puzzle.pos[index].position, Vector3.one * size);
                }
                else
                {
                    float sizeX = halfSize * Mathf.Abs(move[i]) + halfSize;
                    float sizeY = halfSize * Mathf.Abs(move[j]) + halfSize;
                    Gizmos.DrawWireCube(puzzle.pos[index].position, new Vector3(sizeX, sizeY));
                }
            }
        }
    }
}
