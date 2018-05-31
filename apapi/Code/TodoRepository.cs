using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apapi.Abstract;
using apapi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace apapi.Code
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DbContext _context = null;

        public TodoRepository(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            try
            {
                return await _context.Todos
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId(BSonId value)


        public async Task<Todo> GetTodo(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Todos
                                .Find(Todo =>  Todo.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddTodo(Todo item)
        {
            try
            {
                await _context.Todos.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveTodo(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Todos.DeleteOneAsync(
                        Builders<Todo>.Filter.Eq("_id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateTodo(Todo todo)
        { 
            
            ReplaceOneResult updateResult =
            await _context
            .Todos
            .ReplaceOneAsync(
            filter: g => g.InternalId == todo.InternalId,
            replacement: todo);
            return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
            
        }

        public async Task<bool> UpdateTodo(string id, Todo item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Todos
                                    .ReplaceOneAsync(n => n.InternalId.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllTodos()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Todos.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
