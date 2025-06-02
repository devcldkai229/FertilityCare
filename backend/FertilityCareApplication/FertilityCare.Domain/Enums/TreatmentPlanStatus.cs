using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Enums;

public enum TreatmentPlanStatus
{
    Planned = 1,         // Kế hoạch đã tạo, chưa bắt đầu

    InProgress = 2,      // Đang điều trị

    Paused = 3,          // Tạm dừng

    Completed = 4,       // Hoàn tất thành công

    Cancelled = 5,       // Đã huỷ

    Failed = 6           // Không hoàn tất do lỗi
}
