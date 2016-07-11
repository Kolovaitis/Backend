using IAmIt.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Service.CardService
{
    public interface ICardService
    {
        Task<ObjectId> AddCardAsync(AddCardModel model);
        Task DeleteCardAsync(DeleteCardModel model);
        Task ChangeCardAsync(ChangeCardModel model);
        Task AddUserToCardAsync(AddUserToCardModel model);
        Task DeleteUserFromCardAsync(DeleteUserFromCardModel model);
        Task DeleteYourselfFromCardAsync(DeleteYourselfFromCardModel model);
        Task<ICollection<CardToSendLightModel>> GetMyCardsAsync(ObjectId userId);
        Task<CardToSendFullModel> GetCardAsync(GetCardModel model);
        Task<ICollection<ObjectId>> GetUsersInCardAsync(GetUsersInCardModel model);
        Task MoveCardInOtherColumnAsync(MoveCardInOtherColumnModel model);
        Task MoveCardAsync(MoveCardModel model);
    }
}
