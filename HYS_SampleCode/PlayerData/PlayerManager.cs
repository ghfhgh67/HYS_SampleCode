using HYS.EnumType;

namespace HYS.PlayerData
{
    public sealed class PlayerManager : Singleton<PlayerManager>
    {
        //TODO: 유저 정보 더미
        public uint ProfileIconId;
        public uint UserId;
        public string Nickname;

        public ItemContainer ItemContainer { get; private set; }

        public void Login() //TODO: 로그인할 때 호출, 기본적으로는 서버에서 유저 정보를 받아 넣어줍니다.
        {
            ItemContainer = new ItemContainer();
            ItemContainer.Init();
        }

        public bool IsIncludeItem(ItemType itemType, uint classId)
        {
            switch (itemType)
            {
                case ItemType.Unit:
                    return ItemContainer.IsIncludeUnit(classId);
                case ItemType.Equip:
                    return ItemContainer.IsIncludeEquip(classId);
                case ItemType.Material:
                    return ItemContainer.IsIncludeMaterial(classId);
            }

            return false;
        }
    }
}
