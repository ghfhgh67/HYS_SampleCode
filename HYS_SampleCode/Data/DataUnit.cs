namespace HYS.Data
{
    public sealed class DataUnit : BaseData
    {
        public uint Attack { get; private set; }
        public float Speed { get; private set; }

        public override void Init(string[] csvData)
        {
            var count = 0;
            Init(csvData, ref count);
            Attack = csvData[count].ToUInt();
            Speed = csvData[count].ToFloat();
        }
    }
}