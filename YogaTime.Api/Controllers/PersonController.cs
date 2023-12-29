﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.Person;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;

namespace YogaTime.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Участниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonController"/>
        /// </summary>
        public PersonController(IPersonService personService,
            IMapper mapper)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех участников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await personService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PersonResponse>>(result));
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await personService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти участника с идентификатором {id}");
            }

            return Ok(mapper.Map<PersonResponse>(item));
        }

        /// <summary>
        /// Создаёт нового участника
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var personRequestModel = mapper.Map<PersonRequestModel>(request);
            var result = await personService.AddAsync(personRequestModel, cancellationToken);
            return Ok(mapper.Map<PersonModel>(result));
        }

        /// <summary>
        /// Редактирует имеющегося участника
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(PersonRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PersonRequestModel>(request);
            var result = await personService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Добавляет участника в группу по id
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditGroup(Guid id, [Required] Guid groupId, CancellationToken cancellationToken)
        {
            var result = await personService.UpdateGroupAsync(id, groupId, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющегося участника по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await personService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
