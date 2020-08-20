using HYS.Data;

namespace HYS.Instance
{
    public sealed class EquipInstance : IItemInstance
    {
        private DataEquip _dataEquip;
        private int _haveCount;
        private bool _isHave;

        //TODO: 포트폴리오 코드이기에 Nullable 타입으로 지정했습니다. 실제 코드 작성 시에는 null이 될만한 상황이 아니라면 확인하지 않습니다.
        public DataEquip DataEquip => _dataEquip;
        public uint ClassId => _dataEquip?.ClassId ?? 0;
        public string Thumb => _dataEquip?.Thumb ?? string.Empty;
        public string Name => _dataEquip?.Name ?? string.Empty;
        public string Desc => _dataEquip?.Desc ?? string.Empty;
        public int HaveCount => _haveCount;
        public bool IsHave => _isHave;

        public EquipInstance(DataEquip dataEquip, int haveCount, bool isHave = false)
        {
            _dataEquip = dataEquip;
            _haveCount = haveCount;
            _isHave = isHave;
        }

        public void AddHaveCount(int addCount)
        {
            _haveCount += addCount;
        }

        public void UpdateHaveCount(int count)
        {
            _haveCount = count;
        }

        public void UnLockEquip()
        {
            _isHave = true;
        }
    }
}
