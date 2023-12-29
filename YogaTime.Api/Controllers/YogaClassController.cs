using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.YogaClass;
using YogaTime.Context.Contracts.Models;
using YogaTime.Services.Contracts.Exceptions;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;

namespace YogaTime.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Занятиями Йогой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "YogaClass")]
    public class YogaClassController : ControllerBase
    {
        private readonly IYogaClassService yogaClassService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="YogaClassController"/>
        /// </summary>
        public YogaClassController(IYogaClassService YogaClassService,
            IMapper mapper)
        {
            this.yogaClassService = YogaClassService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех занятий
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<YogaClassResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await yogaClassService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<YogaClassResponse>>(result));
        }

        /// <summary>
        /// Получает занятие по id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(YogaClassResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await yogaClassService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<YogaClassResponse>(result));
        }

        /// <summary>
        /// Создаёт новое занятие
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(YogaClassResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(TimeTableEntityNotFoundException<YogaClass>), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(TimeTableInvalidOperationException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateYogaClassRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await yogaClassService.AddAsync(request.Name, request.Description, cancellationToken);
                return Ok(mapper.Map<YogaClassResponse>(result));
            }
            catch (TimeTableEntityNotFoundException<YogaClass> TimeEntityNotFound)
            {
                return NotFound(TimeEntityNotFound.Message);
            }
            catch (TimeTableInvalidOperationException TimeInvalidOperation)
            {
                return BadRequest(TimeInvalidOperation.Message);
            }
        }

        /// <summary>
        /// Редактирует имеющееся занятие
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(YogaClassResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(YogaClassRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<YogaClassModel>(request);
            var result = await yogaClassService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<YogaClassResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся занятие
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await yogaClassService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
