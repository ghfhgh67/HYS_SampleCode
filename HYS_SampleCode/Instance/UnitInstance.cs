using HYS.Data;

namespace HYS.Instance
{
    public sealed class UnitInstance : IItemInstance
    {
        private DataUnit _dataUnit;
        private int _haveCount;
        private bool _isHave;

        //TODO: 포트폴리오 코드이기에 Nullable 타입으로 지정했습니다. 실제 코드 작성 시에는 null이 될만한 상황이 아니라면 확인하지 않습니다.
        public DataUnit DataUnit => _dataUnit;
        public uint ClassId => _dataUnit?.ClassId ?? 0;
        public string Thumb => _dataUnit?.Thumb ?? string.Empty;
        public string Name => _dataUnit?.Name ?? string.Empty;
        public string Desc => _dataUnit?.Desc ?? string.Empty;
        public int HaveCount => _haveCount;
        public bool IsHave => _isHave;

        public UnitInstance(DataUnit dataUnit, int haveCount, bool isHave)
        {
            _dataUnit = dataUnit;
            _isHave = isHave;
            _haveCount = haveCount;
        }

        public void UpdateHaveCount(int count)
        {
            _haveCount = count;
        }

        public void AddHaveCount(int addCount)
        {
            _haveCount += addCount;
        }

        public void UnLockEquip()
        {

        }

        public void LevelUp()
        {
            //TODO: 레벨업 기능 수행
        }
    }
}
