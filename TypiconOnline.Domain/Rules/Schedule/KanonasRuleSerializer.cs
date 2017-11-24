﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KanonasRuleSerializer : YmnosStructureRuleSerializer
    {
        public KanonasRuleSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.KanonasRuleNode };
        }

        protected override ExecContainer CreateObject(XmlDescriptor d)
        {
            return new KanonasRule(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, ExecContainer container)
        {
            base.FillObject(d, container);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.KanonasRuleEktenis3AttrName];
            (container as KanonasRule).Ektenis3 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = d.Element.Attributes[RuleConstants.KanonasRuleEktenis6AttrName];
            (container as KanonasRule).Ektenis6 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = d.Element.Attributes[RuleConstants.KanonasRuleEktenis9AttrName];
            (container as KanonasRule).Ektenis9 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = d.Element.Attributes[RuleConstants.KanonasRulePanagiasAttrName];
            (container as KanonasRule).Panagias = (attr != null) ? new CommonRuleElement(attr.Value) : null;

        }
    }
}
