using HYS.Instance;
using HYS.Data;
using HYS.EnumType;
using System;
using System.Collections.Generic;

namespace HYS.PlayerData
{
    public sealed class ItemContainer
    {
        private readonly Dictionary<ItemType, List<IItemInstance>> _goodsInstanceCategory = new Dictionary<ItemType, List<IItemInstance>>(ItemTypeComparer.Comparer);

        private readonly Dictionary<uint, UnitInstance> _unitInstances = new Dictionary<uint, UnitInstance>();
        private readonly Dictionary<uint, EquipInstance> _equipInstance = new Dictionary<uint, EquipInstance>();
        private readonly Dictionary<MaterialType, MaterialInstance> _materialInstnace = new Dictionary<MaterialType, MaterialInstance>(MaterialTypeComparer.Comparer);

        public void Init()
        {
            InitUnits();
            InitEquips();
            InitMaterial();
        }

        #region Init

        private void InitUnits()
        {
            var dataUnits = DataGetter.GetItems(DataType.data_unit);
            if (_goodsInstanceCategory.ContainsKey(ItemType.Unit) == false)
                _goodsInstanceCategory.Add(ItemType.Unit, new List<IItemInstance>());

            //TODO: 임의값을 넣기위해 랜덤으로,기본적으로는 서버에서 받는 값을 넣습니다.
            Random r = new Random();
            foreach (var data in dataUnits)
            {
                var dataUnit = data.Value as DataUnit;
                if (_unitInstances.ContainsKey(dataUnit.ClassId))
                    continue;

                var unitInstance = new UnitInstance(dataUnit, r.Next(0, 100), r.Next(0, 2) == 0);

                _unitInstances.Add(dataUnit.ClassId, unitInstance);
                _goodsInstanceCategory[ItemType.Unit].Add(unitInstance);
            }
        }

        private void InitEquips()
        {
            var dataEquips = DataGetter.GetItems(DataType.data_equip);
            if (_goodsInstanceCategory.ContainsKey(ItemType.Equip) == false)
                _goodsInstanceCategory.Add(ItemType.Equip, new List<IItemInstance>());

            //TODO: 임의값을 넣기위해 랜덤으로,기본적으로는 서버에서 받는 값을 넣습니다.
            Random r = new Random();
            foreach (var data in dataEquips)
            {
                var dataEquip = data.Value as DataEquip;
                if (_equipInstance.ContainsKey(dataEquip.ClassId))
                    continue;

                var equipInstance = new EquipInstance(dataEquip, r.Next(0, 100), r.Next(0, 2) == 0);

                _equipInstance.Add(dataEquip.ClassId, equipInstance);
                _goodsInstanceCategory[ItemType.Equip].Add(equipInstance);
            }
        }

        private void InitMaterial()
        {
            var dataMaterials = DataGetter.GetItems(DataType.data_material);
            if (_goodsInstanceCategory.ContainsKey(ItemType.Material) == false)
                _goodsInstanceCategory.Add(ItemType.Material, new List<IItemInstance>());

            //TODO: 임의값을 넣기위해 랜덤으로,기본적으로는 서버에서 받는 값을 넣습니다.
            Random r = new Random();
            foreach (var data in dataMaterials)
            {
                var dataMaterial = data.Value as DataMaterial;
                if (_materialInstnace.ContainsKey(dataMaterial.MaterialType))
                    continue;

                var materialInstance = new MaterialInstance(dataMaterial, r.Next(0, 100));

                _materialInstnace.Add(dataMaterial.MaterialType, materialInstance);
                _goodsInstanceCategory[ItemType.Material].Add(materialInstance);
            }
        }

        #endregion

        public IItemInstance GetItemInstance(ItemType itemType, uint classId)
        {
            if (_goodsInstanceCategory.ContainsKey(itemType) == false)
                return null;

            var goodsList = _goodsInstanceCategory[itemType];
            for (var i = 0; i < goodsList.Count; i++)
            {
                if (goodsList[i].ClassId == classId)
                    return goodsList[i];
            }
            return null;
        }

        public bool UpdateItemCount(ItemType itemType, uint classId, int addCount)
        {
            if (addCount < 0)
                return false;

            var itemInstance = GetItemInstance(itemType, classId);
            if (itemInstance == null)
                return false;

            var haveCount = itemInstance.HaveCount + addCount;
            itemInstance.UpdateHaveCount(haveCount);
            return true;
        }

        #region Unit

        public bool IsIncludeUnit(uint unitId)
        {
            return _unitInstances.ContainsKey(unitId);
        }

        public void UnitLevelUp(uint unitId)
        {
            var unitInstance = GetUnitInstance(unitId);
            if (unitInstance == null)
                return;

            unitInstance.LevelUp();
        }

        public UnitInstance GetUnitInstance(uint unitId)
        {
            if (_unitInstances.ContainsKey(unitId) == false)
                return null;

            return _unitInstances[unitId];
        }

        #endregion

        #region Equip

        public bool IsIncludeEquip(uint equipId)
        {
            return _equipInstance.ContainsKey(equipId);
        }

        public EquipInstance GetEquipInstance(uint equipId)
        {
            if (_equipInstance.ContainsKey(equipId) == false)
                return null;

            return _equipInstance[equipId];
        }

        #endregion

        #region Material

        public bool IsIncludeMaterial(uint matertialId)
        {
            return _unitInstances.ContainsKey(matertialId);
        }

        public MaterialInstance GetMaterialInstance(MaterialType materialType)
        {
            if (_materialInstnace.ContainsKey(materialType) == false)
                return null;

            return _materialInstnace[materialType];
        }

        #endregion
    }
}