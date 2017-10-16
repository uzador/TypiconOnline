﻿using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Описание текста службы празднику
    /// </summary>
    public class DayService : DayStructureBase
    {
        public DayService()
        {
            ServiceName = new ItemText();
            ServiceShortName = new ItemText();
        }

        /// <summary>
        /// Наименование праздника
        /// </summary>
        public virtual ItemText ServiceName { get; set; }
        /// <summary>
        /// Краткое наименование праздника (для Недель - используется при формировании расписания)
        /// </summary>
        public virtual ItemText ServiceShortName { get; set; }
        /// <summary>
        /// Признак, использовать ли полное имя при составлении расписания
        /// Например, имеет значение true для праздника Недели по Рождестве
        /// </summary>
        public virtual bool UseFullName { get; set; }
        /// <summary>
        /// Признак Господского или Богородичного праздника, его предпразднества или попразднества
        /// </summary>
        public virtual bool IsCelebrating { get; set; }
        /// <summary>
        /// День Минеи или Триоди, которому принадлежит данное описание текста службы
        /// </summary>
        public virtual Day Parent { get; set; }
        
        protected override void Validate()
        {
            if (!ServiceName.IsValid)//(ServiceName?.IsValid == false)
            {
                AppendAllBrokenConstraints(ServiceName);
            }
        }
    }
}

