﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Вычисляет логическую операцию OR
    /// </summary>
    public class Or : LogicalOperation
    {
        public Or(string name) : base(name) { }

        protected override bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue)
        {
            return (previousValue != null) ? ((bool)exp1.ValueCalculated || (bool)exp2.ValueCalculated || (bool)previousValue)
                : ((bool)exp1.ValueCalculated || (bool)exp2.ValueCalculated);
        }
    }
}
