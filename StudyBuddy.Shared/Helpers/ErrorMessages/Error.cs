using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.Helpers.ErrorMessages
{
    public class Error
    {
        public const string MessageMustNotNull = "the message must`t null";
        public const string DeleteFailed = "Delete Failed";
        public const string CreateFailed = "Create Failed";
        public const string UpdateFailed = "Update Failed";
        public const string RequestFailed = "Request Failed";
        public const string GroupChatNameAlreadyExists = "group chat name alreadyExists";
        public const string UserAlreadyInGroupChat = "user already in group chat";
        public const string AddFailed = "Add Failed";
        public const string RemoveFailed = "Remove Failed";
        public const string UserNotInThisGroup = "User Not In This Group";
        public const string AiServiceFailed = "Ai Service Failed";
        public const string CreateUserFailed = "Create User Failed";
        public const string MajorNotFound = "Major Not Found";
        public const string UniversityNotFound = "University Not Found";
        public const string CityNotFound = "City Not Found";
        public const string CountryNotFound = "Country Not Found";
        public const string GenerateSkillFailed = "Generate Skill Failed";
        public const string GroupChatNotFound = "Group Chat Not Found";
        public const string MajorAlreadyExists = "Major Already Exists";
        public const string UniversityAlreadyExists = "University Already Exists";
        public const string CityAlreadyExists = "City Already Exists";
        public const string CountryAlreadyExists = "Country Already Exists";
        public const string DayAlreadyExists = "Day Already Exists";
        public const string ArticleAlreadyExists = "Article Already Exists";
        public const string ClientUserNotFound = "ClientUser Not Found";
        public const string ArticleTypeNotFound = "ArticleType Not Found";
        public const string NotificationTypeNotFound = "Notification Type Not Found";
        public const string YouCanNotSendFromDeferentId = "You Can Not Send From Deferent Id";
        public const string ArticleTypeAlreadyExists = "ArticleType Already Exists";
        public const string ClientFileAlreadyExists = "ClientFile Already Exists";
        public const string ClientUserAlreadyExists = "ClientUser Already Exists";
        public const string ClientUserAvailableDayAlreadyExists = "ClientUserAvailableDay Already Exists";
        public const string DayNotFound = "Day Not Found";
        public const string ClientUserGroupChatAlreadyExists = "ClientUserGroupChat Already Exists";
        public const string ClientUserSkillAlreadyExists = "ClientUserSkill Already Exists";
        public const string SkillNotFound = "Skill Not Found";
        public const string EventAlreadyExists = "Event Already Exists";
        public const string FeedAlreadyExists = "Feed Already Exists";
        public const string FeedReplyAlreadyExists = "FeedReply Already Exists";
        public const string FeedNotFound = "Feed Not Found";
        public const string FriendShipAlreadyExists = "FriendShip Already Exists";
        public const string FriendNotFound = "Friend Not Found";
        public const string FriendRequestAlreadyExists = "FriendRequest Already Exists";
        public const string GroupChatAlreadyExists = "GroupChat Already Exists";
        public const string GroupMessageAlreadyExists = "GroupMessage Already Exists";
        public const string FromClientUserNotFound = "FromClientUser Not Found";
        public const string MessageAlreadyExists = "Message Already Exists";
        public const string ToClientUserNotFound = "ToClientUser Not Found";
        public const string NoteAlreadyExists = "Note Already Exists";
        public const string NotificationAlreadyExists = "Notification Already Exists";
        public const string NotificationTypeAlreadyExists = "NotificationType Already Exists";
        public const string PostAlreadyExists = "Post Already Exists";
        public const string SkillAlreadyExists = "Skill Already Exists";
        public const string FirstClientUserNotFound = "First Client User Not Found";
        public const string SecondClientUserNotFound = "Second Client User Not Found";
        public const string ArticleNotFound = "Article Not Found";

        public const string ClientFileNotFound = "ClientFile Not Found";
        public const string ClientUserAvailableDayNotFound = "ClientUserAvailableDay Not Found";
        public const string ClientUserGroupChatNotFound = "ClientUserGroupChat Not Found";
        public const string ClientUserSkillNotFound = "ClientUserSkill Not Found";
        public const string EventNotFound = "Event Not Found";
        public const string FeedReplyNotFound = "FeedReply Not Found";
        public const string FriendRequestNotFound = "FriendRequest Not Found";
        public const string GroupMessageNotFound = "GroupMessage Not Found";
        public const string MessageNotFound = "Message Not Found";
        public const string NoteNotFound = "Note Not Found";
        public const string NotificationNotFound = "Notification Not Found";
        public const string PostNotFound = "Post Not Found";
        public const string UserIdNotFound = "UserId Not Found";
        public const string ClientUserAlreadyLikeThisFeed = "ClientUser Already Like This Feed";
        public const string ClientUserNotLikeThisFeedYet = "ClientUser Not Like This Feed Yet";
        public const string LikeFailed = "Like Failed";
        public const string ClientUserAlreadyRequestFriend = "ClientUser Already Request Friend";
        public const string ClientUserIdCanNotSameRequestClientUserId = "ClientUser Id Can Not Same Request ClientUser Id";
        public const string ClientUserCanNotAcceptRequestHeMake = "ClientUser Can Not Accept Request He Make";
        public const string TheFriendRequestNotForThisClient = "The FriendRequest Not For This Client";
        public const string AccessDeniedNotOwner = "Access Denied Not Owner";
        public const string ClientUserNotInThisGroup = "ClientUser Not In This Group";
        public const string ClientUserNotFriendWithThisClient = "ClientUser Not Friend With This Client";
        public const string ClientUserCannotMakeRequestToHimSelf = "ClientUser Can not MakeRequest To HimSelf";
        public const string ClientUserAlreadyLikeThisPost = "ClientUser Already Like This Post";
        public const string ClientUserNotLikedThisPost = "ClientUser Not Liked This Post";
        public const string ClientUserNotLikeThisPostYet = "ClientUser Not Like This Post Yet";

        public const string PostReplyNotFound = "PostReply Not Found";
        public const string PostReplyAlreadyExists = "PostReply Already Exists";
        public const string JsonNotFound = "JsonNotFound";
        public const string JsonFormatWrong = "JsonFormatWrong";
        public const string TagsNotFound = "TagsNotFound";
        public const string StudyInterestNotFound = "StudyInterest Not Found";
        public const string StudyInterestAlreadyExists = "StudyInterest Already Exists";
        public static string UserNotFound = "User Not Found";
        public static string InvalidOrExpiredToken = "Invalid or expired token";
        public static string TokenIsEmpty = "Token Is Empty";
        public static string ClientUserAlreadyLikeThisReply = "ClientUser Already Like This Reply";
        public static string ClientUserAlreadyUnLikeThisReply = "ClientUser Already UnLike This Reply";
        public static string TheFileIsEmpty = "The File Is Empty";
        public const string ReviewNotFound = "Review Not Found";
        public const string ReviewAlreadyExists = "Review Already Exists";
        public const string YouNotInvitedToThisGroup = "You Not Invited To This Group";
        public const string ClientAlReadyInThisGroup = "Client AlReady In This Group";
        public const string ClientUserAlreadyInvited = "Client Already Invited";
        public const string InviteNotFound = "Invite Not Found";
        public const string ThisMessageNotForYou = "This Message Not For You";
        public const string YouCannotReadYourOwnMessage = "You Cannot Read Your Own Message";
        public const string TopicNotFound = "Topic Not Found";
        public const string TopicAlreadyExists = "Topic Already Exists";
        public const string UserAlReadyRegistered = "User Already Registered";



    }
}