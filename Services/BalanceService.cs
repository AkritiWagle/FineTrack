using System;
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

        public decimal AvailableBalance { get; private set; } = 0;
        // Exposed categories as properties
        public List<string> IncomeCategories { get; private set; } = new List<string>();
        public List<string> ExpenseCategories { get; private set; } = new List<string>();


        public BalanceService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateAvailableBalanceAsync()
        {
            var transactions = await _dbContext.Transactions.ToListAsync();
            // Calculate AvailableBalance using the formula
            var totalIncome = transactions
                .Where(t => t.TransactionType == "Income")
                .Sum(t => t.TransactionAmount);

            var totalExpense = transactions
                .Where(t => t.TransactionType == "Expense")
                .Sum(t => t.TransactionAmount);

            var debt = 0; // Replace with actual debt calculation when available
            AvailableBalance = totalIncome + debt - totalExpense;
        }

        // Method to update the categories
        public async Task UpdateTransactionCategoriesAsync()
        {
            var categories = await _dbContext.TransactionCategories.ToListAsync();

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