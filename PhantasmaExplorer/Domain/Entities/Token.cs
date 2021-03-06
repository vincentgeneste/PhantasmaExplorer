﻿using System.Collections.Generic;
using Phantasma.Explorer.Domain.ValueObjects;
using Phantasma.RpcClient.DTOs;

namespace Phantasma.Explorer.Domain.Entities
{
    public class Token
    {
        public Token()
        {
            Flags = TokenFlags.None;
            MetadataList = new List<TokenMetadata>();
            Transactions = new HashSet<Transaction>();
        }

        public string Symbol { get; set; }
        public string Name { get; set; }
        public uint Decimals { get; set; }
        public string CurrentSupply { get; set; }
        public string MaxSupply { get; set; }
        public string OwnerAddress { get; set; }

        public List<TokenMetadata> MetadataList { get; set; }
        public TokenFlags Flags { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
