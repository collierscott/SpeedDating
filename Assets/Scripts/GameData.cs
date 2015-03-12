﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public partial class GameData
    {
        public static readonly List<Hobby> Hobbies = Enum.GetValues(typeof(Hobby)).Cast<Hobby>().ToList();

        public static List<CharacterId> PremiumCharacters = new List<CharacterId>
        {
            CharacterId.f9,
            CharacterId.f10,
            CharacterId.m9,
            CharacterId.m10
        };

        public static Dictionary<CharacterId, int> CharacterPrice = new Dictionary<CharacterId, int>
        {
            { CharacterId.f1, 2 },
            { CharacterId.f2, 2 },
            { CharacterId.f3, 2 },
            { CharacterId.f4, 2 },
            { CharacterId.f5, 2 },
            { CharacterId.f6, 2 },
            { CharacterId.f7, 2 },
            { CharacterId.f8, 2 },
            { CharacterId.f9, 0 },
            { CharacterId.f10, 0 },

            { CharacterId.m1, 2 },
            { CharacterId.m2, 2 },
            { CharacterId.m3, 2 },
            { CharacterId.m4, 2 },
            { CharacterId.m5, 2 },
            { CharacterId.m6, 2 },
            { CharacterId.m7, 2 },
            { CharacterId.m8, 2 },
            { CharacterId.m9, 0 },
            { CharacterId.m10, 0 }
        }; 

        public static List<Level> EasyLevels
        {
            get
            {
                return new List<Level>
                {
                    Level2x0, Level3x0, Level3x1, Level3x2, Level4x0a,
                    Level4x0b, Level4x2, Level4x3, Level4x4, Level4x5,
                    Level5x0, Level5x1, Level5x2, Level5x3, Level5x4,
                    Level5x5a, Level5x5b, Level5x6, Level6x0, Level6x1,
                    Level6x2, Level6x3, Level6x4a, Level6x4a, Level6x4b,
                };
            }
        }

        public static List<Level> GetLevels(LevelType levelType)
        {
            switch (levelType)
            {
                case LevelType.Easy:
                    return EasyLevels;
                case LevelType.Hard:
                    return Levels.HardLevels;
                case LevelType.Swap:
                    return Levels.SwapLevels;
                case LevelType.Memo:
                    return Levels.MemoLevels;
                default:
                    throw new Exception();
            }
        }

        public static void Shuffle()
        {
            _maleImages = _maleImages.Shuffle();
            _femaleImages = _femaleImages.Shuffle();
        }

        public static string GetNextMaleImage()
        {
            return GeNext(_maleImages, ref _maleImage);
        }

        public static string GeNextFemaleImage()
        {
            return GeNext(_femaleImages, ref _femaleImage);
        }

        public static string GetNameById(CharacterId id)
        {
            return GetNameById(Convert.ToString(id));
        }

        public static string GetNameById(string id)
        {
            switch (id)
            {
                case "m1": return "Mike";
                case "m2": return "Max";
                case "m3": return "Luis";
                case "m4": return "Robert";
                case "m5": return "Alex";
                case "m6": return "Daniel";
                case "m7": return "Tony";
                case "m8": return "Kevin";
                case "m9": return "Alan";
                case "m10": return "Nate";

                case "f1": return "Lisa";
                case "f2": return "Helen";
                case "f3": return "Jessy";
                case "f4": return "Amy";
                case "f5": return "Emily";
                case "f6": return "Yuki";
                case "f7": return "Rose";
                case "f8": return "Diana";
                case "f9": return "Kate";
                case "f10": return "Alice";

                default:
                    throw new Exception();
            }
        }

        private static List<string> _maleImages = new List<string>
        {
            "m1",
            "m2",
            "m3",
            "m4",
            "m5",
            "m6",
            "m7",
            "m8",
            "m9",
            "m10",
        };

        private static List<string> _femaleImages = new List<string>
        {
            "f1",
            "f2",
            "f3",
            "f4",
            "f5",
            "f6",
            "f7",
            "f8",
            "f9",
            "f10"
        };

        private static int _maleName, _femaleName;
        private static int _maleImage, _femaleImage;

        private static string GeNext(IList<string> list, ref int index)
        {
            var result = list[index];

            if (index == list.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            return result;
        }

        public static readonly Dictionary<int, float> TableScales = new Dictionary<int, float>
        {
            { 2, 1.00f },
            { 3, 1.00f },
            { 4, 0.75f },
            { 5, 0.75f },
            { 6, 0.75f },
            { 7, 0.65f },
            { 8, 0.60f },
        };

        public static readonly Dictionary<int, List<Vector2>> TablePositions = new Dictionary<int, List<Vector2>>
        {
            {
                2, new List<Vector2>
                {
                    new Vector2(-280, -40), new Vector2(280, -40)
                }
            },
            {
                3, new List<Vector2>
                {
                    new Vector2(-400, 40), new Vector2(400, 40), new Vector2(0, -120)
                }
            },
            {
                4, new List<Vector2>
                {
                    new Vector2(-240, 80), new Vector2(240, 80), new Vector2(-440, -240), new Vector2(440, -240)
                }
            },
            {
                5, new List<Vector2>
                {
                    new Vector2(-280, 80), new Vector2(280, 80), new Vector2(0, -120), new Vector2(-440, -260), new Vector2(440, -260)
                }
            },
            {
                6, new List<Vector2>
                {
                    new Vector2(-180, 100), new Vector2(180, 100),
                    new Vector2(-500, -60), new Vector2(500, -60),
                    new Vector2(-180, -260), new Vector2(180, -260)
                }
            },
            {
                7, new List<Vector2>
                {
                    new Vector2(-250, 120), new Vector2(250, 120),
                    new Vector2(-500, -80), new Vector2(0, -80), new Vector2(500, -80),
                    new Vector2(-250, -300), new Vector2(250, -300)
                }
            },
            {
                8, new List<Vector2>
                {
                    new Vector2(-500, 60), new Vector2(0, 120), new Vector2(500, 60),
                    new Vector2(-230, -80), new Vector2(230, -80),
                    new Vector2(-500, -240), new Vector2(0, -300), new Vector2(500, -240)
                }
            }
        };
    }
}
