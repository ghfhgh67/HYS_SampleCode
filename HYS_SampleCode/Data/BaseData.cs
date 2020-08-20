namespace HYS.Data
{
    public class BaseData
    {
        public uint ClassId { get; private set; }
        public string Thumb { get; private set; }
        public string Name { get; private set; }
        public string Desc { get; private set; }

        public virtual void Init(string[] csvData) { }

        public void Init(string[] csvData, ref int count)
        {
            ClassId = csvData[count++].ToUInt();
            Thumb = csvData[count++];
            Name = csvData[count++];
            Desc = csvData[count++];
        }
    }
}