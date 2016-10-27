using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Zadatak3;
using TodoRepository;

namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {

        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >

        private readonly IGenericList<TodoItem> _inMemoryToDoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryToDoDatabase = initialDbState;
            }
            else
            {
                _inMemoryToDoDatabase = new GenericList<TodoItem>();
            }
            //_inMemoryToDoDatabase = initialDbState ?? new GenericList<TodoItem>();
        }


        public TodoItem Get(Guid todoId)
        {
            IEnumerable<TodoItem> current = _inMemoryToDoDatabase.Where(e => e.Id == todoId);

            return current.FirstOrDefault();
        }

        public void Add(TodoItem todoItem)
        {
            int count = _inMemoryToDoDatabase.Where(e => e.Id == todoItem.Id)
                                             .Count();
            if (count > 0)
            {
                throw new DuplicateTodoItemException("duplicate id: {" + todoItem.Id + "}");  
            }
            else
            {
                if (todoItem == null) _inMemoryToDoDatabase.Add(todoItem);
                TodoItem newItem = new TodoItem(todoItem.Text);
                newItem.Id = todoItem.Id;
                newItem.IsCompleted = todoItem.IsCompleted;
                newItem.DateCompleted = todoItem.DateCompleted;
                newItem.DateCreated = todoItem.DateCreated;

                _inMemoryToDoDatabase.Add(newItem);
            }
        }

        public bool Remove(Guid todoId)
        {
            TodoItem current;
            current = _inMemoryToDoDatabase.Where(e => e.Id == todoId).FirstOrDefault();
            if (current != null)
            {
                if (_inMemoryToDoDatabase.Remove(current))
                {
                    return true;
                }
                
            }

            return false;
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem current = _inMemoryToDoDatabase.Where(e => e.Id == todoItem.Id).FirstOrDefault();
            if (current != null)
            {
                //current.Text = todoItem.Text;
                //current.IsCompleted = todoItem.IsCompleted;
                //current.DateCompleted = todoItem.DateCompleted;
                //current.DateCreated = todoItem.DateCreated;
                _inMemoryToDoDatabase.Where(e => e.Id == todoItem.Id)
                                     .ToList()
                                     .ForEach(e =>
                                     {
                                         e.Text = todoItem.Text;
                                         e.IsCompleted = todoItem.IsCompleted;
                                         e.DateCompleted = todoItem.DateCompleted;
                                         e.DateCreated = todoItem.DateCreated;
                                         if (e.IsCompleted == true) e.MarkAsCompleted();
                                         
                                     });
            }
            else
            {
                if (todoItem == null) _inMemoryToDoDatabase.Add(todoItem);
                TodoItem newItem = new TodoItem(todoItem.Text);
                newItem.Id = todoItem.Id;
                _inMemoryToDoDatabase.Add(newItem);
            }

        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem current;
            current = _inMemoryToDoDatabase.Where(e => e.Id == todoId).FirstOrDefault();
            if (current == null) return false;

            _inMemoryToDoDatabase.Where(e => e.Id == todoId)
                                 .ToList()
                                 .ForEach(e => e.MarkAsCompleted());

            return true;
        }

        public List<TodoItem> GetAll()
        {

            return _inMemoryToDoDatabase.Where(e => true)
                                        .OrderByDescending(e => e.DateCreated)
                                        .ToList();

        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryToDoDatabase.Where(e => e.IsCompleted == false)
                                        .ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryToDoDatabase.Where(e => e.IsCompleted == true)
                                        .ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryToDoDatabase.Where(e => filterFunction(e) == true)
                                        .ToList();
        }

     

        

      
    }
}
