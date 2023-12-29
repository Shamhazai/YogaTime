﻿namespace YogaTime.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class TimeTableException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableException"/> без параметров
        /// </summary>
        protected TimeTableException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected TimeTableException(string message)
            : base(message) { }
    }
}
