﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FineTrack.Database;

namespace FineTrack.Services
{
    public class BalanceService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserSessionService _userSessionService; 

        // Exposed properties
        public decimal AvailableBalance { get; private set; } = 0;
        public decimal TotalInflows { get; private set; } = 0;
        public decimal TotalOutflows { get; private set; } = 0;
        public decimal TotalDebt { get; private set; } = 0;
        public decimal TotalPendingDebt { get; private set; } = 0;
        public decimal TotalClearedDebt { get; private set; } = 0;
        public List<string> IncomeCategories { get; private set; } = new List<string>();
        public List<string> ExpenseCategories { get; private set; } = new List<string>();
        public decimal TransactionCount { get; private set; } = 0;
        public decimal DebtTransactionCount { get; private set; } = 0;
        public decimal TotalTransactionCount { get; private set; } = 0;



        public BalanceService(ApplicationDbContext dbContext , UserSessionService userSessionService)
        {
            _dbContext = dbContext;
            _userSessionService = userSessionService;
        }

        public async Task UpdateAvailableBalanceAsync()
        {
            var currentUserId = _userSessionService.CurrentUser.UserId;

            var allTransactions = await _dbContext.Transactions.ToListAsync();
            var allDebts = await _dbContext.Debts.ToListAsync();

            //var transactions = alltransactionsToListAsync();
            var transactions = allTransactions.Where(t => t.UserId == currentUserId).ToList();

            var Debts = allDebts.Where(t => t.UserId == currentUserId).ToList();

            //var Debts = await _dbContext.Debts.ToListAsync();

            //var transactions = await _dbContext.Transactions.ToListAsync();
            //var Debts = await _dbContext.Debts.ToListAsync();

            // Calculate AvailableBalance using the formula
            var totalIncome = transactions
                .Where(t => t.TransactionType == "Income")
                .Sum(t => t.TransactionAmount);

            TotalInflows = totalIncome;
            TransactionCount = transactions.Count();

            var totalExpense = transactions
                .Where(t => t.TransactionType == "Expense")
                .Sum(t => t.TransactionAmount);

            TotalOutflows = totalExpense;

            var totalPendingDebt = Debts
                .Where(t => t.DebtStatus == "Pending")
                .Sum(t => t.DebtAmount);

            TotalPendingDebt = totalPendingDebt;
            DebtTransactionCount = Debts.Count();
            TotalTransactionCount = TransactionCount + DebtTransactionCount;

            var totalClearedDebt = Debts
                .Where(t => t.DebtStatus == "Cleared")
                .Sum(t => t.DebtAmount);

            TotalClearedDebt = totalClearedDebt;
            TotalDebt = TotalPendingDebt + TotalClearedDebt;
            AvailableBalance = totalIncome + totalPendingDebt - totalExpense;
        }

        // Method to update the categories
        public async Task UpdateTransactionCategoriesAsync()
        {
            var currentUserId = _userSessionService.CurrentUser.UserId;

            var allCategories = await _dbContext.TransactionCategories.ToListAsync();
            var categories = allCategories.Where(t => t.UserId == currentUserId).ToList();


            // Clear existing categories before adding new ones
            IncomeCategories.Clear();
            ExpenseCategories.Clear();

            // Separate categories based on their type
            foreach (var category in categories)
            {
                if (category.TypeOfTransaction == "Income")
                {
                    IncomeCategories.Add(category.TransactionCategory);
                }
                else if (category.TypeOfTransaction == "Expense")
                {
                    ExpenseCategories.Add(category.TransactionCategory);
                }
            }
        }
    }
}