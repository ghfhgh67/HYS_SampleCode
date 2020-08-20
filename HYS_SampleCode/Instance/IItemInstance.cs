namespace HYS.Instance
{
    //TODO: 아이템들의 인터페이스
    public interface IItemInstance
    {
        string Thumb { get; } //TODO: 아이템 섬네일 파일 이름
        string Name { get; } //TODO: 아이템 이름
        string Desc { get; } //TODO: 아이템 설명
        int HaveCount { get; } //TODO: 아이탬의 갯수
        uint ClassId { get; } //TODO: 아이템의 클래스 아이디
        bool IsHave { get; } //TODO: 아이템을 소유 중에 있는지 여부

        void UpdateHaveCount(int count); //TODO: 아이템의 갯수를 갱신
        void AddHaveCount(int addCount); //TODO: 아이템을 더해주기
    }
}
