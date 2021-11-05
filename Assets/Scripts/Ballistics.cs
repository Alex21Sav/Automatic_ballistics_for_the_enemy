using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _targetPosition;

    [SerializeField] private float _angleDegrees;

    private float g = Physics.gravity.y; // ��������� ���������� ������� (9.8f)

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

        //��������� ��� Y 
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        // ��������� �� ���� 
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        // ��������� ��� �� �������� � ������� 
        float _angleRadian = _angleDegrees * Mathf.PI / 180;

        // ������ �������� 
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(_angleRadian) * x) * Mathf.Pow(Mathf.Cos(_angleRadian), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2)); // ��������� ������ �� ������
        //float v = Mathf.Sqrt(v2);

        // �������� ����
        GameObject newBullet = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = _spawnPoint.forward * v;
    }
}
