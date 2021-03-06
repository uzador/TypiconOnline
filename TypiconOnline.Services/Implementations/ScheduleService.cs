﻿using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Services
{
    public class ScheduleService : IScheduleService
    {
        //ITypiconEntityService _typiconEntityService;
        IRuleHandlerSettingsFactory settingsFactory;// = new RuleHandlerSettingsFactory();
        //IModifiedRuleService _modifiedRuleService;
        //IRuleHandler _ruleHandler;
        //BookStorage _bookStorage;
        IScheduleDayNameComposer nameComposer;
        IRuleSerializerRoot ruleSerializer;

        public ScheduleService(/*ITypiconEntityService typiconEntityService
            , */IRuleHandlerSettingsFactory settingsFactory
            , IRuleSerializerRoot ruleSerializer
            //, IModifiedRuleService modifiedRuleService
            //, IRuleHandler ruleHandler
            )
        {
            //_typiconEntityService = typiconEntityService ?? throw new ArgumentNullException("ITypiconEntityService");
            this.settingsFactory = settingsFactory ?? throw new ArgumentNullException("IRuleHandlerSettingsFactory");
            //_modifiedRuleService = modifiedRuleService ?? throw new ArgumentNullException("modifiedRuleService");
            this.ruleSerializer = ruleSerializer ?? throw new ArgumentNullException("IRuleSerializerRoot");

            nameComposer = new ScheduleDayNameComposer(this.ruleSerializer.BookStorage.Oktoikh);

            //_ruleHandler = ruleHandler ?? throw new ArgumentNullException("IRuleHandler");
        }

        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request)
        {
            //находим метод обработки дня
            HandlingMode mode = request.CheckParameters.GetMode();
            
            //Формируем данные для обработки
            var settingsRequest = new GetRuleSettingsRequest()
            {
                Date = request.Date,
                Language = request.Language,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
                    .SetModeParam((mode == HandlingMode.AstronomicDay) ? HandlingMode.ThisDay : mode)
            };

            ScheduleDay scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Typicon, request.Handler);

            if (mode == HandlingMode.AstronomicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true
                settingsRequest.Date = request.Date.AddDays(1);
                settingsRequest.CheckParameters = settingsRequest.CheckParameters.SetModeParam(HandlingMode.DayBefore);

                scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Typicon, request.Handler, scheduleDay);
            }

            return new GetScheduleDayResponse()
            {
                Day = scheduleDay
            };
        }

        

        private ScheduleDay GetOrFillScheduleDay(GetRuleSettingsRequest request, TypiconEntity typicon,
            ScheduleHandler handler, ScheduleDay scheduleDay = null)
        {
            //заполняем Правила и день Октоиха
            request.MenologyRule = typicon.GetMenologyRule(request.Date);

            int daysFromEaster = ruleSerializer.BookStorage.Easters.GetDaysFromCurrentEaster(request.Date);
            request.TriodionRule = typicon.GetTriodionRule(daysFromEaster);

            request.ModifiedRule = typicon.GetModifiedRuleHighestPriority(request.Date, ruleSerializer);
            request.OktoikhDay = ruleSerializer.BookStorage.Oktoikh.Get(request.Date);

            //Формируем данные для обработки
            var settings = settingsFactory.Create(request);

            handler.Settings = settings;

            settings.Rule.GetRule(ruleSerializer).Interpret(handler);

            var container = handler.GetResult();

            if (scheduleDay == null)
            {
                //Sign sign = (settings.Rule is Sign s) ? s : GetTemplateSign(settings.Rule.Template);
                Sign sign = GetRootAdditionSign(settings);

                //Если settings.SignNumber определен в ModifiedRule, то назначаем его
                int signNumber = settings.SignNumber ?? (int)sign.Number;

                //if (request.Date.DayOfWeek == DayOfWeek.Sunday && sign.Priority > 3)
                //{
                //    //TODO: жесткая привязка к номеру знака воскресного дня
                //    signNumber = 8;// SignMigrator.GetOldId(k => k.Value.Name == "Воскресный день");
                //}

                scheduleDay = new ScheduleDay
                {
                    //задаем имя дню
                    Name = nameComposer.Compose(settings, request.Date),
                    Date = request.Date,
                    SignNumber = signNumber,
                    SignName = sign.SignName[settings.Language.Name],
                };
            }

            //if (container != null)
            //{
                scheduleDay.Schedule.AddRange(container);
            //}

            return scheduleDay;
        }

        private Sign GetRootAdditionSign(RuleHandlerSettings settings)
        {
            if (settings.Addition == null)
            {
                var sign = (settings.Rule is Sign s) ? s : settings.Rule.Template;

                return sign?.GetPredefinedTemplate();
            }
            
            return GetRootAdditionSign(settings.Addition);
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            ScheduleWeek week = new ScheduleWeek() 
            {
                Name = ruleSerializer.BookStorage.Oktoikh.GetWeekName(request.Date, false)
            };

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                Typicon = request.Typicon,
                Handler = request.Handler,
                Language = request.Language,
                ThrowExceptionIfInvalid = request.ThrowExceptionIfInvalid,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
            };

            int i = 0;

            while (i < 7)
            {
                GetScheduleDayResponse dayResponse = GetScheduleDay(dayRequest);
                week.Days.Add(dayResponse.Day);
                dayRequest.Date = dayRequest.Date.AddDays(1);
                i++;
            }

            return new GetScheduleWeekResponse() { Week = week };
        }
    }
}
