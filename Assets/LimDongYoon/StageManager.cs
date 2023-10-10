using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject stagePrefab;
    public float levelSpacing = 3f; // 레벨 간 간격
    public float[] sameLevelSpacing; // 같은 레벨 내 간격

    private Stage[,] stages;

    void Start()
    {
        GenerateStages();
    }

    void GenerateStages()
    {
        int totalLevels = 10; // 예제 이미지에는 총 5개의 레벨이 있습니다.
        for (int level = 0; level < totalLevels; level++)
        {
            if(level == 0 || level == totalLevels)
            {
                continue;
            }
            // 현재 레벨의 노드 개수 계산
            int nodesInLevel = ((level%2) == 0 )? 4:3;

            
            for (int node = 0; node < nodesInLevel; node++)
            {
                float x = (node - nodesInLevel / 2) * sameLevelSpacing[level];
                float y = -levelSpacing * level;

                Vector2 position = new Vector2(x, y);

                var a = Instantiate(stagePrefab, position, Quaternion.identity);
                
            }
        }
        
    }
    
}