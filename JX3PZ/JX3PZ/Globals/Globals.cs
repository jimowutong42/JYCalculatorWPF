﻿using JX3CalculatorShared.Globals;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JX3PZ.Class;
using JX3PZ.Data;

namespace JX3PZ.Globals
{
    public static class PzConst
    {
        public const int MAX_DIAMOND_LEVEL = 8; // 最大五行石等级
        public const int MAX_STRENGTH_LEVEL = 8; // 最大精炼等级
        public const int POSITIONS = 12; // 装备部位总数
        public const int MIN_EQUIP_LEVEL = 9000;
        public const int MAX_EQUIP_LEVEL = 13000;

        public const decimal GKiloDenominator = 1024.0m;
    }

    public static class PzConstString
    {
        public const string Base = "基础";
        public const string Final = "最终";
        public const string PercentAdd = "提升";
        public const string Additional = "额外";
        public const string RateAdd = "提高";
    }


    public static class PzStatic
    {
        public static int DEFAULT_DIAMON_LEVEL = 0; // 默认五行石等级
    }

    public static class AttributeEntryTypeEnumToColor
    {
        public static ImmutableDictionary<AttributeEntryTypeEnum, string> Dict =
            new Dictionary<AttributeEntryTypeEnum, string>()
            {
                {AttributeEntryTypeEnum.Default, ColorConst.Green},
                {AttributeEntryTypeEnum.EquipBase, ColorConst.White},
                {AttributeEntryTypeEnum.EquipBasicMagic, ColorConst.White},
                {AttributeEntryTypeEnum.EquipExtraMagic, ColorConst.Green},
                {AttributeEntryTypeEnum.Diamond, ColorConst.Green},
                {AttributeEntryTypeEnum.Enhance, ColorConst.Enhance},
                {AttributeEntryTypeEnum.BigFM, ColorConst.Enhance},
                {AttributeEntryTypeEnum.Special, ColorConst.Orange},
                {AttributeEntryTypeEnum.Stone, ColorConst.Green},
                {AttributeEntryTypeEnum.Strength, ColorConst.Strength},
                {AttributeEntryTypeEnum.Set, ColorConst.Green},
            }.ToImmutableDictionary();


        public static string GetColor(this AttributeEntryTypeEnum t)
        {
            return Dict[t];
        }

        public static string GetColor(this AttributeEntryBase a) => GetColor(a.EntryType);
    }

    public static class ColorConst
    {
        public const string INACTIVE = "#adadad"; // 未激活的属性颜色
        public const string Enhance = "#7e7eff"; // 附魔（蓝字）
        public const string Green = "#00c848"; // 通用绿字
        public const string White = "#ffffff"; // 通用白字
        public const string Orange = "#ff9600"; // 通用橙字（特效）
        public const string Strength = "#7ee3a3"; // 通用精炼（绿字）
        public const string Yellow = "#ff0"; // 通用黄字
    }

    public static class PzScore
    {
        public const decimal A = 8.8m;
        public const decimal B = 16.0m;
        public const decimal C = 20.0m;

        public static readonly ImmutableArray<decimal> DiamondScores;
        public static readonly ImmutableArray<decimal> StoneScores;

        static PzScore()
        {
            DiamondScores = Enumerable.Range(0, PzConst.MAX_DIAMOND_LEVEL + 1).Select(_CalcDiamondScore)
                .ToImmutableArray();
            StoneScores = Enumerable.Range(0, PzConst.MAX_DIAMOND_LEVEL + 1).Select(_CalcStoneScore).ToImmutableArray();
        }

        public static decimal _CalcDiamondScore(int level)
        {
            // 计算五行石装分
            decimal res;
            if (level > 6)
            {
                res = (1.3m * (0.65m * level - 3.2m)) * A * B;
            }
            else
            {
                res = 0.195m * A * B * level;
            }

            return res;
        }

        public static decimal _CalcStoneScore(int level)
        {
            // 计算五彩石装分
            decimal res = 3.5m * A * C * level;
            return res;
        }

        public static decimal GetDiamondScore(int level) => DiamondScores[level];
        public static decimal GetStoneScore(int level) => StoneScores[level];
        public static decimal GetScore(this Stone s) => GetStoneScore(s.Level);
        public static decimal GetScore(this DiamondLevelItem d) => GetDiamondScore(d.Level);
    }
}