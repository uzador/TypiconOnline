﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class TriodionRuleConfiguration : IEntityTypeConfiguration<TriodionRule>
    {
        public void Configure(EntityTypeBuilder<TriodionRule> builder)
        {
            //builder.OwnsOne(c => c.Date).Property(d => d.Expression).IsRequired(false);
            //builder.OwnsOne(c => c.DateB).Property(d => d.Expression).IsRequired(false);

            //builder.Property(c => c.DateB.Expression).
            //    HasColumnName("DateB").
            //    HasMaxLength(7).
            //    IsRequired(false);

            //builder.HasOne(e => e.Owner).
            //    WithMany();

            builder.HasOne(e => e.Template).
                WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
