﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipRule : ExecContainer, ICustomInterpreted
    {
        public WorshipRule(string name) : base(name) { }

        public WorshipRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.ServiceTimeAttrName];
            Time = new ItemTime((attr != null) ? attr.Value : string.Empty);

            attr = node.Attributes[RuleConstants.ServiceNameAttrName];
            Name = (attr != null) ? attr.Value : string.Empty;

            attr = node.Attributes[RuleConstants.ServiceIsDayBeforeAttrName];
            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                IsDayBefore = showPsalm;
            }

            attr = node.Attributes[RuleConstants.ServiceAdditionalNameAttrName];
            AdditionalName = (attr != null) ? attr.Value : string.Empty;
        }

        #region Properties

        public ItemTime Time { get; set; }
        public string Name { get; set; }
        public bool IsDayBefore { get; set; } = false;
        public string AdditionalName { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<WorshipRule>())
            {
                base.InnerInterpret(date, handler);

                handler.Execute(this);

                //обработка дочерних элементов совершается в handler.Execute(this);
                //base.InnerInterpret(date, handler);
            }
        }

        protected override void Validate()
        {
            if (!Time.IsValid)
            {
                AddBrokenConstraint(ServiceBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(ServiceBusinessConstraint.NameReqiured, ElementName);
            }

            foreach (RuleElement element in ChildElements)
            {
                //добавляем ломаные правила к родителю
                if (!element.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in element.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }
}

