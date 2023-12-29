﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.TimeTableItem;
using YogaTime.Api.ModelsRequest.TimeTableItemRequest;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;

namespace YogaTime.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с элементами расписания
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "TimeTableItem")]
    public class TimeTableItemController : Controller
    {
        private readonly ITimeTableItemService timeTableItemService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemController"/>
        /// </summary>
        public TimeTableItemController(ITimeTableItemService timeTableItemService,
            IMapper mapper)
        {
            this.timeTableItemService = timeTableItemService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех занятий на указанный день
        /// </summary>
        [HttpGet("{targetDate:datetime}")]
        [ProducesResponseType(typeof(IEnumerable<TimeTableItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDate(DateTime targetDate, CancellationToken cancellationToken)
        {
            var items = await timeTableItemService.GetAllAsync(targetDate, cancellationToken);
            return Ok(mapper.Map<IEnumerable<TimeTableItemResponse>>(items));
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти элемент расписания с идентификатором {id}");
            }

            return Ok(mapper.Map<TimeTableItemResponse>(item));
        }

        /// <summary>
        /// Создаёт новое расписание
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTimeTableItemRequest request, CancellationToken cancellationToken)
        {
            var timeTableRequestModel = mapper.Map<TimeTableItemRequestModel>(request);
            var result = await timeTableItemService.AddAsync(timeTableRequestModel, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся расписание
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(TimeTableItemRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TimeTableItemRequestModel>(request);
            var result = await timeTableItemService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся расписание по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await timeTableItemService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
