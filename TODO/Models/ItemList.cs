using System;
using System.Collections.Generic;

namespace TODO.Models
{
    internal class ItemList
    {
        private List<Item> items;
        public ItemList()
        {
            items = new List<Item>();
        }
        public void Add(Item item)
        {
            items.Add(item);
        }
        public void Update(Item item, string Title = default, string description = default, DateTime expectedEndDate = default(DateTime), Priority priority = default(Priority))
        {
            if (Title != default) item.Title = Title;
            if (description != default) item.Description = description;
            if (expectedEndDate != default) item.ExpectedEndDate = expectedEndDate;
            if (priority != default) item.Priority = priority;
        }

        public List<Item> GetAll()
        {
            return items;
        }

        public void Remove(Item item = default, int id = default)
        {
            if (item != default) items.Remove(item);
            else if (id != default) items.RemoveAt(id);
        }
    }
}
