using UnityEngine;

public class StatusAbnormality : MonoBehaviour
{
    [System.Serializable]
    public enum AbnormalityType
    {
        Stun = 0,
        Poison = 1,
        Noise = 2
    }

    public enum KeyType
    {
        None = 0,
        Right = 1,
        Left = 2,
    }

    [SerializeField] float maxGague = 100f;
    [SerializeField] float currentGague = 20f;
    [SerializeField] float descentSpeed = 5f;
    [SerializeField] float increaseQuantity = 5f;

    [SerializeField] bool statusFlag = false;
    [SerializeField] KeyType keyType;
    [SerializeField] bool successFlag = false;

    [SerializeField] AbnormalityType type;

    private void Start()
    {
        keyType = KeyType.None;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            statusFlag = true;

        if (!statusFlag)
            return;

        switch (type)
        { 
            case AbnormalityType.Stun:
                Stun();
                break;

            case AbnormalityType.Poison:
                break;

            case AbnormalityType.Noise:
                break;
        }
    }

    void Stun()
    {
        currentGague -= descentSpeed * Time.deltaTime;

        switch(keyType)
        {
            case KeyType.None:
                GetRightInput();
                GetLeftInput();
                break;
            case KeyType.Left:
                GetRightInput();
                break;
            case KeyType.Right:
                GetLeftInput();
                break;
        }

        if(currentGague > maxGague)
        {
            successFlag = true;
            statusFlag = false;
            Debug.Log("¼º°ø");
        }
    }

    void GetRightInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlusCurrentGague();
            keyType = KeyType.Right;
            return;
        }
    }

    void GetLeftInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlusCurrentGague();
            keyType = KeyType.Left;
            return;
        }
    }


    void PlusCurrentGague()
    {
        currentGague += increaseQuantity;
        Debug.Log(currentGague);
    }

    void Noise()
    {

    }

    void Poison()
    {

    }


}
