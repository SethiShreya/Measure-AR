using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    private float value= 4.36f;
    [SerializeField]
    private GameObject obj;
    private LineManager _lineManager;

    void Start()
    {
        _lineManager = FindObjectOfType<LineManager>();
        if (_lineManager == null)
        {
            Debug.Log("Line manager not found");
        }
    }

    public void CreatePoints()
    {
        float x = Random.Range(-value, value);
        float z = Random.Range(-value, value);
        Vector3 pos = new Vector3(x, 0, z);

        Transform anchor = (Instantiate(obj, pos, Quaternion.identity)).transform;
        _lineManager.DrawLine(anchor);
    }
}
