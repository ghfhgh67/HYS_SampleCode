using HYS.EnumType;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HYS.Data
{
    public static class DataGetter
    {
        public static readonly ConcurrentDictionary<DataType, object> DataMap = new ConcurrentDictionary<DataType, object>();

        public static void AddGameData(DataType dataType, BaseData data)
        {
            if (DataMap.ContainsKey(dataType) == false)
                DataMap.TryAdd(dataType, new Dictionary<uint, BaseData>());

            var temp = DataMap[dataType] as Dictionary<uint, BaseData>;
            temp.TryAdd(data.ClassId, data);
        }

        public static DataUnit GetUnit(uint unitId)
        {
            var datas = DataMap[DataType.data_unit] as Dictionary<uint, BaseData>;
            return datas[unitId] as DataUnit;
        }

        public static Dictionary<uint, BaseData> GetItems(DataType dataType)
        {
            return DataMap[dataType] as Dictionary<uint, BaseData>;
        }

        public static DataEquip GetEquip(uint equipId)
        {
            var datas = DataMap[DataType.data_equip] as Dictionary<uint, BaseData>;
            return datas[equipId] as DataEquip;
        }

        public static DataMaterial GetMaterial(uint materialId)
        {
            var datas = DataMap[DataType.data_material] as Dictionary<uint, BaseData>;
            return datas[materialId] as DataMaterial;
        }

        public static DataMaterial GetMaterial(MaterialType materialType)
        {
            foreach (var data in DataMap[DataType.data_material] as Dictionary<uint, BaseData>)
            {
                var dataMaterial = data.Value as DataMaterial;
                if (dataMaterial.MaterialType == materialType)
                    return dataMaterial;
            }

            return null;
        }
    }
}