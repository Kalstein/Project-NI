using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ü�� ���� �̵� Ŭ����
public class EnemyMove : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0); // ��ǥ ���� �ӽ� ����� ���� ��, ���� ��ġ�� ���� �� ���
    Vector3 position = new Vector3(0, 0, 0); // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    public float movementSpeed = 20; // �̵��ӵ�
    GameObject player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    Rigidbody rb; // ������ �ٵ�

    int count = 0; // ���� ���� ���� Ȯ�ο�
    public float height = 1.0f;

    private delegate void Control(); // �̵��� ��������Ʈ
    Control control; // ����

    System.Diagnostics.Stopwatch watch; // � �̵� �׽�Ʈ ��
    void Start()
    {
        watch = new System.Diagnostics.Stopwatch(); // � �̵� �׽�Ʈ ��
        watch.Start(); // � �̵� �׽�Ʈ ��
        position = this.gameObject.transform.position; // �ʱ� ��ǥ�� ��ǥ ��ǥ�� �Է�, ������ �����ϱ� ���� �ʿ�
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ��������
        rb = this.gameObject.GetComponent<Rigidbody>(); // ������ �ٵ�

        // ��������Ʈ �տ���
        //control += move; // �̵� �޼ҵ�
        control += p3; // �̵� �޼ҵ�
        //control += CheckActive; // Ȱ��ȭ ���� ���� �޼ҵ�
    }

    float t = 0.0f;
    void Update()
    {
        control(); // ��������Ʈ control ����

        if (t <= 1)
        {
            run(t);
            t += 0.02f;
        }
    }

    // �̵� �޼ҵ�
    private void move()
    {
        this.gameObject.transform.LookAt(player.transform); // �÷��̾ �ٶ󺸰� �Ѵ�.
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, position, movementSpeed * Time.deltaTime); // ���� ��ǥ���� ��ǥ ��ǥ�� ������ �ӵ��� �̵�
    }

    // Ȱ��ȭ ���� �޼ҵ�
    // �ѤѤѤѤѤѤѤѤѤѤѤѤѼ��� �ʿ�ѤѤѤѤѤѤѤѤѤѤѤѤ�
    private void CheckActive()
    {
        if (this.gameObject.transform.position.z < player.transform.position.z - 10) // �÷��̾� �ڷ� 10��ŭ ���� �� ��Ȱ��ȭ
        {
            this.gameObject.SetActive(false); // ��Ȱ��ȭ
        }
        else if (this.gameObject.transform.position.x < player.transform.position.x - 50) // �÷��̾� ���� 50 ��ŭ �������� ���� �� ��Ȱ��ȭ
        {
            this.gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }

    // ���̹ޱ�
    public void MoveToPlayer()
    {
        position = player.transform.position; // ��ǥ ��ǥ�� �÷��̾� ��ǥ�� ����
    }

    // ��ǥ ��ǥ ���� �޼ҵ�
    public void ChangeVector3(float x, float y, float z)
    {
        position.x = x;
        position.y = y;
        position.z = z;
    }

    // �÷��̾� ���� ��ǥ ��ǥ ���� �޼ҵ�
    public void ChangeVector3ToPlayer(float x, float y, float z)
    {
        position.x = x + player.transform.position.x;
        position.y = y + player.transform.position.y;
        position.z = z + player.transform.position.z;
    }

    // �÷��̾� ���� ȸ�� �޼ҵ�
    // �ٸ� �̵� �޼ҵ�� ���� ���� �Ǹ� �̻������� ����
    public void RotateAroundPlayer()
    {
        this.gameObject.transform.RotateAround(player.transform.position, Vector3.down, movementSpeed * Time.deltaTime); // �÷��̾ �������� ȸ���Ѵ�.
    }

    // ���� ȸ�� �޼ҵ�
    public void RotateAroundPosition(Vector3 target)
    {
        this.gameObject.transform.RotateAround(target, Vector3.up, movementSpeed * Time.deltaTime);
    }

    // � �̵�, ���� ����, �׽�Ʈ ��
    void run(float t)
    {
        Vector3 mep = this.gameObject.transform.position; // �ش� ������Ʈ ���� ��ǥ ��������

        Vector3 startHeightPos = mep + new Vector3(0, 1, 0) * height;
        Vector3 endHeightPos = position + new Vector3(0, 1, 0) * height;

        Vector3 a = Vector3.Lerp(mep, startHeightPos, t); //Mathf.SmoothStep(tMin, tMax, t)���� �ڿ������� �����ϴ°ǵ� �������� ���δ�.
        Vector3 b = Vector3.Lerp(startHeightPos, endHeightPos, t);
        Vector3 c = Vector3.Lerp(endHeightPos, position, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 f = Vector3.Lerp(d, e, t);

        transform.position = f;
    }

    // �ڵ� ��ǥ ���� �޼ҵ�
    // �Ⱬ�� ������
    void AutoMove()
    {
        Vector3 mep = this.gameObject.transform.position; // �ش� ������Ʈ ���� ��ǥ ��������
        float x = 0;
        float y = 0;
        float z = 0;

        if (position == this.gameObject.transform.position) // ��ǥ ��ǥ�� ���� ��ǥ�� ���� �� ����
        {
            x = Random.Range(-20, 20);
            y = Random.Range(-15, 15);
            z = player.transform.position.z - Random.Range(-10, 0);

            ChangeVector3(x, y, z);
        }

        if (mep.z <= player.transform.position.z + 10) // �÷��̾�� 10��ŭ ����� ���� ����
        {
            z = Random.Range(30, 70);
            ChangeVector3(position.x, position.y, z);
        }
    }

    // �÷��̾� �������� ��������
    void p1_1()
    {
        ChangeVector3ToPlayer(-20, -5, -10);
    }

    // �÷��̾� ���������� ��������
    void p1_2()
    {
        ChangeVector3ToPlayer(20, -5, -10);
    }

    // �÷��̾� ���� �տ��� �������� �̵��ϱ�, �÷��̾� �þ� ������
    void p2_1()
    {
        ChangeVector3(-50 + player.transform.position.x, -5 + position.y, 5 + position.z);
    }

    // �÷��̾� ���� �տ��� ���������� �̵��ϱ�, �÷��̾� �þ� ������
    void p2_2()
    {
        ChangeVector3(50 + player.transform.position.x, -5 + position.y, 5 + position.z);
    }

    // ��������
    void p3()
    {
        Vector3 mep = this.gameObject.transform.position; // �ش� ������Ʈ ���� ��ǥ ��������

        bool change = false; // ��ǥ ��ǥ�� ���� Ȯ�ο� 

        float x = 0;
        float y = 0;
        float z = 0;

        if (count == 0) // �ش� ������ ó�� ������ ��
        {
            // ���� �������� �̵�
            x = 30 + player.transform.position.x;
            y = Random.Range(-10, 10);
            z = 80;

            target = new Vector3(x, y, z); // ���� �̵�

            ChangeVector3(x, y, z);

            count++;
        }

            if (mep.x == target.x && mep.z == 80) // ���� ������ ��
            {
                // ���� ���� ����
                x = -30;
                y = Random.Range(-20, 20);
                z = 60;

                change = true; // �̵��� ���� true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 60) // 1 ������ ��
            {
                // ���� ���� ����
                x = 30;
                y = Random.Range(-20, 20);
                z = 40;

                change = true; // �̵��� ���� true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 40) // 2 ������ ��
            {
                // ���� ���� ����
                x = -30;
                y = Random.Range(-20, 20);
                z = 20;

                change = true; // �̵��� ���� true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 20) // 3 ������ ��
            {
                // ���� ���� ����
                x = 30;
                y = Random.Range(-20, 20);
                z = 0;

                change = true; // �̵��� ���� true
                target.x = x + player.transform.position.x;
            }

            if (change) // �� if ������ ���� ���� ���� ��
            {
                ChangeVector3ToPlayer(x, y, z - player.transform.position.z); // ���� ��ǥ�� �̵�, �÷��̾��� z�� �̵��� �����ʿ� ���� - ����
            }
        
    }

    // �÷��̾�� �̵��ϴٰ� �÷��̾�� 20(z)��ŭ ��������� �÷��̾� ������ ��������. �󸶳� ���ܰ����� ���� ����
    void p4()
    {
        ChangeVector3(player.transform.position.x, Random.Range(-5, 5), player.transform.position.z);

        if (this.gameObject.transform.position.z <= player.transform.position.z + 20)
        {
            ChangeVector3ToPlayer(Random.Range(-10, 10), Random.Range(-10, 10), -10);
        }
    }

    // �÷��̾� �տ��� ��ȸ, �簢���� �׸���.
    void p5()
    {
        Vector3 mep = this.gameObject.transform.position; // �ش� ������Ʈ ���� ��ǥ ��������

        float x = 0;
        float y = 0;
        float z = 0;

        bool change = false; // ��ǥ ��ǥ�� ���� Ȯ�ο� 

        if (position == this.gameObject.transform.position) // ��ǥ ��ǥ�� ���� ��ǥ�� ���� �� ����
        {
            // ������Ʈ�� �÷��̾� ���� ���ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            if (mep.x < player.transform.position.x && mep.z > player.transform.position.z + 45)
            {
                // ���� ���� ����
                x = 30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // �̵��� ���� true
            }
            // ������Ʈ�� �÷��̾� ���� �����ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x > player.transform.position.x && mep.z > player.transform.position.z + 45)
            {
                // ���� ���� ����
                x = 30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // �̵��� ���� true
            }
            // ������Ʈ�� �÷��̾� ���� �����ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x > player.transform.position.x && mep.z < player.transform.position.z + 45)
            {
                // ���� ���� ����
                x = -30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // �̵��� ���� true
            }
            // ������Ʈ�� �÷��̾� ���� ���ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x < player.transform.position.x && mep.z < player.transform.position.z + 45)
            {
                // ���� ���� ����
                x = -30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // �̵��� ���� true
            }

            if (change) // �� if ������ ���� ���� ���� ��, �� ��ǥ ��ǥ�� ������� ��
            {
                ChangeVector3ToPlayer(x, y, z - player.transform.position.z); // ���� ��ǥ�� �̵�, �÷��̾��� z�� �̵��� �����ʿ� ���� - ����
            }
        }
    }

    void p6()
    {

    }
}
