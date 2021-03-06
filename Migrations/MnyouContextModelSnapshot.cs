﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using mn_you.Models.SQLite;

namespace mnyou.Migrations
{
    [DbContext(typeof(MnyouContext))]
    partial class MnyouContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("mn_you.Models.SQLite.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Bio");

                    b.Property<string>("City");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FacebookUrl");

                    b.Property<string>("InstgramUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Slug");

                    b.Property<string>("State");

                    b.Property<string>("TwitterUrl");

                    b.Property<string>("Zip");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });
        }
    }
}
