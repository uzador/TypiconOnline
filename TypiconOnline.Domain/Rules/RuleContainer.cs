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
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules
{
    public abstract class RuleContainer : RuleExecutable
    {
        //private List<RuleElement> _childElements;
        public RuleContainer()
        {
            ChildElements = new List<RuleElement>();
        }

        public RuleContainer(XmlNode xmlNode) : base(xmlNode)
        {
            ChildElements = new List<RuleElement>();

            if (xmlNode.HasChildNodes)
            {
                //ParentElement = null;

                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    RuleElement element = Factories.RuleFactory.CreateElement(childNode);
                    ChildElements.Add(element);
                }
            }
        }

        #region Properties

        //public RuleElement ParentElement { get; set; }

        public virtual List<RuleElement> ChildElements { get; set; }


        #endregion

        #region Methods

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid)
            {
                foreach (RuleElement el in ChildElements)
                {
                    el.Interpret(date, handler);
                }
            }
        }

        protected override void Validate()
        {
            if (ChildElements.Count == 0)
            {
                AddBrokenConstraint(RuleContainerBusinessConstraint.DefinitionContainerChildrenRequired);
            }
            else
            {
                foreach (RuleElement element in ChildElements)
                {
                    //добавляем ломаные правила к родителю
                    if (!element.IsValid)
                    {
                        AppendAllBrokenConstraints(element);
                    }
                }
            }
        }

        #endregion
    }
}

