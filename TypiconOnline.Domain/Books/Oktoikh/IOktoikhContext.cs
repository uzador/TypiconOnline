﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public interface IOktoikhContext
    {
        OktoikhDay Get(DateTime date);
        int CalculateIhosNumber(DateTime date);
        string GetSundayName(DateTime date, string language, string stringToPaste = null);
        string GetWeekName(DateTime date, bool isShortName);
    }
}
