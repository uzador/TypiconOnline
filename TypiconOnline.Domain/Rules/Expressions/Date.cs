﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Простой класс. Внутри должна быть строка с датой без привязки к году --MM-dd
    /// Если пустое значение, то дата принимает текущее значение
    /// </summary>
    public class Date : DateExpression
    {
        public Date(string name) : base(name) { }

        public override object ValueCalculated
        {
            get
            {
                return (IsValid) ? base.ValueCalculated : DateTime.MinValue;
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler settings)
        {
            //если значение выражения пустое, просто передаем текущую дату
            //из вводимой даты берем только год
            ValueCalculated = (((ItemDate)ValueExpression).IsEmpty) ? date : new DateTime(date.Year, ((ItemDate)ValueExpression).Month, ((ItemDate)ValueExpression).Day);
        }

        protected override void Validate()
        {
            if (!((ItemDate)ValueExpression).IsValid)
            {
                foreach (BusinessConstraint brokenRule in ((ItemDate)ValueExpression).GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName);
                }
            }
        }
    }
}

