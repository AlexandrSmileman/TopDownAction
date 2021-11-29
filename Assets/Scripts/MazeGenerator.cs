using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject enemySpawn;
    private int _width = 500;
    private int _height = 500;
    private int _step = 10;

    public void Generate()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        var go = Instantiate(wallPrefab, new Vector3(0, 1, _height / 2f), Quaternion.identity, transform);
        go.transform.localScale = new Vector3(1, 2, _height + 1);
        go = Instantiate(wallPrefab, new Vector3(_width, 1, _height / 2f), Quaternion.identity, transform);
        go.transform.localScale = new Vector3(1, 2, _height + 1);
        go = Instantiate(wallPrefab, new Vector3(_width / 2f, 1, 0), Quaternion.identity, transform);
        go.transform.localScale = new Vector3(_width + 1, 2, 1);
        go = Instantiate(wallPrefab, new Vector3(_width / 2f, 1, _height), Quaternion.identity, transform);
        go.transform.localScale = new Vector3(_width + 1, 2, 1);

        for (int x = 1; x < _width / _step; x++)
            for (int y = 0; y < _height / _step; y++)
            {
                //int r = Random.Range(0, 10);
                if (Random.Range(0, 7) == 0)
                {
                    int length = Random.Range(1, Mathf.Min(4, _height / _step - y + 1));

                    go = Instantiate(wallPrefab, new Vector3(x * _step, 1, y * _step + length * _step / 2f), Quaternion.identity, transform);
                    go.transform.localScale = new Vector3(1, 2, length * _step + 1);
                    y += length;
                }

            }

        for (int y = 1; y < _height / _step; y++)
            for (int x = 0; x < _width / _step; x++)
            {
                //int r = Random.Range(0, 10);
                if (Random.Range(0, 7) == 0)
                {
                    int length = Random.Range(1, Mathf.Min(4, _width / _step - x + 1));

                    go = Instantiate(wallPrefab, new Vector3(x * _step + length * _step / 2f, 1, y * _step), Quaternion.identity, transform);
                    go.transform.localScale = new Vector3(length * _step + 1, 2, 1);
                    x += length;
                }

            }

        for (int y = 0; y < _height / _step - 1; y++)
            for (int x = 0; x < _width / _step - 1; x++)
            {
                if (Random.Range(0, 15) == 0)
                {
                    Vector3 pos = new Vector3((x + 0.5f) * _step, 1.5f, (y + 0.5f) * _step);
                    Instantiate(enemySpawn, pos, Quaternion.identity, transform);
                }

            }
    }
}
