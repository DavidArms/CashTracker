using CashTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashTracker.Services
{
    public class MockDataStore : IDataStore<Job>
    {
        readonly List<Job> items;

        public MockDataStore()
        {
            items = new List<Job>()
            {
                //new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Job item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Job item)
        {
            var oldItem = items.Where((Job arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Job arg) => arg.ID == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Job> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Job>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}