using HYS.EnumType;

namespace HYS.Data
{
    public class DataMaterial : BaseData
    {
        public MaterialType MaterialType { get; private set; }

        public override void Init(string[] csvData)
        {
            var count = 0;
            Init(csvData, ref count);
            MaterialType = (MaterialType)csvData[count].ToInt();
        }
    }
}