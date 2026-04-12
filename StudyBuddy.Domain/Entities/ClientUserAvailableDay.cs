using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.ClientUserAvailableDayDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ClientUserAvailableDay : EntityBase<int>
{
    public int ClientUserId { get; private set; }

    public int DayId { get; private set; }

    public virtual Day Day { get; private set; } = null!;

    public virtual ClientUser ClientUser { get; private set; } = null!;


    private ClientUserAvailableDay() { }

    public static Result<ClientUserAvailableDay> Create(CreateClientUserAvailableDayDTO clientUserAvailableDayDTO)
    {
        var newClientUserAvailableDay = new ClientUserAvailableDay();
        clientUserAvailableDayDTO.Adapt(newClientUserAvailableDay);
        newClientUserAvailableDay.CreateDate = DateTime.Now;
        return Result<ClientUserAvailableDay>.Success(newClientUserAvailableDay);
    }
    public static ClientUserAvailableDay Create(int clientUserId, int dayId)
    {
        var newClientUserAvailableDay = new ClientUserAvailableDay();
        newClientUserAvailableDay.ClientUserId = clientUserId;
        newClientUserAvailableDay.DayId = dayId;
        newClientUserAvailableDay.CreateDate = DateTime.Now;
        return newClientUserAvailableDay;
    }

    public Result<ClientUserAvailableDay> Update(UpdateClientUserAvailableDayDTO clientUserAvailableDayDTO)
    {
        clientUserAvailableDayDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<ClientUserAvailableDay>.Success(this);
    }


}
