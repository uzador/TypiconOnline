﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KKontakionRuleBusinessConstraint
    {
        
        public static readonly BusinessConstraint SourceRequired = new BusinessConstraint("Отсутствуют определение источника песнопения (книги).");
        public static readonly BusinessConstraint KanonasRequired = new BusinessConstraint("Отсутствуют определение канона в источнике песнопения (книги).");
    }
}