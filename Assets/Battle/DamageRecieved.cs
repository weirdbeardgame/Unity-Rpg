using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageRecieved : MonoBehaviour
{
    [SerializeField]
    GameObject Damage;

    public DamageRecieved Create(Vector3 Pos, float DamageTaken)
    {
        Damage = Instantiate(Damage, Pos, Quaternion.identity);
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
