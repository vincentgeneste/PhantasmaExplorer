﻿using System.Collections.Generic;
using System.Linq;
using Phantasma.Blockchain;
using Phantasma.Core.Utils;

namespace Phantasma.Explorer.ViewModels
{
    public class ChainViewModel
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public int Transactions { get; set; }
        public string ParentChain { get; set; }
        public int Height { get; set; }
        public List<BlockViewModel> Blocks { get; set; }

        public static ChainViewModel FromChain(Chain chain, List<BlockViewModel> lastBlocks)
        {
            return new ChainViewModel
            {
                Address = chain.Address.Text,
                Name = chain.Name.ToTitleCase(),
                Transactions = chain.TransactionCount,
                Height = chain.Blocks.Count(),
                Blocks = lastBlocks,
                ParentChain = chain.ParentChain?.Address.Text ?? ""
            };
        }
    }
}