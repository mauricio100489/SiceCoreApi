﻿namespace SiceCoreApi.Models.Responses
{
    public class ListItemDTO<T>
    {
        public IEnumerable<T> Value { get; set; }
    }
}
