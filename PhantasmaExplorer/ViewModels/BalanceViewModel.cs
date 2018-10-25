﻿namespace Phantasma.Explorer.ViewModels
{
    public class BalanceViewModel
    {
        public string ChainName { get; set; }
        public decimal Balance { get; set; }

        public BalanceViewModel(string name, decimal balance)
        {
            ChainName = name;
            Balance = balance;
        }
    }
}