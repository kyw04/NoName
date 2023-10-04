using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class NodeLineConnector : MonoBehaviour
{
    SelStageManager stageMgr;
    public int levelDepth = 0;
    public List<RectTransform> stageNodes;
    public List<NodeLineConnector> connectors=  new List<NodeLineConnector>();
    public List<LineRenderer> lineRenderers;
    public LineRenderer lineRenderer;
    public float drawSpeed = 1f;
    private Button btn;
    private int numState = 0;

    public Vector3 plusPos;

    public void MoveLIne()
    {
        
    }
  
    private void Start()
    {
        foreach (var node in stageNodes)
        {
            connectors.Add(node.GetComponent<NodeLineConnector>());
        }
        stageMgr = FindObjectOfType<SelStageManager>();
        btn = GetComponent<Button>();
        btn.targetGraphic = GetComponent<RawImage>();

        btn.onClick.AddListener(Connect);

        for (int i = 0; i < stageNodes.Count; i++) 
        {
            var a = Instantiate(lineRenderer, transform);
            a.positionCount = 1;
            a.SetPosition(0, transform.position + plusPos);
            Debug.Log(transform.position);
            lineRenderers.Add(a);
               
        }

        /*    if (stageNodes.Count <= 1)
            {
                Debug.LogWarning("Need at least two nodes to connect!");
                return;
            }*/

        //Initialize lineRenderer

        if (stageMgr.isCreateDefalutLine)
        {

            for (int i = 0; i < stageNodes.Count; i++)
            {
                lineRenderers[i].positionCount++;
                lineRenderers[i].SetPosition(1, stageNodes[i].position + plusPos);
                Debug.Log(transform.name + " 스테이지 호출");
            }
        }
    }
    public void Connect()
    {
        stageMgr.ConnectLIneNode(this);
    }
    private void ConnectLine()
    {
        
        
        if (numState > stageNodes.Count -1) { Debug.Log("인덱스 범위 넘어섬"); return; }
        lineRenderers[numState].positionCount++;
        lineRenderers[numState].SetPosition(1, stageNodes[numState].position  + plusPos );
        numState += 1;

        Debug.Log(transform.name + " 스테이지 호출");
  
    }

    IEnumerator DrawLinesBetweenNodes()
    {
        Vector3 currentTarget;
        LineRenderer lr;
        for (int i = 1; i < stageNodes.Count; i++)
        {
            currentTarget = stageNodes[i].position;
            while (Vector3.Distance(lineRenderers[i].GetPosition(lineRenderers[i].positionCount - 1), currentTarget) > 0.05f)
            {
                Vector3 newPoint = Vector3.MoveTowards(lineRenderers[i].GetPosition(lineRenderers[i].positionCount - 1), currentTarget, drawSpeed * Time.deltaTime);
                if (Vector3.Distance(newPoint, lineRenderers[i].GetPosition(lineRenderers[i].positionCount - 1)) > 0.05f)
                {
                    Debug.Log(transform.name);
                    lineRenderers[i].positionCount += 1;
                    lineRenderers[i].SetPosition(lineRenderers[i].positionCount - 1, newPoint);
                }
                yield return null;
            }
        }
    }
}
