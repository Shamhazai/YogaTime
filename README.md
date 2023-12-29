# Yoga classes API

API для составления расписания студии йоги.



## Схема базы данных
```mermaid

erDiagram

    Persons {
        Guid Id
        string LastName
        string FirstName
        string Patronymic
        string Email
        string Phone
        Guid GroupId
    }

    YogaClasses {
        Guid Id
        string Name
        string Description
    }

    Rooms {
        Guid Id
        string Name
        string Description
    }
    

    Instructors {
        Guid Id
        Enum Description
        int PersonId
    }

    Groups {
        Guid Id
        string Name
        string Description
        Guid InstructorId
    }

     TimeTableItem {
        Guid Id
        DateTimeOffset StartDate
        DateTimeOffset EndDate
        Guid YogaClassId
        Guid GroupId
        int RoomNumber
        Guid TeacherId
    }
    Persons ||--o{ Instructors: is
    Groups ||--o{ Persons: is
    Instructors ||--o{ Groups: is
    YogaClasses ||--o{ TimeTableItem: is
    Rooms ||--o{ TimeTableItem: is
    Groups ||--o{ TimeTableItem: is
    Instructors ||--o{ TimeTableItem: is

  BaseAuditEntity {
        Guid ID
        DateTimeOffset CreatedAt
        string CreatedBy
        DateTimeOffset UpdatedAt
        string UpdatedBy
        DateTimeOffset DeleteddAt
  }

'''

Скрипт для заполнения БД:

``` sql
GO
INSERT [dbo].[Persons] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Phone], [GroupId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'7edf7d5b-5db7-4683-9943-4f955ea27cd3', N'Пшеничникова', N'Мария', N'Владимировнв', N'm79522006332@yandex.ru', N'+79117562375', N'a614609e-d775-4b8b-bd85-8abc1986e267', CAST(N'2023-12-29T17:11:31.5959451+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T17:44:38.5143515+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
INSERT [dbo].[Persons] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Phone], [GroupId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'98f29a4b-07d1-4c30-961e-5b7c1f721df7', N'Ахметьянов', N'Азат', N'Ришатович', N'annaka4356@gmail.com', N'+79522006333', NULL, CAST(N'2023-12-29T16:58:11.4781545+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T16:59:23.8946031+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
INSERT [dbo].[Persons] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Phone], [GroupId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'd3dc2f4a-dc60-4fbc-b0ad-e67a4b1ed0aa', N'Лизунова', N'Ольга', N'Васильевна', N'ufiusebguire@GMAIL.COM', N'+79115647387', N'a614609e-d775-4b8b-bd85-8abc1986e267', CAST(N'2023-12-29T17:45:56.7448256+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T17:47:48.7883267+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
GO
INSERT [dbo].[Instructors] ([Id], [Desc], [PersonId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'6087c50a-7df7-4ea6-a8e3-0c872ff4c08d', N'Лучший инструктор', N'98f29a4b-07d1-4c30-961e-5b7c1f721df7', CAST(N'2023-12-29T17:04:33.3382315+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T17:04:33.3382635+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
GO
INSERT [dbo].[Groups] ([Id], [InstructorId], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'a614609e-d775-4b8b-bd85-8abc1986e267', N'6087c50a-7df7-4ea6-a8e3-0c872ff4c08d', N'Зубарята', N'Самые крутышки', CAST(N'2023-12-29T17:08:38.4535522+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T17:15:00.5660164+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
GO
INSERT [dbo].[Room] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'a04d8d94-9c6e-49b3-8059-903586a0da59', N'Зал Сатурна', N'Самый большой', CAST(N'2023-12-29T17:06:04.5870576+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T17:06:04.5870683+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
GO
INSERT [dbo].[YogaClass] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'f3439352-c51e-4b30-8b2b-ea9865e1ec60', N'Аэройога', N'Лети лети лети', CAST(N'2023-12-29T16:53:27.3498960+00:00' AS DateTimeOffset), N'YogaTime.Api', CAST(N'2023-12-29T16:54:51.5454419+00:00' AS DateTimeOffset), N'YogaTime.Api', NULL)
GO
```
