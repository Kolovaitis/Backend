using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using IAmIt.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public class CardRepository : ICardRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Card> _cards;
        private readonly IMongoCollection<UserCardMembership> _memberships;
        public CardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient();
            var db = client.GetDatabase(_configuration.NameDatabase);
            _cards = db.GetCollection<Card>("cards");
            _memberships = db.GetCollection<UserCardMembership>("userCardMemberships");
        }
        public async Task AddCardAsync(Card card)
        {
            await _cards.InsertOneAsync(card);
        }

        public async Task AddUserToCardAsync(ObjectId cardId, ObjectId userId)
        {
            var membership = new UserCardMembership
            {
                UserId = userId,
                CardId = cardId
            };
            await _memberships.InsertOneAsync(membership);
        }

        public async Task ChangeCardAsync(Card card)
        {
            var update = Builders<Card>.Update
                .Set(c => c.Name, card.Name)
                .Set(c => c.Description, card.Description);
            await _cards.UpdateOneAsync(c => c.Id == card.Id, update);
        }

        public async Task DeleteCardAsync(ObjectId id)
        {
            await _memberships.DeleteOneAsync(m => m.CardId == id);
            await _cards.DeleteOneAsync(c => c.Id == id);
        }

        public async Task DeleteUserFromCardAsync(ObjectId cardId, ObjectId userId)
        {
            await _memberships.DeleteOneAsync(m => m.CardId == cardId && m.UserId == userId);
        }

        public async Task<List<Card>> GetAllCardsByUserAsync(ObjectId userId)
        {
            var ids = (await _memberships.FindAsync(m => m.UserId == userId)).ToList().Select(m => m.CardId).ToList();
            return (await _cards.FindAsync(c => ids.Contains(c.Id))).ToList();
        }

        public async Task<List<ObjectId>> GetAllUsersInCardAsync(ObjectId cardId)
        {
            return (await _memberships.FindAsync(m => m.CardId == cardId)).ToList().Select(m => m.UserId).ToList();
        }

        public async Task<Card> GetCardAsync(ObjectId id)
        {
            return (await _cards.FindAsync(c => c.Id == id)).FirstOrDefault();
        }

        public async Task MoveCardAsync(ObjectId cardId, int newPosition)
        {
            var currentCard = (await _cards.FindAsync(c => c.Id == cardId)).FirstOrDefault();
            var currentPosition = currentCard.Position;
            var ids = new List<ObjectId>();
            if (currentPosition > newPosition)
            {
                ids = (await _cards.FindAsync(c => c.ColumnId == currentCard.ColumnId && (c.Position <= newPosition && c.Position > currentCard.Position))).ToList().Select(c => c.Id).ToList();
                var update = Builders<Card>.Update.Inc(c => c.Position, -1);
                await _cards.UpdateManyAsync(c => ids.Contains(c.Id), update);
            }
            else if (currentPosition < newPosition)
            {
                ids = (await _cards.FindAsync(c => c.ColumnId == currentCard.ColumnId && (c.Position >= newPosition && c.Position < currentCard.Position))).ToList().Select(c => c.Id).ToList();
                var update = Builders<Card>.Update.Inc(c => c.Position, 1);
                await _cards.UpdateManyAsync(c => ids.Contains(c.Id), update);
            }
            await _cards.UpdateOneAsync(c => c.Id == cardId, new BsonDocument("$set", new BsonDocument("Position", newPosition)));
        }

        public async Task MoveCardToOtherColumnAsync(ObjectId cardId, ObjectId columnId, int newPosition)
        {
            var columnUpdate = Builders<Card>.Update.Set(c => c.ColumnId, columnId);
            await _cards.UpdateOneAsync(c => c.Id == cardId, columnUpdate);
            var ids = (await _cards.FindAsync(c => c.ColumnId == columnId && (c.Position >= newPosition))).ToList().Select(c => c.Id).ToList();
            var update = Builders<Card>.Update.Inc(c => c.Position, 1);
            await _cards.UpdateManyAsync(c => ids.Contains(c.Id), update);
        }

        public async Task<List<Card>> GetAllCardsInColumnAsync(ObjectId columnId)
        {
            return (await _cards.FindAsync(c => c.ColumnId == columnId)).ToList();
        }
    }
}
