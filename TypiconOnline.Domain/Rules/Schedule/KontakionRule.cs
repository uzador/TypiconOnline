﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для использования кондака для правила канона 
    /// </summary>
    public class KontakionRule : KanonasItemRuleBase, IViewModelElement
    {
        public KontakionRule(string name, IElementViewModelFactory<KontakionRule> viewModelFactory) : base(name)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in KontakionRule");
        }

        /// <summary>
        /// Признак, показывать ли вместе с кондаком и Икос. По умолчанию - false
        /// </summary>
        public bool ShowIkos { get; set; } = false;

        protected IElementViewModelFactory<KontakionRule> ViewModelFactory { get; }

        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            if (base.Calculate(settings) is Kontakion kontakion)
            {
                result = kontakion.ToYmnosStructure(ShowIkos);
            }

            return result;
        }

        public void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<KontakionRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
