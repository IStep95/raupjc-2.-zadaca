using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TodoRepository;

namespace Repositories.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {

            ITodoRepository repository = new TodoRepository();
            repository.Add(null);

        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);

            Assert.AreEqual(1, repository.GetAll().Count());
            Assert.IsTrue(repository.Get(todoItem.Id) != null);

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GettingItemWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            Assert.AreEqual(null, repository.Get(new Guid()));
        }

        [TestMethod] 
        public void GettingItemWillReturnCorrectItem()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("first");
            TodoItem item2 = new TodoItem("second");
            TodoItem item3 = new TodoItem("third");
            
            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
         
            Assert.AreEqual(3, repository.GetAll().Count());
            Assert.AreEqual(item1, repository.Get(item1.Id));
        }

        [TestMethod]
        public void GettingNonExistingIdWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("first");
            TodoItem item2 = new TodoItem("second");
            repository.Add(item1);

            Assert.AreEqual(null, repository.Get(item2.Id));
        }

        [TestMethod]
        public void RemovingItemWillRemoveFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);

            Assert.AreEqual(1, repository.GetAll().Count());
            Assert.AreEqual(true, repository.Remove(todoItem.Id));
            Assert.AreEqual(0, repository.GetAll().Count());

        }

        [TestMethod]
        public void RemovingItemWillNotRemoveFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem = new TodoItem("Groceries");
            TodoItem todoItem2 = new TodoItem("Milk");
            repository.Add(todoItem);
            Assert.AreEqual(false, repository.Remove(todoItem2.Id));
            
        }

        [TestMethod]
        public void UpdatingGivenItemInDatabaseWillUpdate()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("first");
            TodoItem todoItem2 = new TodoItem("second");

            repository.Add(todoItem1);
            repository.Add(todoItem2);

            todoItem1.Text = "first, but changed";
            todoItem1.IsCompleted = true;
    
            repository.Update(todoItem1);
            Assert.IsTrue(todoItem1.Text == repository.Get(todoItem1.Id).Text
                && todoItem1.IsCompleted == repository.Get(todoItem1.Id).IsCompleted);
        }

        [TestMethod]
        public void UpdatingGivenItemInDatabaseWillNotUpdate()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("first");
            TodoItem todoItem2 = new TodoItem("second");

            repository.Add(todoItem1);
            repository.Add(todoItem2);

            todoItem1.Text = "first, but changed";
            todoItem1.IsCompleted = true;
 
            Assert.IsFalse(todoItem1.Text == repository.Get(todoItem1.Id).Text
                && todoItem1.IsCompleted == repository.Get(todoItem1.Id).IsCompleted);
        }

        [TestMethod]
        public void UpdatingNonExistingItemWillMakeNewOne()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem = new TodoItem("Milk");
            repository.Update(todoItem);

            Assert.AreEqual(1, repository.GetAll().Count());
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        public void MarkItemAsCompleteSuccess()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);
            Assert.AreEqual(false, repository.Get(todoItem.Id).IsCompleted);
            todoItem.MarkAsCompleted();
            Assert.AreEqual(false, repository.Get(todoItem.Id).IsCompleted);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(true, repository.Get(todoItem.Id).IsCompleted);

        }

        [TestMethod]
        public void GetsAllItemsFromDatabaseOrderByDescending()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("first");
            Thread.Sleep(1000);
            TodoItem todoItem2 = new TodoItem("second");
            Thread.Sleep(1000);
            TodoItem todoItem3 = new TodoItem("third");

            Console.WriteLine(todoItem1.DateCreated);
            Console.WriteLine(todoItem2.DateCreated);
            Console.WriteLine(todoItem3.DateCreated);

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);

            List<TodoItem> list = repository.GetAll();
            foreach(TodoItem item in list)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void GetsAllIncompleteItemsInDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("first");
            TodoItem todoItem2 = new TodoItem("second");
            TodoItem todoItem3 = new TodoItem("third");
            todoItem2.MarkAsCompleted();

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
     
            Assert.AreEqual(2, repository.GetActive().Count());
        }

        [TestMethod]
        public void GetsAllCompletedItemsInDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("first");
            TodoItem todoItem2 = new TodoItem("second");
            TodoItem todoItem3 = new TodoItem("third");
            todoItem2.MarkAsCompleted();

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);

            Assert.AreEqual(1, repository.GetCompleted().Count());
        }

        [TestMethod]
        public void GetsItemsThatApplyFilter()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("small house");
            TodoItem todoItem2 = new TodoItem("big house");
            TodoItem todoItem3 = new TodoItem("big house");
          
            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);

            Assert.AreEqual(2, repository.GetFiltered(testFilterMethod).Count());
        }

        [TestMethod]
        public void GetsItemsThatNotApplyFilter()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem1 = new TodoItem("Groceries");
            TodoItem todoItem2 = new TodoItem("Market");
            TodoItem todoItem3 = new TodoItem("Shop");
            TodoItem todoItem4 = new TodoItem("Milk");

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);

            Assert.AreEqual(0, repository.GetFiltered(testFilterMethod).Count());
        }


        private bool testFilterMethod(TodoItem item)
        {
            if (item.Text.Equals("big house")) return true;
            return false;
        }

    }
}

