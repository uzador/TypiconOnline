﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    public class ProkeimenonBusinessConstraint
    {
        public static readonly BusinessConstraint InvalidIhos = new BusinessConstraint("Неверный глас (значения могут быть с 1 до 8).");
        public static readonly BusinessConstraint StihoiRequired = new BusinessConstraint("Хотя бы один стих должен быть определен.");
    }
}