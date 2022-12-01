using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpwnerProtype : MonoBehaviour
{
    [SerializeField] private float distanceFactor;
    [SerializeField] private float radius;
    [SerializeField] private int numberOfStickmans;
    [SerializeField] private Transform prefab;
    List<Transform> prefabList = new List<Transform>();
    void Start()
    {
        //GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeStickMan();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < prefabList.Count; i++)
            {
                Destroy(prefabList[i].gameObject);
            }
        }
    }
    public void FormatStickMan1()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var NewPos = new Vector3(x, -0.55f, z);

            transform.GetChild(i).DOLocalMove(NewPos, 0.5f).SetEase(Ease.InOutBack);
        }
    }
    public void MakeStickMan()
    {
        for (int i = 0; i < numberOfStickmans; i++)
        {
           Transform newBprefab= Instantiate(prefab,transform.position, Quaternion.identity, transform);
            prefabList.Add(newBprefab);
        }

        //numberOfStickmans = transform.childCount - 1;
        //FormatStickMan();
        FormatStickMan1();

    }
    public void FormatStickMan()
    {
        for (int i = 0; i < numberOfStickmans; i++)
        {
            
            if (numberOfStickmans % 3 == 0)
            {
               
            }
        }
    }
    private Vector3 GetPosition()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < numberOfStickmans; i++)
        {
            //pos = new Vector3(i, 0, j);
            for (int j = 0; j < 5; j++)
            {


                if (j % 2 == 0 && j>0)
                {
                    continue;
                }
                pos = new Vector3(i, 0, j);

                print(pos);

            }

            Transform newBprefab = Instantiate(prefab, pos, Quaternion.identity, transform);
            prefabList.Add(newBprefab);
        }
       
        return pos;
    }
}
