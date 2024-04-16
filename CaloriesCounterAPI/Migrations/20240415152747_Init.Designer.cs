﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CaloriesCounterAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CaloriesCounterAPI.Migrations
{
    [DbContext(typeof(CaloriesCounterAPIContext))]
    [Migration("20240415152747_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CaloriesCounterAPI.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<List<int>>("AmmoutOfProduct")
                        .HasColumnType("integer[]");

                    b.Property<int>("CarbsForMeal")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("FatForMeal")
                        .HasColumnType("integer");

                    b.Property<int>("KcalForMeal")
                        .HasColumnType("integer");

                    b.Property<int>("ProteinForMeal")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("CaloriesCounterAPI.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("Carbs")
                        .HasColumnType("integer");

                    b.Property<int>("Fat")
                        .HasColumnType("integer");

                    b.Property<int>("Kcal")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Protein")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MealProduct", b =>
                {
                    b.Property<int>("MealsId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsID")
                        .HasColumnType("integer");

                    b.HasKey("MealsId", "ProductsID");

                    b.HasIndex("ProductsID");

                    b.ToTable("MealProduct");
                });

            modelBuilder.Entity("MealProduct", b =>
                {
                    b.HasOne("CaloriesCounterAPI.Models.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaloriesCounterAPI.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
