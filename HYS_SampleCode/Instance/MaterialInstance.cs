using HYS.Data;
using HYS.EnumType;
using System;

namespace HYS.Instance
{
    public sealed class MaterialInstance : IItemInstance
    {
        private DataMaterial _dataMaterial;
        private int _haveCount;

        //TODO: 포트폴리오 코드이기에 Nullable 타입으로 지정했습니다. 실제 코드 작성 시에는 null이 될만한 상황이 아니라면 확인하지 않습니다.
        public MaterialType MaterialType => _dataMaterial?.MaterialType ?? MaterialType.Gold;
        public uint ClassId => _dataMaterial?.ClassId ?? 0;
        public string Thumb => _dataMaterial?.Thumb ?? string.Empty;
        public string Name => _dataMaterial?.Name ?? string.Empty;
        public string Desc => _dataMaterial?.Desc ?? string.Empty;
        public int HaveCount => _haveCount;
        public bool IsHave => true; //TODO: 재화는 소유 개념이 딱히 필요하지 않으므로 true로 지정하였습니다.

        public MaterialInstance(DataMaterial dataMaterial, int haveCount)
        {
            _dataMaterial = dataMaterial;
            _haveCount = haveCount;
        }

        public void AddHaveCount(int addCount)
        {
            _haveCount += addCount;
        }

        public void UpdateHaveCount(int count)
        {
            _haveCount = count;
        }
    }
}
