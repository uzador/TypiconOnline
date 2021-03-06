﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Класс описывает коолекцию песнопений.
    /// Таквыми являются Стихиры на Господи воззвах, на Хвалитех, и т.д.
    /// </summary>
    [Serializable]
    public class YmnosStructure : DayElementBase
    {
        private List<YmnosGroup> _groups = new List<YmnosGroup>();
        private List<YmnosGroup> _theotokion = new List<YmnosGroup>();

        public YmnosStructure() { }

        public YmnosStructure(YmnosGroup group)
        {
            _groups.Add(new YmnosGroup(group));
        }

        public YmnosStructure(YmnosStructure ymnosStructure)
        {
            if (ymnosStructure == null) throw new ArgumentNullException("YmnosStructure");

            ymnosStructure.Groups.ForEach(c => _groups.Add(new YmnosGroup(c)));

            if (ymnosStructure.Doxastichon != null)
            {
                Doxastichon = new YmnosGroup(ymnosStructure.Doxastichon);
            }

            ymnosStructure.Theotokion.ForEach(c => _theotokion.Add(new YmnosGroup(c)));
        }

        #region Properties
        [XmlElement(RuleConstants.YmnosStructureGroupNode)]
        public List<YmnosGroup> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
            }
        }
        /// <summary>
        /// Славник
        /// </summary>
        [XmlElement(RuleConstants.YmnosStructureDoxastichonNode)]
        public YmnosGroup Doxastichon { get; set; }
        /// <summary>
        /// Богородичен
        /// </summary>
        [XmlElement(RuleConstants.YmnosStructureTheotokionNode)]
        public List<YmnosGroup> Theotokion
        {
            get
            {
                return _theotokion;
            }
            set
            {
                _theotokion = value;
            }
        }

        #endregion

        protected override void Validate()
        {
            foreach (YmnosGroup group in Groups)
            {
                if (!group.IsValid)
                {
                    AppendAllBrokenConstraints(group, /*ElementName + "." + */RuleConstants.YmnosStructureGroupNode);
                }
            }

            if (Doxastichon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Doxastichon, /*ElementName + "." + */RuleConstants.YmnosStructureDoxastichonNode);
            }

            foreach (YmnosGroup group in Theotokion)
            {
                if (!group.IsValid)
                {
                    AppendAllBrokenConstraints(group, /*ElementName + "." + */RuleConstants.YmnosStructureTheotokionNode);
                }
            }
        }

        private int _sticheraCount = -1;
        /// <summary>
        /// Возвращает общее количество стихир, без учета славника и богородична
        /// </summary>
        /// <returns></returns>
        public int YmnosStructureCount
        {
            get
            { 
                //if (_sticheraCount == -1)
                //{
                    ThrowExceptionIfInvalid();

                    _sticheraCount = 0;

                    foreach (YmnosGroup group in Groups)
                    {
                        _sticheraCount += group.Ymnis.Count;
                    }
                //}

                return _sticheraCount;
            }
        }

        public int Ihos
        {
            get
            {
                ThrowExceptionIfInvalid();

                int result = 0;

                if (Groups.Count > 0)
                {
                    result = Groups[0].Ihos;
                }
                else if (Doxastichon != null)
                {
                    result = Doxastichon.Ihos;
                }
                else if (Theotokion.Count > 0)
                {
                    result = Theotokion[0].Ihos;
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает один богослужебный текст с описанием гласа и подобна.
        /// </summary>
        /// <param name="index">Сквозной индекс из общего числа текстов данного объекта.</param>
        /// <returns></returns>
        public YmnosGroup this[int index]
        {
            get
            {
                if (index >= YmnosStructureCount || index < 0)
                {
                    throw new IndexOutOfRangeException("YmnosGroup");
                }

                YmnosGroup ymnosGroup = new YmnosGroup();

                int i = index;

                foreach (YmnosGroup group in Groups)
                {
                    if (i < group.Ymnis.Count)
                    {
                        //значит ищем в этой коллекции
                        ymnosGroup.Ihos = group.Ihos;
                        ymnosGroup.Annotation = group.Annotation;
                        ymnosGroup.Prosomoion = group.Prosomoion;
                        ymnosGroup.Ymnis.Add( new Ymnos(group.Ymnis[i]) );

                        break;
                    }
                    else
                    {
                        i = i - group.Ymnis.Count;
                    }
                }

                return ymnosGroup;
            }
        }

        /// <summary>
        /// Возвращает коллекцию богослужебных текстов
        /// </summary>
        /// <param name="count">Количество. Если = 0, то выдаем все без фильтрации</param>
        /// <param name="startFrom">стартовый индекс (1 - ориентированный)</param>
        /// <returns></returns>
        public YmnosStructure GetYmnosStructure(int count, int startFrom)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (startFrom < 1 || startFrom > YmnosStructureCount)
            {
                throw new ArgumentOutOfRangeException("startFrom");
            }

            ThrowExceptionIfInvalid();

            if (count == 0)
            {
                //выдаем все без фильтрации
                YmnosStructure result = new YmnosStructure();
                foreach (YmnosGroup group in Groups)
                {
                    result.Groups.Add(new YmnosGroup(group));
                }
                return result;
            }

            YmnosStructure ymnis = new YmnosStructure();

            /*если заявленное количество больше того, что есть, выдаем с повторами
            * например: 8 = 3 3 2
            *           10= 4 4 3
            * 
            */
            //if (count > YmnosStructureCount)
            //{
                int appendedCount = 0;

                int i = startFrom - 1;

                YmnosGroup lastGroup = null;

                while (appendedCount < count)
                {
                    //округляем в большую сторону результат деления count на YmnosStructureCount
                    //в результате получаем, сколько раз необходимо повторять песнопение
                    int b = (int)Math.Ceiling((double)(count - appendedCount) / (YmnosStructureCount - i));

                    YmnosGroup groupToAdd = this[i];

                    if (lastGroup == null || !lastGroup.Equals(groupToAdd))
                    {
                        ymnis.Groups.Add(groupToAdd);
                        lastGroup = groupToAdd;
                        appendedCount++;
                        b--;
                    }

                    Ymnos ymnosToAdd = groupToAdd.Ymnis[0];

                    while (b > 0)
                    {
                        lastGroup.Ymnis.Add(new Ymnos(ymnosToAdd));

                        b--;
                        appendedCount++;
                    }

                    i++;
                }
            //}

            return ymnis;
        }
    }
}
