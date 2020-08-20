namespace HYS.Data
{
    public class DataEquip : BaseData
    {
        public uint Shield { get; private set; }
        public float Defense { get; private set; }

        public override void Init(string[] csvData)
        {
            var count = 0;
            Init(csvData, ref count);
            Shield = csvData[count].ToUInt();
            Defense = csvData[count].ToFloat();
        }
    }
}