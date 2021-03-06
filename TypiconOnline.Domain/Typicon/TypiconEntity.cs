﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconEntity : EntityBase<int>, IAggregateRoot
    {        
        public TypiconEntity()
        {
            Signs = new List<Sign>();
            ModifiedYears = new List<ModifiedYear>();
            CommonRules = new List<CommonRule>();
            MenologyRules = new List<MenologyRule>();
            TriodionRules = new List<TriodionRule>();
            Kathismas = new List<Kathisma>();
        }

        #region Properties

        public string Name { get; set; }

        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }

        /// <summary>
        /// Возвращает true, если у Устава нет шаблона
        /// </summary>
        public bool IsTemplate
        {
            get
            {
                return Template == null;
            }
        }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }

        //private TypiconFolderEntity _rulesFolder;
        /// <summary>
        /// Структуирозованное типизированное хранилище правил для правил
        /// </summary>
        //public virtual TypiconFolderEntity RulesFolder
        //{
        //    get
        //    {
        //        //if (_rulesFolder == null)
        //        //{
        //        //    _rulesFolder = new FolderEntity() { Name = "Правила (шаблон)", Owner = this };
        //        //}
        //        return _rulesFolder;
        //    }
        //    set
        //    {
        //        _rulesFolder = value;
        //        RulesFolder.Owner = this;
        //    }
        //}

        //private Sign _templateSunday;

        //public Sign TemplateSunday
        //{
        //    get
        //    {
        //        //TODO: реализовать покрасивей
        //        //должен быть добавлен признак IsTemplateSunday в Sign
        //        if (_templateSunday == null)
        //        {
        //            _templateSunday = Signs.Find(c => c.Number == 6);
        //        }
        //        return _templateSunday;
        //    }
        //}

        //private Dictionary<int, List<ModifiedRule>> _modifiedYearsDict = new Dictionary<int, List<ModifiedRule>>();

        public virtual List<ModifiedYear> ModifiedYears { get; set; }

        public virtual List<CommonRule> CommonRules { get; set; }
        public virtual List<MenologyRule> MenologyRules { get; set; }
        public virtual List<TriodionRule> TriodionRules { get; set; }

        public virtual List<Kathisma> Kathismas { get; set; }

        private TypiconSettings _settings;
        public virtual TypiconSettings Settings
        {
            get
            {
                //if (_settings == null)
                //    _settings = new TypiconSettings() { TypiconEntity = this };
                return _settings;
            }
            set
            {
                _settings = value;
                
                if (_settings != null)
                {
                    _settings.TypiconEntity = this;
                }
            }
        }

        #endregion

        protected override void Validate()
        {
            //TODO: Добавить валидацию TypiconEntity
            // GetAll().OfType - MenologyRules 

            // GetAll().OfType - TriodionRules 
            throw new NotImplementedException();
        }

        #region ModifiedRules methods

        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        //public List<ModifiedRule> GetModifiedRules(DateTime date)
        //{
        //    ModifiedYear modifiedYear = ModifiedYears.FirstOrDefault(m => m.Year == date.Year);

        //    if (modifiedYear == null)
        //    {
        //        //По умолчанию добавляем год, пусть он и останется пустым
        //        modifiedYear = new ModifiedYear() { Year = date.Year };

        //        ModifiedYears.Add(modifiedYear);

        //        DateTime indexDate = new DateTime(date.Year, 1, 1);

        //        //формируем список дней для изменения до 1 января будущего года
        //        DateTime endDate = new DateTime(date.Year + 1, 1, 1);
        //        while (indexDate != endDate)
        //        {
        //            //Menology

        //            //находим правило для конкретного дня Минеи
        //            MenologyRule menologyRule = GetMenologyRule(indexDate);

        //            if (menologyRule == null)
        //                throw new ArgumentNullException("MenologyRule");

        //            InterpretMenologyRule(menologyRule, indexDate, date.Year);

        //            indexDate = indexDate.AddDays(1);
        //        }

        //        //теперь обрабатываем переходящие минейные праздники
        //        //у них не должны быть определены даты. так их и найдем

        //        MenologyRules.FindAll(c => (c.Date.IsEmpty && c.DateB.IsEmpty)).
        //            ForEach(a =>
        //            {
        //                InterpretMenologyRule(a, date, date.Year);

        //                //не нашел другого способа, как только два раза вычислять изменяемые дни
        //                InterpretMenologyRule(a, date.AddYears(1), date.Year);
        //            });

        //        //Triodion

        //        //найти текущую Пасху
        //        //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
        //        DateTime easter = BookStorage.Instance.Easters.GetCurrentEaster(date.Year);

        //        TriodionRules.
        //            ForEach(a =>
        //            {
        //                if (a.Rule != null)
        //                {
        //                    ModificationsRuleHandler handler = new ModificationsRuleHandler(
        //                        new RuleHandlerSettings(a), date.Year);

        //                    int i = a.DaysFromEaster;
        //                    a.Rule.Interpret(easter.AddDays(i), handler);
        //                }
        //            });
        //    }

        //    return modifiedYear.ModifiedRules.FindAll(d => d.Date.Date == date.Date);
        //}

        //private void InterpretMenologyRule(MenologyRule menologyRule, DateTime date, int year)
        //{
        //    if (menologyRule != null)
        //    {
        //        ModificationsRuleHandler handler = new ModificationsRuleHandler(
        //            new RuleHandlerSettings(menologyRule), year);
        //        //выполняем его
        //        menologyRule.Rule.Interpret(date, handler);
        //    }
        //}

        ///// <summary>
        ///// Добавляет измененное правило.
        ///// Вызывается из метода Execute класса ModificationsRuleHandler
        ///// </summary>
        ///// <param name="request"></param>
        //internal void AddModifiedRule(ModificationsRuleRequest request)
        //{
        //    ModifiedYear modifiedYear = ModifiedYears.FirstOrDefault(m => m.Year == request.Date.Year);

        //    if (modifiedYear == null)
        //    {
        //        modifiedYear = new ModifiedYear() { Year = request.Date.Year, TypiconEntity = this };
        //        ModifiedYears.Add(modifiedYear);
        //    }

        //    //ModifiedRule

        //    modifiedYear.ModifiedRules.Add(new ModifiedRule()
        //    {
        //        Date = request.Date,
        //        RuleEntity = request.Caller,
        //        Priority = request.Priority,
        //        IsLastName = request.IsLastName,
        //        IsAddition = request.AsAddition,
        //        ShortName = request.ShortName,
        //        UseFullName = request.UseFullName
        //    });
        //}
        #endregion

        #region GetRule methods

        public MenologyRule GetMenologyRule(DateTime date)
        {
            return MenologyRules.FirstOrDefault(c => c.GetCurrentDate(date.Year).Date == date.Date);
        }

        public TriodionRule GetTriodionRule(int daysFromEaster)
        {
            return TriodionRules.FirstOrDefault(c => c.DaysFromEaster == daysFromEaster);
        }

        //public TriodionRule GetTriodionRule(DateTime date)
        //{
        //    DateTime easterDate = BookStorage.Instance.Easters.GetCurrentEaster(date.Year);

        //    //вычитаем из даты, потому как неверно считает, если время оставить
        //    int daysFromEaster = date.Date.Subtract(easterDate.Date).Days;

        //    return TriodionRules.FirstOrDefault(c => c.DaysFromEaster == daysFromEaster);
        //}

        public CommonRule GetCommonRule(Func<CommonRule, bool> predicate)
        {
            return CommonRules.FirstOrDefault(predicate);
        }

        #endregion
    }
}
