﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Easter;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DaysFromEaster : Int
    {
        IEasterContext easterContext;

        public DaysFromEaster(string name, IEasterContext context) : base(name)
        {
            easterContext = context ?? throw new ArgumentNullException("IEasterContext");
        }

        public DateExpression ChildExpression { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            DateTime easterDate = BookStorage.Instance.Easters.GetCurrentEaster(date.Year);

            ChildExpression.Interpret(date, handler);

            //DateTime easterDate = handler.GetCurrentEaster(date.Year);

            ValueCalculated = ((DateTime)ChildExpression.ValueCalculated).Subtract(easterDate).Days;
            ValueExpression = new ItemInt((int)ValueCalculated);
        }

        protected override void Validate()
        {
            if (ChildExpression == null)
            {
                AddBrokenConstraint(DaysFromEasterBusinessConstraint.DateAbsense, ElementName);
            }
            else
            {
                if (ChildExpression.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in ChildExpression.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.DateNodeName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }
}

