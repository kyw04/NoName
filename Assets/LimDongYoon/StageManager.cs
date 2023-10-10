using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject stagePrefab;
    public float levelSpacing = 3f; // ���� �� ����
    public float[] sameLevelSpacing; // ���� ���� �� ����

    private Stage[,] stages;

    void Start()
    {
        GenerateStages();
    }

    void GenerateStages()
    {
        int totalLevels = 10; // ���� �̹������� �� 5���� ������ �ֽ��ϴ�.
        for (int level = 0; level < totalLevels; level++)
        {
            if(level == 0 || level == totalLevels)
            {
                continue;
            }
            // ���� ������ ��� ���� ���
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