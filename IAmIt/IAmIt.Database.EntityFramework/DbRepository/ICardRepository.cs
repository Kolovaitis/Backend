using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public interface ICardRepository
    {
        Task AddCardAsync(Card card);
        Task<Card> GetCardAsync(ObjectId id);
        Task ChangeCardAsync(Card card);
        Task DeleteCardAsync(ObjectId id);

        Task AddUserToCardAsync(ObjectId cardId, ObjectId userId);
        Task DeleteUserFromCardAsync(ObjectId cardId, ObjectId userId);
        Task<List<Card>> GetAllCardsByUserAsync(ObjectId userId);
        Task<List<ObjectId>> GetAllUsersInCardAsync(ObjectId cardId);
        Task<List<Card>> GetAllCardsInColumnAsync(ObjectId columnId);

        Task MoveCardToOtherColumnAsync(ObjectId cardId, ObjectId columnId, int newPosition);
        Task MoveCardAsync(ObjectId cardId, int newPosition);
    }
}
