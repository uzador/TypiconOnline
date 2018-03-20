﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using TypiconMigrationTool.Core;
using TypiconOnline.Domain.Rules;

namespace TypiconMigrationTool.Core.Migrations
{
    [DbContext(typeof(MigrationContext))]
    partial class MigrationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("TypiconOnline.Domain.Books.Easter.EasterItem", b =>
                {
                    b.Property<DateTime>("Date");

                    b.HasKey("Date");

                    b.ToTable("EasterItem");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Katavasia.Katavasia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Katavasia");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Oktoikh.OktoikhDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Definition");

                    b.Property<int>("Ihos");

                    b.HasKey("Id");

                    b.ToTable("OktoikhDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Psalter.Psalm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Psalm");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.TheotokionApp.TheotokionApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Definition");

                    b.Property<int>("Ihos");

                    b.Property<int>("Place");

                    b.HasKey("Id");

                    b.ToTable("TheotokionApp");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorship", b =>
                {
                    b.Property<int>("DayRuleId");

                    b.Property<int>("DayWorshipId");

                    b.Property<int?>("DayRuleId1");

                    b.HasKey("DayRuleId", "DayWorshipId");

                    b.HasIndex("DayRuleId1");

                    b.HasIndex("DayWorshipId");

                    b.ToTable("DayRuleWorship");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Day");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Day");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.DayWorship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DayRuleId");

                    b.Property<string>("Definition");

                    b.Property<bool>("IsCelebrating");

                    b.Property<int?>("ModifiedRuleId");

                    b.Property<int?>("ParentId")
                        .IsRequired();

                    b.Property<bool>("UseFullName");

                    b.HasKey("Id");

                    b.HasIndex("DayRuleId");

                    b.HasIndex("ModifiedRuleId");

                    b.HasIndex("ParentId");

                    b.ToTable("DayWorship");
                });

            modelBuilder.Entity("TypiconOnline.Domain.ItemTypes.ItemDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Expression");

                    b.HasKey("Id");

                    b.ToTable("ItemDate");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("CommonRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.DayRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsAddition");

                    b.Property<int>("OwnerId");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("DayRule");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DayRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsAddition");

                    b.Property<bool>("IsLastName");

                    b.Property<int>("ModifiedYearId");

                    b.Property<int>("Priority");

                    b.Property<int?>("RuleEntityId");

                    b.Property<string>("ShortName");

                    b.Property<bool>("UseFullName");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedYearId");

                    b.HasIndex("RuleEntityId");

                    b.ToTable("ModifiedRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TypiconEntityId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("ModifiedYear");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.Kathisma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("Kathisma");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.PsalmLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EndStihos");

                    b.Property<int?>("PsalmId");

                    b.Property<int?>("SlavaElementId");

                    b.Property<int?>("StartStihos");

                    b.HasKey("Id");

                    b.HasIndex("PsalmId");

                    b.HasIndex("SlavaElementId");

                    b.ToTable("PsalmLink");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.SlavaElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KathismaId");

                    b.HasKey("Id");

                    b.HasIndex("KathismaId");

                    b.ToTable("SlavaElement");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAddition");

                    b.Property<bool>("IsTemplate");

                    b.Property<int?>("Number");

                    b.Property<int?>("Owner.Id");

                    b.Property<int>("OwnerId");

                    b.Property<int>("Priority");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TemplateId");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("Owner.Id")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.HasIndex("TemplateId");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("Sign");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("TypiconEntity");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultLanguage");

                    b.Property<int?>("TypiconEntity.Id");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconEntity.Id")
                        .IsUnique();

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("TypiconSettings");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Days.Day");

                    b.Property<int?>("DateBId");

                    b.Property<int?>("DateId");

                    b.HasIndex("DateBId");

                    b.HasIndex("DateId");

                    b.ToTable("MenologyDay");

                    b.HasDiscriminator().HasValue("MenologyDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.TriodionDay", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Days.Day");

                    b.Property<int>("DaysFromEaster");

                    b.ToTable("TriodionDays");

                    b.HasDiscriminator().HasValue("TriodionDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.MenologyRule", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Typicon.DayRule");

                    b.Property<int?>("DateBId");

                    b.Property<int?>("DateId");

                    b.HasIndex("DateBId");

                    b.HasIndex("DateId");

                    b.HasIndex("OwnerId");

                    b.ToTable("MenologyRule");

                    b.HasDiscriminator().HasValue("MenologyRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Typicon.DayRule");

                    b.Property<int>("DaysFromEaster");

                    b.HasIndex("OwnerId");

                    b.ToTable("TriodionRule");

                    b.HasDiscriminator().HasValue("TriodionRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule")
                        .WithMany("DayRuleWorships")
                        .HasForeignKey("DayRuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "DayRule")
                        .WithMany()
                        .HasForeignKey("DayRuleId1");

                    b.HasOne("TypiconOnline.Domain.Days.DayWorship", "DayWorship")
                        .WithMany()
                        .HasForeignKey("DayWorshipId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.DayWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule")
                        .WithMany("DayWorships")
                        .HasForeignKey("DayRuleId");

                    b.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule")
                        .WithMany("DayWorships")
                        .HasForeignKey("ModifiedRuleId");

                    b.HasOne("TypiconOnline.Domain.Days.Day", "Parent")
                        .WithMany("DayWorships")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "WorshipName", b1 =>
                        {
                            b1.Property<int>("DayWorshipId");

                            b1.Property<string>("StringExpression");

                            b1.ToTable("DayWorship");

                            b1.HasOne("TypiconOnline.Domain.Days.DayWorship")
                                .WithOne("WorshipName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "DayWorshipId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "WorshipShortName", b1 =>
                        {
                            b1.Property<int?>("DayWorshipId");

                            b1.Property<string>("StringExpression");

                            b1.ToTable("DayWorship");

                            b1.HasOne("TypiconOnline.Domain.Days.DayWorship")
                                .WithOne("WorshipShortName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "DayWorshipId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithMany("CommonRules")
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.DayRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Sign", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", "Parent")
                        .WithMany("ModifiedRules")
                        .HasForeignKey("ModifiedYearId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "RuleEntity")
                        .WithMany()
                        .HasForeignKey("RuleEntityId");

                    b.OwnsOne("TypiconOnline.Domain.Typicon.Modifications.DayWorshipsFilter", "Filter", b1 =>
                        {
                            b1.Property<int>("ModifiedRuleId");

                            b1.Property<int?>("ExcludedItem");

                            b1.Property<int?>("IncludedItem");

                            b1.Property<bool?>("IsCelebrating");

                            b1.ToTable("ModifiedRule");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule")
                                .WithOne("Filter")
                                .HasForeignKey("TypiconOnline.Domain.Typicon.Modifications.DayWorshipsFilter", "ModifiedRuleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "TypiconEntity")
                        .WithMany("ModifiedYears")
                        .HasForeignKey("TypiconEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.Kathisma", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "TypiconEntity")
                        .WithMany("Kathismas")
                        .HasForeignKey("TypiconEntityId");

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemText", "NumberName", b1 =>
                        {
                            b1.Property<int?>("KathismaId");

                            b1.Property<string>("StringExpression");

                            b1.ToTable("Kathisma");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Psalter.Kathisma")
                                .WithOne("NumberName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemText", "KathismaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.PsalmLink", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Books.Psalter.Psalm", "Psalm")
                        .WithMany()
                        .HasForeignKey("PsalmId");

                    b.HasOne("TypiconOnline.Domain.Typicon.Psalter.SlavaElement")
                        .WithMany("PsalmLinks")
                        .HasForeignKey("SlavaElementId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.SlavaElement", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Psalter.Kathisma")
                        .WithMany("SlavaElements")
                        .HasForeignKey("KathismaId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconSettings")
                        .WithOne("TemplateSunday")
                        .HasForeignKey("TypiconOnline.Domain.Typicon.Sign", "Owner.Id");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.Sign", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithMany("Signs")
                        .HasForeignKey("TypiconEntityId");

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemText", "SignName", b1 =>
                        {
                            b1.Property<int>("SignId");

                            b1.Property<string>("StringExpression");

                            b1.ToTable("Sign");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Sign")
                                .WithOne("SignName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemText", "SignId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconEntity", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconSettings", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithOne("Settings")
                        .HasForeignKey("TypiconOnline.Domain.Typicon.TypiconSettings", "TypiconEntity.Id");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "TypiconEntity")
                        .WithMany()
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "DateB")
                        .WithMany()
                        .HasForeignKey("DateBId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date")
                        .WithMany()
                        .HasForeignKey("DateId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.MenologyRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "DateB")
                        .WithMany()
                        .HasForeignKey("DateBId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date")
                        .WithMany()
                        .HasForeignKey("DateId");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany("MenologyRules")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany("TriodionRules")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
