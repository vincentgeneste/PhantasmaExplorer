﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Phantasma.Explorer.Persistance;

namespace Phantasma.Explorer.Migrations
{
    [DbContext(typeof(ExplorerDbContext))]
    [Migration("20190123162708_Added Transaciton Count Token")]
    partial class AddedTransacitonCountToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Account", b =>
                {
                    b.Property<string>("Address")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Address");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.AccountTransaction", b =>
                {
                    b.Property<string>("AccountId");

                    b.Property<string>("TransactionId");

                    b.HasKey("AccountId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("AccountTransaction");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.App", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Icon");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Block", b =>
                {
                    b.Property<string>("Hash")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChainAddress");

                    b.Property<string>("ChainName");

                    b.Property<long>("Height");

                    b.Property<string>("Payload");

                    b.Property<string>("PreviousHash");

                    b.Property<decimal>("Reward");

                    b.Property<long>("Timestamp");

                    b.Property<string>("ValidatorAddress");

                    b.HasKey("Hash");

                    b.HasIndex("ChainAddress");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Chain", b =>
                {
                    b.Property<string>("Address")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Height");

                    b.Property<string>("Name");

                    b.Property<string>("ParentAddress");

                    b.HasKey("Address");

                    b.ToTable("Chains");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.NonFungibleToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountAddress");

                    b.Property<string>("Chain");

                    b.Property<string>("TokenSymbol");

                    b.HasKey("Id");

                    b.HasIndex("AccountAddress");

                    b.ToTable("NonFungibleTokens");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Token", b =>
                {
                    b.Property<string>("Symbol")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrentSupply");

                    b.Property<long>("Decimals");

                    b.Property<int>("Flags");

                    b.Property<string>("MaxSupply");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerAddress");

                    b.Property<long>("TransactionCount");

                    b.HasKey("Symbol");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Transaction", b =>
                {
                    b.Property<string>("Hash")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlockHash");

                    b.Property<string>("Result");

                    b.Property<string>("Script");

                    b.Property<long>("Timestamp");

                    b.HasKey("Hash");

                    b.HasIndex("BlockHash");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Account", b =>
                {
                    b.OwnsMany("Phantasma.Explorer.Domain.ValueObjects.FBalance", "TokenBalance", b1 =>
                        {
                            b1.Property<string>("Address");

                            b1.Property<string>("Chain");

                            b1.Property<string>("TokenSymbol");

                            b1.Property<string>("Amount");

                            b1.HasKey("Address", "Chain", "TokenSymbol", "Amount");

                            b1.ToTable("FBalance");

                            b1.HasOne("Phantasma.Explorer.Domain.Entities.Account")
                                .WithMany("TokenBalance")
                                .HasForeignKey("Address")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.AccountTransaction", b =>
                {
                    b.HasOne("Phantasma.Explorer.Domain.Entities.Account", "Account")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Phantasma.Explorer.Domain.Entities.Transaction", "Transaction")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Block", b =>
                {
                    b.HasOne("Phantasma.Explorer.Domain.Entities.Chain", "Chain")
                        .WithMany("Blocks")
                        .HasForeignKey("ChainAddress")
                        .HasConstraintName("FK_Blocks_Chains");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.NonFungibleToken", b =>
                {
                    b.HasOne("Phantasma.Explorer.Domain.Entities.Account", "Account")
                        .WithMany("NonFungibleTokens")
                        .HasForeignKey("AccountAddress");
                });

            modelBuilder.Entity("Phantasma.Explorer.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Phantasma.Explorer.Domain.Entities.Block", "Block")
                        .WithMany("Transactions")
                        .HasForeignKey("BlockHash")
                        .HasConstraintName("FK_Transactions_Blocks");

                    b.OwnsMany("Phantasma.Explorer.Domain.ValueObjects.Event", "Events", b1 =>
                        {
                            b1.Property<string>("Hash");

                            b1.Property<string>("Data");

                            b1.Property<string>("EventAddress");

                            b1.Property<int>("EventKind");

                            b1.HasKey("Hash", "Data", "EventAddress", "EventKind");

                            b1.ToTable("Event");

                            b1.HasOne("Phantasma.Explorer.Domain.Entities.Transaction")
                                .WithMany("Events")
                                .HasForeignKey("Hash")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
