﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerRoot
    {
        BookStorage BookStorage { get; }
        RuleSerializerContainerBase<T> Factory<T>() where T : RuleElement;
        RuleSerializerContainerBase<T> Factory<T, U>() where T : RuleElement;
    }
}
