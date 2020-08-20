using HYS.Data;
using HYS.EnumType;
using HYS.Instance;
using HYS.PlayerData;
using System;
using System.IO;
using System.Text;

namespace HYS
{
    class Program
    {
        private static readonly string _extensionName = ".csv";
        private static StringBuilder _stringAppend = new StringBuilder();

        static void Main(string[] args)
        {
            InitData();
            PlayerManager.Instance.Login();

            var itemType = GetSelectItemType();
            var classId = GetSelectClassId(itemType);
            ShowingItemData(itemType, classId);
        }

        #region Init

        private static void InitData()
        {
            var dataTypes = (EnumType.DataType[])Enum.GetValues(typeof(EnumType.DataType));

            for (var i = 0; i < dataTypes.Length; i++)
            {
                ReadCSV(dataTypes[i]);
            }
        }

        private static void ReadCSV(EnumType.DataType dataType)
        {
            var csvName = GetStringAppend(dataType.ToString(), _extensionName);

            using (var reader = new StreamReader(csvName))
            {
                var isFirst = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                    AddData(dataType, line.Split('|'));
                }
            }
        }

        private static void AddData(EnumType.DataType dataType, string[] data)
        {
            switch (dataType)
            {
                case EnumType.DataType.data_unit:
                    {
                        var dataUnit = new DataUnit();
                        dataUnit.Init(data);
                        DataGetter.AddGameData(EnumType.DataType.data_unit, dataUnit);
                    }
                    break;
                case EnumType.DataType.data_equip:
                    {
                        var dataEquip = new DataEquip();
                        dataEquip.Init(data);
                        DataGetter.AddGameData(EnumType.DataType.data_equip, dataEquip);
                    }
                    break;
                case EnumType.DataType.data_material:
                    {
                        var dataMaterial = new Data.DataMaterial();
                        dataMaterial.Init(data);
                        DataGetter.AddGameData(EnumType.DataType.data_material, dataMaterial);
                    }
                    break;
            }
        }

        #endregion

        #region Input Method

        private static ItemType GetSelectItemType()
        {
            Console.WriteLine("1) ItemType을 입력해주세요. \n (Unit, Equip, Material)");
            ItemType itemType;

            var input = Console.ReadLine();
            while (Enum.TryParse(input, out itemType) == false)
            {
                Console.WriteLine("! 없는 ItemType을 선택하셨습니다. !");
                input = Console.ReadLine();
            }
            Console.WriteLine(GetStringAppend("<< ", itemType.ToString(), "을 선택하셨습니다. >>"));

            return itemType;
        }

        private static uint GetSelectClassId(ItemType itemType)
        {
            Console.WriteLine("----------------------------------------");
            uint classId = 0;
            switch (itemType)
            {
                case ItemType.Unit:
                case ItemType.Equip:
                    {
                        Console.WriteLine(GetStringAppend("2) ", itemType == ItemType.Unit ? "유닛" : "장비", "의 Id를 입력해주세요"));
                        var input = Console.ReadLine();
                        while (uint.TryParse(input, out classId) == false || IsInCludeItem(itemType, classId) == false)
                        {
                            Console.WriteLine("한글 또는 없는 ID를 입력하셨습니다.");
                            Console.WriteLine("한글 또는 없는 ID를 입력하셨습니다.");
                            input = Console.ReadLine();
                        }

                        Console.WriteLine(GetStringAppend("<< ", classId.ToString(), "을 선택하셨습니다. >>"));
                    }
                    break;
                case ItemType.Material:
                    {
                        Console.WriteLine(GetStringAppend("2) 재화 타입을 입력해주세요\n (Gold, Crystal)"));
                        MaterialType materialType;
                        var input = Console.ReadLine();
                        while (Enum.TryParse(input, out materialType) == false)
                        {
                            Console.WriteLine("없는 EnumType을 선택하셨습니다.");
                            input = Console.ReadLine();
                        }
                        classId = (uint)materialType;
                        Console.WriteLine(GetStringAppend("<< ", materialType.ToString(), "을 선택하셨습니다. >>"));
                    }
                    break;
            }

            return classId;
        }

        private static bool IsInCludeItem(ItemType itemType, uint id)
        {
            return PlayerManager.Instance.IsIncludeItem(itemType, id);
        }

        #endregion

        #region Showing Method

        private static void ShowingItemData(ItemType itemType, uint id)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("3) 선택하신 아이템의 정보\n");
            switch (itemType)
            {
                case ItemType.Unit:
                    {
                        var unitInstance = PlayerManager.Instance.ItemContainer.GetUnitInstance(id);
                        ShowingDefaultItemData(unitInstance);

                        Console.WriteLine("2. 고유 정보 ----------------------");
                        Console.WriteLine(GetStringAppend("공격력: ", unitInstance.DataUnit.Attack.ToString()));
                        Console.WriteLine(GetStringAppend("공격스피드: ", unitInstance.DataUnit.Speed.ToString()));
                    }
                    break;
                case ItemType.Equip:
                    {
                        var equipInstance = PlayerManager.Instance.ItemContainer.GetEquipInstance(id);
                        ShowingDefaultItemData(equipInstance);

                        Console.WriteLine("2. 고유 정보 ----------------------");
                        Console.WriteLine(GetStringAppend("보호막: ", equipInstance.DataEquip.Shield.ToString()));
                        Console.WriteLine(GetStringAppend("방어: ", equipInstance.DataEquip.Defense.ToString()));
                    }
                    break;
                case ItemType.Material:
                    {
                        var materialInstance = PlayerManager.Instance.ItemContainer.GetMaterialInstance((MaterialType)id);
                        ShowingDefaultItemData(materialInstance);

                        Console.WriteLine("2. 고유 정보 ----------------------");
                        Console.WriteLine(GetStringAppend("재화 타입: ", materialInstance.MaterialType.ToString()));
                    }
                    break;
            }
        }

        private static void ShowingDefaultItemData(IItemInstance itemInstance)
        {
            Console.WriteLine("1. 공통 정보 ----------------------");
            Console.WriteLine(GetStringAppend("아이디: ", itemInstance.ClassId.ToString()));
            Console.WriteLine(GetStringAppend("섬네일 이름: ", itemInstance.Thumb));
            Console.WriteLine(GetStringAppend("이름: ", itemInstance.Name));
            Console.WriteLine(GetStringAppend("설명: ", itemInstance.Desc));
            Console.WriteLine(GetStringAppend("보유 여부: ", itemInstance.IsHave.ToString()));
            Console.WriteLine(GetStringAppend("보유 갯수: ", itemInstance.HaveCount.ToString(), "\n"));
        }

        #endregion

        public static string GetStringAppend(params string[] param)
        {
            _stringAppend.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                _stringAppend.Append(param[i]);
            }
            return _stringAppend.ToString();
        }
    }
}