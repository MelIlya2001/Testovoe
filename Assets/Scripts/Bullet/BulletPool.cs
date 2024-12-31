using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance {get; private set;}
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] uint _startCount;

    private Pool _pool;
    private List<GameObject> _activeBullets;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _pool = new Pool(transform);
        _activeBullets = new List<GameObject>();
        InitPool();
    }



    private void InitPool(){
        for (int i = 0; i < _startCount; i++)
        {
            GameObject in_obj = Instantiate(_bulletPrefab, transform);
            _pool.Objects.Enqueue(in_obj);
        }
    }


    public void GetObject(Color color, Transform parent, Vector2 direction){
        GameObject obj = _pool.Objects.Count > 0 ?
            _pool.Objects.Dequeue() : Instantiate(_bulletPrefab, transform);

        obj.transform.position = parent.position;
        obj.transform.rotation = parent.rotation;
        obj.GetComponent<BulletMove>().Direction = direction;

        _activeBullets.Add(obj);
        obj.GetComponent<SpriteRenderer>().color = color;
        obj.SetActive(true);
    }

    public void Remove(GameObject bulletObject){
        _pool.Objects.Enqueue(bulletObject);
        _activeBullets.Remove(bulletObject);
        bulletObject.SetActive(false);
    }

    public void DeactivateAllBullets()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
            _activeBullets[i].SetActive(false);
            
        _activeBullets.Clear();
    }
}
