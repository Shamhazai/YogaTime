using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaTime.Api.Models;
using YogaTime.Api.ModelsRequest.Room;
using YogaTime.Context.Contracts.Models;
using YogaTime.Services.Contracts.Exceptions;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;

namespace YogaTime.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с залами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoomController"/>
        /// </summary>
        public RoomController(IRoomService RoomService,
            IMapper mapper)
        {
            this.roomService = RoomService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех залов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoomResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await roomService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<RoomResponse>>(result));
        }

        /// <summary>
        /// Получает зал по id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await roomService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Создаёт новый зал
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(TimeTableEntityNotFoundException<Room>), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(TimeTableInvalidOperationException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await roomService.AddAsync(request.Name, request.Description, cancellationToken);
                return Ok(mapper.Map<RoomResponse>(result));
            }
            catch (TimeTableEntityNotFoundException<Room> TimeEntityNotFound)
            {
                return NotFound(TimeEntityNotFound.Message);
            }
            catch (TimeTableInvalidOperationException TimeInvalidOperation)
            {
                return BadRequest(TimeInvalidOperation.Message);
            }
        }

        /// <summary>
        /// Редактирует имеющийся зал
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(RoomRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<RoomModel>(request);
            var result = await roomService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся зал
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await roomService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
