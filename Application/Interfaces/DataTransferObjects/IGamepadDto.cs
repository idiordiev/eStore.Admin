using System;
using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IGamepadDto : IGoodsDto
    {
        int ConnectionTypeId { get; set; }
        int FeedbackId { get; set; }
        float Weight { get; set; }
    }
}