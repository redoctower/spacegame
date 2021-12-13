using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform shootPivot;
    [SerializeField] private UnityEvent hit;
    private BonusSpawner _bonus;
    
    private Camera cam;
    private void Awake()
    {
        _bonus = FindObjectOfType<BonusSpawner>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootRay();
        }
    }

    private void ShootRay()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 12)
            {
                var bul = PoolManager.GetObject(bulletprefab.name, shootPivot.position, Quaternion.identity);
                bul.transform.position = shootPivot.position;
                StartCoroutine(DoShoot(bul.transform, hit.collider.gameObject, 0.3f));
            }
        }
    }

    public IEnumerator DoShoot(Transform obj, GameObject target, float animationDuration)
    {
        Vector3 startPosition = obj.position;
        float t = 0;

        while (t < 1)
        {
            obj.position = Vector3.Lerp(startPosition, target.transform.position, t * t);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
        obj.position = target.transform.position;
        obj.GetComponent<PoolObject>().ReturnToPool();
        
        LevelProgress.gameScore++;

        if (target.layer == 12)
        {
            target.GetComponent<Bonus>().Die();
        }
        else
        {
            _bonus.AddBonus();
            target.GetComponent<Enemy>().Die();
        }
    }
}
