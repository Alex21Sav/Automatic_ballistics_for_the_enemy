using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _targetPosition;

    [SerializeField] private float _angleDegrees;

    private float g = Physics.gravity.y; // Ускорение свободного падения (9.8f)

    private void Update()
    {
        _spawnPoint.localEulerAngles = new Vector3(-_angleDegrees, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    private void Shot()
    {
        Vector3 fromTo = _targetPosition.position - transform.position;

        //Исключаем ось Y 
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        // растояние до цели 
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        // Переводим угл из градусов в радианы 
        float _angleRadian = _angleDegrees * Mathf.PI / 180;

        // Расчёт скорости 
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(_angleRadian) * x) * Mathf.Pow(Mathf.Cos(_angleRadian), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2)); // извлекаем корень по модулю
        //float v = Mathf.Sqrt(v2);

        // Создание пули
        GameObject newBullet = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = _spawnPoint.forward * v;
    }
}
