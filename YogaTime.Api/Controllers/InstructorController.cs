using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.Instructor;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;

namespace YogaTime.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с инструкторами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService employeeService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InstructorController"/>
        /// </summary>
        public InstructorController(IInstructorService employeeService,
            IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех инструкторов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InstructorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<InstructorResponse>>(result));
        }

        /// <summary>
        /// Получает инструктора по id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(InstructorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Нет инструктора с id {id}");
            }

            return Ok(mapper.Map<InstructorResponse>(item));
        }

        /// <summary>
        /// Создаёт нового инструктора
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InstructorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateInstructorRequest request, CancellationToken cancellationToken)
        {
            var employeeRequestModel = mapper.Map<InstructorRequestModel>(request);
            var result = await employeeService.AddAsync(employeeRequestModel, cancellationToken);
            return Ok(mapper.Map<InstructorResponse>(result));
        }

        /// <summary>
        /// Редактирует инструктора
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(InstructorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(InstructorRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<InstructorRequestModel>(request);
            var result = await employeeService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<InstructorResponse>(result));
        }

        /// <summary>
        /// Удаляет инструктора по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
