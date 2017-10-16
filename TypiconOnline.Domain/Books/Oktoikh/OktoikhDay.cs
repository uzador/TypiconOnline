﻿using System;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public class OktoikhDay : DayStructureBase
    {
        public int Ihos { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        protected override void Validate()
        {
            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(OktoikhDayBusinessConstraint.InvalidIhos);
            }
        }
    }
}

