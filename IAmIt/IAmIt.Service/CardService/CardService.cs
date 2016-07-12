using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;
using IAmIt.DbEntity.DbEntity;

namespace IAmIt.Service.CardService
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<ObjectId> AddCardAsync(AddCardModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _cardRepository.GetCardAsync(id) != null)
            {
                return await AddCardAsync(model);
            }
            var card = new Card
            {
                Id = id,
                Name = model.Name,
                ColumnId = new ObjectId(model.ColumnId),
                Position = -1
            };
            await _cardRepository.AddCardAsync(card);
            return id;
        }

        public async Task AddUserToCardAsync(AddUserToCardModel model)
        {
            if((await _cardRepository.GetAllUsersInCardAsync(new ObjectId(model.CardId))).Contains(model.UserId))
            {
                throw new Exception("This user is already a member of this card");
            }
            await _cardRepository.AddUserToCardAsync(new ObjectId(model.CardId), model.UserId);
        }

        public async Task ChangeCardAsync(ChangeCardModel model)
        {
            var card = (await _cardRepository.GetCardAsync(new ObjectId(model.CardId)));
            card.Name = model.Name;
            await _cardRepository.ChangeCardAsync(card);
        }

        public async Task DeleteCardAsync(DeleteCardModel model)
        {
            await _cardRepository.DeleteCardAsync(new ObjectId(model.CardId));
        }

        public async Task DeleteUserFromCardAsync(DeleteUserFromCardModel model)
        {
            await _cardRepository.DeleteUserFromCardAsync(new ObjectId(model.CardId), model.UserId);
        }

        public async Task DeleteYourselfFromCardAsync(DeleteYourselfFromCardModel model)
        {
            if ((await _cardRepository.GetAllUsersInCardAsync(new ObjectId(model.CardId))).AsQueryable().Count() == 1)
            {
                throw new Exception
                    ("You are the last member of the card. If you really want to delete yourself, delete the card");
            }
            await _cardRepository.DeleteUserFromCardAsync(new ObjectId(model.CardId), model.UserId);
        }

        public async Task<CardToSendFullModel> GetCardAsync(GetCardModel model)
        {
            var card = (await _cardRepository.GetCardAsync(new ObjectId(model.CardId)));
            return new CardToSendFullModel
            {
                ColumnId = card.ColumnId.ToString(),
                Name = card.Name,
                CardId = model.CardId,
                Position = card.Position
            };
        }

        public async Task<ICollection<CardToSendLightModel>> GetMyCardsAsync(ObjectId userId)
        {
            return (await _cardRepository.GetAllCardsByUserAsync(userId))
                .Select(g => new CardToSendLightModel { ColumnId = g.ColumnId.ToString(), Name = g.Name, Position = g.Position, CardId = g.Id.ToString() }).ToList();
        }

        public async Task<ICollection<ObjectId>> GetUsersInCardAsync(GetUsersInCardModel model)
        {
            return (await _cardRepository.GetAllUsersInCardAsync(new ObjectId(model.CardId)));
        }

        public async Task MoveCardAsync(MoveCardModel model)
        {
            await _cardRepository.MoveCardAsync(new ObjectId(model.CardId), model.NewPosition);
        }

        public async Task MoveCardInOtherColumnAsync(MoveCardInOtherColumnModel model)
        {
            await _cardRepository.MoveCardToOtherColumnAsync
                (new ObjectId(model.CardId), new ObjectId(model.NewColumnId), model.NewPosition);
        }
    }
}
