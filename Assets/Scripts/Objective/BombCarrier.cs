using UnityEngine;

public class BombCarrier : MonoBehaviour
{
    [Header("Bomb setup")]
    public BombController bombPrefab;
    public BombSite bombSite;
    public Transform holdPoint;
    public KeyCode plantKey = KeyCode.G;
    public float dropDistance = 0.75f;

    private BombController carriedBomb;
    private Coroutine plantRoutine;

    private void Start()
    {
        if (bombPrefab == null)
        {
            return;
        }

        carriedBomb = Instantiate(bombPrefab);
        carriedBomb.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (carriedBomb == null)
        {
            return;
        }

        if (!carriedBomb.IsPlanted && holdPoint != null)
        {
            carriedBomb.transform.SetPositionAndRotation(holdPoint.position, holdPoint.rotation);
        }

        if (Input.GetKey(plantKey))
        {
            if (plantRoutine == null)
            {
                carriedBomb.plantKey = plantKey;
                carriedBomb.targetSite = bombSite;
                carriedBomb.transform.position = holdPoint ? holdPoint.position : transform.position + transform.forward * dropDistance;
                carriedBomb.gameObject.SetActive(true);
                plantRoutine = StartCoroutine(PlantRoutine());
            }
        }
        else if (plantRoutine != null)
        {
            StopCoroutine(plantRoutine);
            plantRoutine = null;
            carriedBomb.CancelPlanting();
        }

        if (carriedBomb.IsPlanted)
        {
            plantRoutine = null;
            carriedBomb = null;
        }
    }

    private System.Collections.IEnumerator PlantRoutine()
    {
        yield return StartCoroutine(carriedBomb.TryPlant(transform));
        plantRoutine = null;
    }
}
