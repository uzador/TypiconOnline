﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил, которые возвращают тексты богослужений
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICalcStructureElement//<T> where T: DayElementBase
    {
        DayElementBase Calculate(DateTime date, RuleHandlerSettings settings);
    }
}
