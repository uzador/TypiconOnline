﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    public class Makarios : DayElementBase
    {
        public Makarios(XmlNode node) 
        {

        }

        #region Properties

        public List<MakariosOdi> Odes { get; set; }

        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
