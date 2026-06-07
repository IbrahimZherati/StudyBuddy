using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.ReviewDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Review : EntityBase<int>
{
    public int ClientUserId { get; private set; }

    public string? Title { get; private set; }

    public string? Description { get; private set; }

    public float Rating { get; private set; }

    public ClientUser ClientUser { get; private set; } = null!;


    private Review() { }

    public static Result<Review> Create(CreateReviewDTO reviewDTO)
    {
        var newReview = new Review();
        reviewDTO.Adapt(newReview);
        newReview.CreateDate = DateTime.Now;
        return Result<Review>.Success(newReview);
    }

    public Result<Review> Update(UpdateReviewDTO reviewDTO)
    {
        reviewDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<Review>.Success(this);
    }


}
