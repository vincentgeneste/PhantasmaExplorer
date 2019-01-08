﻿using System.Collections.Generic;
using System.Linq;
using Phantasma.Core.Utils;
using Phantasma.RpcClient.DTOs;

namespace Phantasma.Explorer.ViewModels
{
    public class ChainViewModel
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public int Transactions { get; set; }
        public string ParentChain { get; set; }
        public uint Height { get; set; }
        public List<BlockViewModel> Blocks { get; set; }
        public Dictionary<string, string> ChildChains { get; set; }


        public static ChainViewModel FromChain(List<ChainDto> chains, ChainDto chain, List<BlockViewModel> lastBlocks, int totalTxs)
        {
            var vm = new ChainViewModel
            {
                Address = chain.Address,
                Name = chain.Name.ToTitleCase(),
                Transactions = totalTxs,
                Height = chain.Height,
                Blocks = lastBlocks,
                ParentChain = chain.ParentAddress ?? ""
            };

            if (chains != null && chains.Any())
            {
                vm.ChildChains = new Dictionary<string, string>();
                foreach (var repoChain in chains)
                {
                    if (repoChain.ParentAddress.Equals(chain.Address))
                    {
                        vm.ChildChains[repoChain.Name] = repoChain.Address;
                    }
                }
            }
            return vm;
        }
    }
}