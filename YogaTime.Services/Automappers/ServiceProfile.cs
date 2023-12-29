using AutoMapper;
using Serilog;
using YogaTime.Context.Contracts.Models;
using YogaTime.Services.Contracts.Models;
namespace YogaTime.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<YogaClass, YogaClassModel>(MemberList.Destination);

            CreateMap<Room, RoomModel>(MemberList.Destination);


            CreateMap<Person, PersonModel>(MemberList.Destination);

            CreateMap<Instructor, InstructorModel>(MemberList.Destination)
                .ForMember(x => x.Person, next => next.Ignore());

            CreateMap<Group, GroupModel>(MemberList.Destination)
                .ForMember(x => x.Clients, next => next.Ignore())
                .ForMember(x => x.ClassroomInstructor, next => next.Ignore());

            CreateMap<TimeTableItem, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Group, next => next.Ignore())
                .ForMember(x => x.YogaClass, next => next.Ignore())
                .ForMember(x => x.Room, next => next.Ignore())
                .ForMember(x => x.Instructor, next => next.Ignore());

            Log.Information("Инициализирован Mapper в классе ServiceProfile");
        }
    }
}
