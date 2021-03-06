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
    /// Описание службы малой вечерни
    /// </summary>
    [Serializable]
    [XmlRoot(RuleConstants.MikrosEsperinosNode)]
    public class MikrosEsperinos : DayElementBase
    {
        public MikrosEsperinos() { }

        #region Properties
        /// <summary>
        /// Господи воззвах
        /// </summary>
        [XmlElement(RuleConstants.KekragariaNode)]
        public YmnosStructure Kekragaria { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        [XmlElement(RuleConstants.ApostichaNode)]
        public YmnosStructure Aposticha { get; set; }
        /// <summary>
        /// Отпустительный тропарь
        /// </summary>
        [XmlElement(RuleConstants.TroparionNode)]
        public YmnosStructure Troparion { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Kekragaria?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kekragaria, RuleConstants.MikrosEsperinosNode + "." + RuleConstants.KekragariaNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, RuleConstants.MikrosEsperinosNode + "." + RuleConstants.ApostichaNode);
            }

            if (Troparion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Troparion, RuleConstants.MikrosEsperinosNode + "." + RuleConstants.TroparionNode);
            }
        }
    }
}
