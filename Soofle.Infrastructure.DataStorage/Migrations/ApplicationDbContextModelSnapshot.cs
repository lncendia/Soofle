﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Soofle.Infrastructure.DataStorage.Context;

#nullable disable

namespace Soofle.Infrastructure.DataStorage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ReportElementModelSequence");

            modelBuilder.Entity("PublicationReportModelUserModel", b =>
                {
                    b.Property<Guid>("LinkedUsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublicationReportModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LinkedUsersId", "PublicationReportModelId");

                    b.HasIndex("PublicationReportModelId");

                    b.ToTable("PublicationReportModelUserModel");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.LinkModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<Guid>("User1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User2Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("User1Id");

                    b.HasIndex("User2Id");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.ParticipantModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Vip")
                        .HasColumnType("bit");

                    b.Property<long>("VkId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentParticipantId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.ProxyModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Proxies");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.ReportLogModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("FinishedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Success")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ReportLogs");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportElementModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [ReportElementModelSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("VkId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsSucceeded")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsLoaded")
                        .HasColumnType("bit");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.TransactionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTimeOffset?>("ConfirmationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentSystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("PaymentSystemUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ExpirationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProxyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("SubscriptionDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("Target")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("TargetSetTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("VkName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProxyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport.CommentReportElementModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportElementModel");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("LikeChatName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Vip")
                        .HasColumnType("bit");

                    b.HasIndex("ReportId");

                    b.ToTable("CommentReportElements");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport.LikeReportElementModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportElementModel");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("LikeChatName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Likes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Vip")
                        .HasColumnType("bit");

                    b.HasIndex("ReportId");

                    b.ToTable("LikeReportElements");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport.ParticipantReportElementModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportElementModel");

                    b.Property<string>("NewName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParticipantType")
                        .HasColumnType("int");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasIndex("ReportId");

                    b.ToTable("ParticipantReportElements");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport.ParticipantReportModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportModel");

                    b.Property<long>("VkId")
                        .HasColumnType("bigint");

                    b.ToTable("ParticipantReports");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportModel");

                    b.Property<bool>("AllParticipants")
                        .HasColumnType("bit");

                    b.Property<string>("Hashtag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Process")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("SearchStartDate")
                        .HasColumnType("datetimeoffset");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport.CommentReportModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel");

                    b.ToTable("CommentReports");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport.LikeReportModel", b =>
                {
                    b.HasBaseType("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel");

                    b.ToTable("LikeReports");
                });

            modelBuilder.Entity("PublicationReportModelUserModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("LinkedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel", null)
                        .WithMany()
                        .HasForeignKey("PublicationReportModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.LinkModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.ParticipantModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.ParticipantModel", "ParentParticipant")
                        .WithMany()
                        .HasForeignKey("ParentParticipantId");

                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentParticipant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.ReportLogModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.Base.ReportModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel", "Report")
                        .WithMany("Publications")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.TransactionModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.UserModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.ProxyModel", "Proxy")
                        .WithMany()
                        .HasForeignKey("ProxyId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Proxy");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport.CommentReportElementModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport.CommentReportModel", "Report")
                        .WithMany("ReportElementsList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport.LikeReportElementModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport.LikeReportModel", "Report")
                        .WithMany("ReportElementsList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport.ParticipantReportElementModel", b =>
                {
                    b.HasOne("Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport.ParticipantReportModel", "Report")
                        .WithMany("ReportElementsList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport.ParticipantReportModel", b =>
                {
                    b.Navigation("ReportElementsList");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport.PublicationReportModel", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport.CommentReportModel", b =>
                {
                    b.Navigation("ReportElementsList");
                });

            modelBuilder.Entity("Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport.LikeReportModel", b =>
                {
                    b.Navigation("ReportElementsList");
                });
#pragma warning restore 612, 618
        }
    }
}
