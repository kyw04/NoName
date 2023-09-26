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


        Debug.Log(transform.name + " 스테이지 호출");

    }

    public void ConnectLIneNode(NodeLineConnector childNode)
    {
        NodeLineConnector parNode = currentStage;
        if (state != ConnectState.End) { Debug.Log("선이 동시에 생기지 않습니다."); return; }   
        state = ConnectState.Connecting;
        if (!parNode.connectors.Contains(childNode)) { Debug.Log("해당 스테이지애 접근 할 수 없습니다. " + parNode.name + " to " + childNode.name);  return; }


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
        line.positionCount = 2;   // 시작점과 끝점이므로 포인트 2개 필요
        line.SetPosition(0, start);

        while (timePassed < duration)
        {
            float t = timePassed / duration;
            Vector3 currentPoint = Vector3.Lerp(start, end, t);
            line.SetPosition(1, currentPoint);

            timePassed += Time.deltaTime;
            yield return null;
        }

        line.SetPosition(1, end); // 최종적으로 끝점을 확실히 지정해줌
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
