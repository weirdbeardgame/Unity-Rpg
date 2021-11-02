using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageRecieved : MonoBehaviour
{
    [SerializeField]
    GameObject Damage;

    // Pos was to place the text in the scene. Though in this case. I should just have it parent to prefab of the damage taker then have it do the thing
    public DamageRecieved Create(GameObject prefab, float DamageTaken)
    {
        // Currently will spawn ontop of damage taker...
        Damage = Instantiate(Damage, prefab.transform.localPosition, Quaternion.identity);
        DamageRecieved Recieved = Damage.GetComponent<DamageRecieved>();
        Recieved.SetText(DamageTaken);

        return Recieved;
    }

    public void SetText(float Damage)
    {
        GetComponent<TextMeshPro>().SetText(Damage.ToString());
    }

    void Update()
    {
        float Speed = 5f;
        transform.position += new Vector3(0, Speed) * Time.deltaTime;
        Destroy(this.gameObject, 5);
    }

}
