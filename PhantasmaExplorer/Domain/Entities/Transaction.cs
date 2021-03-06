﻿using System.Collections.Generic;
using Phantasma.Explorer.Domain.ValueObjects;

namespace Phantasma.Explorer.Domain.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            AccountTransactions = new List<AccountTransaction>();
            Events = new List<Event>();
        }

        public string Hash { get; set; }
        public string BlockHash { get; set; }
        public uint Timestamp { get; set; }
        public string Script { get; set; }
        public string Result { get; set; }

        public Block Block { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<AccountTransaction> AccountTransactions { get; set; }

        public Token Token { get; set; }
    }
}
