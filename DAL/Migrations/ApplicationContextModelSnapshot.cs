﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("DAL.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpireRefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastVisit")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DAL.Models.Chat", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("DAL.Models.Contact", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayedName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("DAL.Models.Message", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserChat", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayedName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChat");
                });

            modelBuilder.Entity("DAL.Models.Account", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Contact", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Message", b =>
                {
                    b.HasOne("DAL.Models.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.UserChat", b =>
                {
                    b.HasOne("DAL.Models.Chat", "Chat")
                        .WithMany("UserChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Chat", b =>
                {
                    b.Navigation("UserChats");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
