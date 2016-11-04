﻿public class CreateUnitDataPacket : IPacket<CreateUnitData>
{
    public class CreateUnitDataSerializer : Serializer
    {

        public bool Serialize(CreateUnitData data)
        {
            bool ret = true;

            ret &= Serialize(data.ID);
            ret &= Serialize(data.PosX);
            ret &= Serialize(data.PosY);
            ret &= Serialize(data.PosZ);
            return ret;
        }

        public bool Deserialize(ref CreateUnitData element)
        {
            if (GetDataSize() == 0)
            {
                // 데이터가 설정되지 않았다.
                return false;
            }

            bool ret = true;
            short id = 0;
            float posX = 0;
            float posY = 0;
            float posZ = 0;

            ret &= Deserialize(ref id);
            ret &= Deserialize(ref posX);
            ret &= Deserialize(ref posY);
            ret &= Deserialize(ref posZ);
            element = new CreateUnitData(id, posX, posY, posZ);

            return ret;
        }
    }

    CreateUnitData m_data;

    public CreateUnitDataPacket(CreateUnitData data) // 데이터로 초기화(송신용)
    {
        m_data = data;
    }

    public CreateUnitDataPacket(byte[] data) // 패킷을 데이터로 변환(수신용)
    {
        m_data = new CreateUnitData();
        CreateUnitDataSerializer serializer = new CreateUnitDataSerializer();
        serializer.SetDeserializedData(data);
        serializer.Deserialize(ref m_data);
    }

    public byte[] GetPacketData() // 바이트형 패킷(송신용)
    {
        CreateUnitDataSerializer serializer = new CreateUnitDataSerializer();
        serializer.Serialize(m_data);
        return serializer.GetSerializedData();
    }

    public CreateUnitData GetData() // 데이터 얻기(수신용)
    {
        return m_data;
    }

    public int GetPacketId()
    {
        return (int)P2PPacketId.CreateUnit;
    }
}

public class CreateUnitData
{
    private short id;
    private float posX;
    private float posY;
    private float posZ;

    public short ID { get { return id; } }
    public float PosX { get { return posX; } }
    public float PosY { get { return posY; } }
    public float PosZ { get { return posZ; } }

    public CreateUnitData()
    {
        id = 0;
        posX = 0;
        posY = 0;
        posZ = 0;
    }

    public CreateUnitData(short newId, float newX, float newY, float newZ)
    {
        id = newId;
        posX = newX;
        posY = newY;
        posZ = newZ;
    }
}