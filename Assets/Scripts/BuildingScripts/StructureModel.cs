using PathologicalGames;
using UnityEngine;

public class StructureModel : MonoBehaviour
{
    float yHeight = 0;

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
           // PoolManager.Pools["Buildings"].Despawn(child.transform);
        }

        print("pravi se " + model.name);
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }
}
