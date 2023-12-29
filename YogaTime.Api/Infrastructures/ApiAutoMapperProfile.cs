using AutoMapper;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.YogaClass;
using YogaTime.Api.ModelsRequest.Instructor;
using YogaTime.Api.ModelsRequest.Group;
using YogaTime.Api.ModelsRequest.Person;
using YogaTime.Api.ModelsRequest.TimeTableItem;
using YogaTime.Api.ModelsRequest.TimeTableItemRequest;
using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;
using YogaTime.Api.ModelsRequest.Room;

namespace YogaTime.Api.Infrastructures
{
    /// <summary>
    /// Профиль маппера АПИшки
    /// </summary>
    public class ApiAutoMapperProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiAutoMapperProfile"/>
        /// </summary>
        public ApiAutoMapperProfile()
        {
            CreateMap<YogaClassModel, YogaClassResponse>(MemberList.Destination);
            CreateMap<YogaClassRequest, YogaClassModel>(MemberList.Destination);

            CreateMap<RoomModel, RoomResponse>(MemberList.Destination);
            CreateMap<RoomRequest, RoomModel>(MemberList.Destination);


            CreateMap<InstructorModel, InstructorResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Person != null
                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
                    : string.Empty))
                .ForMember(x => x.MobilePhone, opt => opt.MapFrom(x => x.Person != null
                    ? x.Person.Phone
                    : string.Empty));
            CreateMap<CreateInstructorRequest, InstructorRequestModel>(MemberList.Destination);
            CreateMap<InstructorRequest, InstructorRequestModel>(MemberList.Destination);

            CreateMap<PersonModel, PersonResponse>(MemberList.Destination);
            CreateMap<CreatePersonRequest, PersonRequestModel>(MemberList.Destination);
            CreateMap<PersonRequest, PersonRequestModel>(MemberList.Destination);

            CreateMap<GroupModel, GroupResponse>(MemberList.Destination);
            CreateMap<CreateGroupRequest, GroupRequestModel>(MemberList.Destination);
            CreateMap<GroupRequest, GroupRequestModel>(MemberList.Destination);


            CreateMap<TimeTableItemModel, TimeTableItemResponse>(MemberList.Destination)
                .ForMember(x => x.NameYogaClass, opt => opt.MapFrom(x => x.YogaClass!.Name))
                .ForMember(x => x.NameRoom, opt => opt.MapFrom(x => x.Room!.Name))
                .ForMember(x => x.NameGroup, opt => opt.MapFrom(x => x.Group!.Name))
                .ForMember(x => x.InstructorName, opt => opt.MapFrom(x => $"{x.Instructor!.LastName} {x.Instructor.FirstName} {x.Instructor.Patronymic}"))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Instructor!.Phone));
            CreateMap<CreateTimeTableItemRequest, TimeTableItemRequestModel>(MemberList.Destination);
            CreateMap<TimeTableItemRequest, TimeTableItemRequestModel>(MemberList.Destination);


        }
    }

}
