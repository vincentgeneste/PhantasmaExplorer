﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phantasma.Core;
using Phantasma.Cryptography;
using Phantasma.Explorer.Infrastructure.Interfaces;
using Phantasma.Explorer.Utils;
using Phantasma.Explorer.ViewModels;
using Phantasma.Numerics;

namespace Phantasma.Explorer.Controllers
{
    public class HomeController
    {
        private IRepository Repository { get; }

        public HomeController(IRepository repo)
        {
            Repository = repo;
        }

        public HomeViewModel GetLastestInfo()
        {
            var blocks = new List<BlockViewModel>();
            var txs = new List<TransactionViewModel>();
            foreach (var block in Repository.GetBlocks())
            {
                blocks.Add(BlockViewModel.FromBlock(Repository, block));
            }

            var chart = new Dictionary<string, uint>();

            foreach (var transaction in Repository.GetTransactions())
            {
                txs.Add(TransactionViewModel.FromTransaction(Repository, BlockViewModel.FromBlock(Repository, transaction.Block), transaction));
            }

            // tx history chart calculation
            var repTxs = Repository.GetTransactions(null, 1000);
            foreach (var transaction in repTxs)
            {
                DateTime chartTime = transaction.Block.Timestamp;
                var chartKey = $"{chartTime.Day}/{chartTime.Month}";

                if (chart.ContainsKey(chartKey))
                {
                    chart[chartKey] += 200;
                }
                else
                {
                    chart[chartKey] = 1;
                }
            }

            int totalChains = Repository.GetChainCount();
            uint height = Repository.GetChainByName("main").BlockHeight; //todo repo
            int totalTransactions = Repository.GetTotalTransactions();

            var vm = new HomeViewModel
            {
                Blocks = blocks,
                Transactions = txs,
                Chart = chart,
                TotalTransactions = totalTransactions,
                TotalChains = totalChains,
                BlockHeight = height,
            };
            return vm;
        }

        public List<CoinRateViewModel> GetRateInfo()
        {
            var symbols = new[] { "USD", "BTC", "ETH", "NEO" };

            var tasks = symbols.Select(symbol => CoinUtils.GetCoinInfoAsync(CoinUtils.SoulId, symbol));
            var rates = Task.WhenAll(tasks).GetAwaiter().GetResult();

            var coins = new List<CoinRateViewModel>();
            for (int i = 0; i < rates.Length; i++)
            {
                var coin = new CoinRateViewModel
                {
                    Symbol = symbols[i],
                    Rate = rates[i]["quotes"][symbols[i]].GetDecimal("price"),
                    ChangePercentage = rates[i]["quotes"][symbols[i]].GetDecimal("percent_change_24h")
                };
                coins.Add(coin);
            }

            return coins;
        }

        public string SearchCommand(string input)
        {
            try
            {
                if (Address.IsValidAddress(input)) //maybe is address
                {
                    return $"address/{input}";
                }

                //token
                var token = Repository.GetToken(input.ToUpperInvariant());
                if (token != null)// token
                {
                    return $"token/{token.Symbol}";
                }

                //app
                var apps = Repository.GetApps();
                var app = apps.SingleOrDefault(a => a.id == input);
                if (app.id == input)
                {
                    return $"app/{app.id}";
                }

                //chain
                var chain = Repository.GetChainByName(input) ?? Repository.GetChain(input);
                if (chain != null)
                {
                    return $"chain/{chain.Address.Text}";
                }

                //hash
                var hash = Hash.Parse(input);
                if (hash != null)
                {
                    var tx = Repository.GetTransaction(hash.ToString());
                    if (tx != null)
                    {
                        return $"tx/{tx.Hash}";
                    }

                    var block = Repository.GetBlock(hash.ToString());
                    if (block != null)
                    {
                        return $"block/{block.Hash}";
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "/home";
            }
        }
    }
}
