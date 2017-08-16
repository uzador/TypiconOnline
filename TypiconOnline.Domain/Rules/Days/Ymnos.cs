﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнопение
    /// </summary>
    public class Ymnos : RuleElement
    {
        public Ymnos(Ymnos source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Ymnos");
            }

            ElementName = string.Copy(source.ElementName);
            Stihos = new ItemText(source.Stihos.StringExpression);
            Text = new ItemText(source.Text.StringExpression);
        }

        public Ymnos(XmlNode node) : base(node)
        {
            //ymnos
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.YmnosTextNode);
            Text =  new ItemText((elemNode != null) ? elemNode.OuterXml : string.Empty);

            elemNode = node.SelectSingleNode(RuleConstants.YmnosStihosNode);
            Stihos = new ItemText((elemNode != null) ? elemNode.OuterXml : string.Empty);
        }

        /// <summary>
        /// Стих, предваряющий песнопение
        /// </summary>
        public ItemText Stihos { get; set; }
        /// <summary>
        /// Текст песнопения
        /// </summary>
        public ItemText Text { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //ничего
        }

        protected override void Validate()
        {
            if (Text == null || Text.IsEmpty == true)
            {
                AddBrokenConstraint(YmnosBusinessConstraint.TextRequired, ElementName);
            }

            if (Text?.IsValid == false)
            {
                AppendAllBrokenConstraints(Text, ElementName + "." + RuleConstants.YmnosTextNode);
            }

            if (Stihos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Stihos, ElementName + "." + RuleConstants.YmnosStihosNode);
            }
        }
    }
}
