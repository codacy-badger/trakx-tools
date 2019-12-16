﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trakx.Data.Models.Index;

namespace Trakx.Data.Models.Migrations
{
    [DbContext(typeof(IndexRepositoryContext))]
    partial class IndexRepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trakx.Data.Models.Index.ComponentDefinition", b =>
                {
                    b.Property<Guid>("ComponentDefinitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<int>("Decimals")
                        .HasColumnType("int");

                    b.Property<string>("IndexDefinitionSymbol")
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<Guid>("InitialValuationComponentValuationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(512)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.HasKey("ComponentDefinitionId");

                    b.HasIndex("IndexDefinitionSymbol");

                    b.HasIndex("InitialValuationComponentValuationId");

                    b.ToTable("ComponentDefinition");
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.ComponentValuation", b =>
                {
                    b.Property<Guid>("ComponentValuationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IndexValuationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(38, 18)");

                    b.Property<string>("QuoteCurrency")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(38, 18)");

                    b.HasKey("ComponentValuationId");

                    b.HasIndex("IndexValuationId");

                    b.ToTable("ComponentValuation");
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.IndexDefinition", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<Guid>("InitialValuationIndexValuationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(512)");

                    b.Property<int>("NaturalUnit")
                        .HasColumnType("int");

                    b.HasKey("Symbol");

                    b.HasIndex("InitialValuationIndexValuationId");

                    b.ToTable("IndexDefinitions");
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.IndexValuation", b =>
                {
                    b.Property<Guid>("IndexValuationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("NetAssetValue")
                        .HasColumnType("decimal(38, 18)");

                    b.Property<string>("QuoteCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("IndexValuationId");

                    b.ToTable("IndexValuation");
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.ComponentDefinition", b =>
                {
                    b.HasOne("Trakx.Data.Models.Index.IndexDefinition", null)
                        .WithMany("ComponentDefinitions")
                        .HasForeignKey("IndexDefinitionSymbol");

                    b.HasOne("Trakx.Data.Models.Index.ComponentValuation", "InitialValuation")
                        .WithMany()
                        .HasForeignKey("InitialValuationComponentValuationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.ComponentValuation", b =>
                {
                    b.HasOne("Trakx.Data.Models.Index.IndexValuation", null)
                        .WithMany("Valuations")
                        .HasForeignKey("IndexValuationId");
                });

            modelBuilder.Entity("Trakx.Data.Models.Index.IndexDefinition", b =>
                {
                    b.HasOne("Trakx.Data.Models.Index.IndexValuation", "InitialValuation")
                        .WithMany()
                        .HasForeignKey("InitialValuationIndexValuationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
