﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class DayMap : EntityTypeConfiguration<Day>
    {
        public DayMap()
        {
            Property(c => c.Name1.StringExpression).IsRequired();

            Property(c => c.Name2.StringExpression).IsRequired();

            Property(c => c.Name3.StringExpression).IsRequired();
        }
    }
}
