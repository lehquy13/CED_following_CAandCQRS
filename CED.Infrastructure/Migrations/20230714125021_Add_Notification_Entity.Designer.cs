﻿// <auto-generated />
using System;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230714125021_Add_Notification_Entity")]
    partial class Add_Notification_Entity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CED.Domain.ClassInformations.ClassInformation", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("AcademicLevelRequirement")
                    .HasColumnType("int");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<float>("ChargeFee")
                    .HasColumnType("real");

                b.Property<string>("ContactNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("CreatorUserId")
                    .HasColumnType("bigint");

                b.Property<long?>("DeleterUserId")
                    .HasColumnType("bigint");

                b.Property<DateTime?>("DeletionTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Description")
                    .IsRequired()
                    .IsUnicode(true)
                    .HasColumnType("nvarchar(max)");

                b.Property<float>("Fee")
                    .HasColumnType("real");

                b.Property<int>("GenderRequirement")
                    .HasColumnType("int");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                b.Property<DateTime?>("LastModificationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("LastModifierUserId")
                    .HasColumnType("bigint");

                b.Property<int>("LearnerGender")
                    .HasColumnType("int");

                b.Property<Guid?>("LearnerId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("LearnerName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("LearningMode")
                    .HasColumnType("int");

                b.Property<int>("MinutePerSession")
                    .HasColumnType("int");

                b.Property<int>("NumberOfLearner")
                    .HasColumnType("int");

                b.Property<int>("SessionPerWeek")
                    .HasColumnType("int");

                b.Property<int>("Status")
                    .HasColumnType("int");

                b.Property<Guid>("SubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<Guid?>("TutorId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("LearnerId");

                b.HasIndex("SubjectId");

                b.HasIndex("TutorId");

                b.ToTable("ClassInformation", (string)null);
            });

            modelBuilder.Entity("CED.Domain.ClassInformations.RequestGettingClass", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("ClassInformationId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RequestStatus")
                    .HasColumnType("int");

                b.Property<Guid>("TutorId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("ClassInformationId");

                b.HasIndex("TutorId");

                b.ToTable("RequestGettingClass", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Notifications.Notification", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Message")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("NotificationType")
                    .HasColumnType("int");

                b.Property<Guid>("ObjectId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.ToTable("Notification", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Review.TutorReview", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("ClassInformationId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("CreatorUserId")
                    .HasColumnType("bigint");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("LastModificationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("LastModifierUserId")
                    .HasColumnType("bigint");

                b.Property<short>("Rate")
                    .HasColumnType("smallint");

                b.HasKey("Id");

                b.HasIndex("ClassInformationId")
                    .IsUnique();

                b.ToTable("TutorReview", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Subjects.Subject", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("CreatorUserId")
                    .HasColumnType("bigint");

                b.Property<long?>("DeleterUserId")
                    .HasColumnType("bigint");

                b.Property<DateTime?>("DeletionTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                b.Property<DateTime?>("LastModificationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("LastModifierUserId")
                    .HasColumnType("bigint");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Subject", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Subjects.TutorMajor", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("SubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("TutorId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("SubjectId");

                b.HasIndex("TutorId");

                b.ToTable("TutorMajor", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Subscribers.Subscriber", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("TutorId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("TutorId");

                b.ToTable("Subscriber", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.City", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("City", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.District", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("CityId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("EnglishName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("CityId");

                b.ToTable("District", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.Tutor", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("AcademicLevel")
                    .HasColumnType("int");

                b.Property<bool>("IsVerified")
                    .HasColumnType("bit");

                b.Property<short>("Rate")
                    .HasColumnType("smallint");

                b.Property<string>("University")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Tutor", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.TutorVerificationInfo", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Image")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("TutorId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("TutorId");

                b.ToTable("TutorVerificationInfo", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("BirthYear")
                    .HasColumnType("int");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("CreatorUserId")
                    .HasColumnType("bigint");

                b.Property<long?>("DeleterUserId")
                    .HasColumnType("bigint");

                b.Property<DateTime?>("DeletionTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<int>("Gender")
                    .HasColumnType("int");

                b.Property<string>("Image")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                b.Property<bool>("IsEmailConfirmed")
                    .HasColumnType("bit");

                b.Property<DateTime?>("LastModificationTime")
                    .HasColumnType("datetime2");

                b.Property<long?>("LastModifierUserId")
                    .HasColumnType("bigint");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Role")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("User", (string)null);
            });

            modelBuilder.Entity("CED.Domain.Users.Ward", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("DistrictId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("WardLevel")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("DistrictId");

                b.ToTable("Ward", (string)null);
            });

            modelBuilder.Entity("CED.Domain.ClassInformations.ClassInformation", b =>
            {
                b.HasOne("CED.Domain.Users.User", null)
                    .WithMany()
                    .HasForeignKey("LearnerId");

                b.HasOne("CED.Domain.Subjects.Subject", null)
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("CED.Domain.Users.Tutor", null)
                    .WithMany()
                    .HasForeignKey("TutorId");
            });

            modelBuilder.Entity("CED.Domain.ClassInformations.RequestGettingClass", b =>
            {
                b.HasOne("CED.Domain.ClassInformations.ClassInformation", null)
                    .WithMany()
                    .HasForeignKey("ClassInformationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("CED.Domain.Users.Tutor", null)
                    .WithMany()
                    .HasForeignKey("TutorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Review.TutorReview", b =>
            {
                b.HasOne("CED.Domain.ClassInformations.ClassInformation", null)
                    .WithOne()
                    .HasForeignKey("CED.Domain.Review.TutorReview", "ClassInformationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Subjects.TutorMajor", b =>
            {
                b.HasOne("CED.Domain.Subjects.Subject", null)
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("CED.Domain.Users.Tutor", null)
                    .WithMany()
                    .HasForeignKey("TutorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Subscribers.Subscriber", b =>
            {
                b.HasOne("CED.Domain.Users.Tutor", null)
                    .WithMany()
                    .HasForeignKey("TutorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Users.District", b =>
            {
                b.HasOne("CED.Domain.Users.City", null)
                    .WithMany()
                    .HasForeignKey("CityId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Users.Tutor", b =>
            {
                b.HasOne("CED.Domain.Users.User", null)
                    .WithOne()
                    .HasForeignKey("CED.Domain.Users.Tutor", "Id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Users.TutorVerificationInfo", b =>
            {
                b.HasOne("CED.Domain.Users.Tutor", null)
                    .WithMany()
                    .HasForeignKey("TutorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("CED.Domain.Users.Ward", b =>
            {
                b.HasOne("CED.Domain.Users.District", null)
                    .WithMany()
                    .HasForeignKey("DistrictId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}