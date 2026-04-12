using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Day : EntityBase<int>
{
         public string Name { get; private set; } = null!;
     private readonly List<ClientUserAvailableDay> _clientUserAvailableDays = new();
     public virtual IReadOnlyCollection<ClientUserAvailableDay> ClientUserAvailableDays => _clientUserAvailableDays;


     private Day() { }

     public static Result<Day> Create(CreateDayDTO dayDTO)
     {
         var newDay = new Day();
         dayDTO.Adapt(newDay);
         newDay.CreateDate = DateTime.Now;
         return Result<Day>.Success(newDay);
     }
     public static Day Create(string day)
     {
         var newDay = new Day();
         newDay.Name = day;
         newDay.CreateDate = DateTime.Now;
         return newDay;
     }

     public Result<Day> Update(UpdateDayDTO dayDTO)
     {
         dayDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Day>.Success(this);
     }


 }
