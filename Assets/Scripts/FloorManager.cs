using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private float deadzoneLimitY;
    [SerializeField] private GameObject deadzone;

    private Dictionary<int, LevelObjectInfo> childsDict = new Dictionary<int, LevelObjectInfo>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(deadzone, transform.position + new Vector3(0, -deadzoneLimitY, 0), Quaternion.identity, transform);

        for (int i = 0; i < transform.childCount; ++i)
        {
            LevelObjectInfo origin = new LevelObjectInfo(transform.GetChild(i).gameObject);
            childsDict.Add(origin.gameObject.GetInstanceID(), origin);
        }

    }
    public LevelObjectInfo FindObjectWithID(int id)
    {
        return childsDict[id];
    }
}
public class LevelObjectInfo
{
    public GameObject gameObject;
    public Vector3 originPosition;
    public Quaternion originRotation;
    public Rigidbody rigidbody;
    public LevelObjectInfo(GameObject _gameObject) 
    {
        gameObject = _gameObject;
        originPosition = gameObject.transform.localPosition;
        originRotation = gameObject.transform.localRotation;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
}
