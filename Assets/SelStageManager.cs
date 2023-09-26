using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelStageManager : MonoBehaviour
{
    public enum ConnectState
    {
        Connecting,
        End,
    }
    public ConnectState state = ConnectState.End;
    public NodeLineConnector currentStage;
    public List<NodeLineConnector> Stages = new List<NodeLineConnector>();
    public LineRenderer linePrefab;
    public LineRenderer line;
    public float duration = 0.5f;


    // Start is called before the first frame update


    private void ConnectLine()
    {


        Debug.Log(transform.name + " �������� ȣ��");

    }

    public void ConnectLIneNode(NodeLineConnector childNode)
    {
        NodeLineConnector parNode = currentStage;
        if (state != ConnectState.End) { Debug.Log("���� ���ÿ� ������ �ʽ��ϴ�."); return; }   
        state = ConnectState.Connecting;
        if (!parNode.connectors.Contains(childNode)) { Debug.Log("�ش� ���������� ���� �� �� �����ϴ�. " + parNode.name + " to " + childNode.name);  return; }


        Vector3 plusPos = new Vector3(0.5f, 0.5f, 0);
        Vector3 startPos = parNode.transform.position + plusPos;
        Vector3 endPos = childNode.transform.position + plusPos;
        line = Instantiate(linePrefab, parNode.transform);
        line.positionCount = 2;
        line.SetPosition(0, startPos);
        StartCoroutine(DrawLineOverTime(startPos, endPos, duration));
        currentStage = childNode;

    }
    IEnumerator DrawLineOverTime(Vector3 start, Vector3 end, float duration)
    {
        
        float timePassed = 0;
        line.positionCount = 2;   // �������� �����̹Ƿ� ����Ʈ 2�� �ʿ�
        line.SetPosition(0, start);

        while (timePassed < duration)
        {
            float t = timePassed / duration;
            Vector3 currentPoint = Vector3.Lerp(start, end, t);
            line.SetPosition(1, currentPoint);

            timePassed += Time.deltaTime;
            yield return null;
        }

        line.SetPosition(1, end); // ���������� ������ Ȯ���� ��������
        line = null;
        state = ConnectState.End;
        yield break;
    }

    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
