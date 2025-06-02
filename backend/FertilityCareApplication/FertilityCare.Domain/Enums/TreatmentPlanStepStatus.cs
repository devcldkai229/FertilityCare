using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Enums;

public enum TreatmentPlanStepStatus
{
    Planned = 1,         // Kế hoạch đã tạo, chưa bắt đầu

    InProgress = 2,      // Đang điều trị

    Paused = 3,          // Tạm dừng

    Skipped = 4,       // Hoàn tất thành công

    Failed = 5,       // Đã huỷ
}
